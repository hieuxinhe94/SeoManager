
using SeoManager.Constants;
using SeoManager.Dal;
using SeoManager.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SeoManager.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        private static DashBoardViewModel dashBoardViewModel { get; set; }
        public DashboardController()
        {
            dashBoardViewModel = new DashBoardViewModel();
            try
            {
                var userCurrent = (NguoiDungViewModel)System.Web.HttpContext.Current.Session["user_login"];
                if (userCurrent != null)
                {
                    dashBoardViewModel.NguoiDungViewModel = userCurrent;
                    db.Historys.Add(new Entities.History { NguoiDungId = userCurrent.Id, ThoiGian = DateTime.Now, MieuTa = "Comback to server" });
                    Task.Run(() => db.SaveChangesAsync());
                    this.ViewData["DashBoardViewModel"] = dashBoardViewModel;
                }
                else
                {
                    RedirectToAction("LogInAsync", "Account");
                }
            }
            catch (Exception ex)
            {
                RedirectToAction("LogInAsync", "Account");
                return;
            }

        }

        // GET: Dasboard
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(dashBoardViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> ChooseDomain()
        {
            db = new ApplicationContext();
            // get list domain cua nguoi dung khai bao trong profile, nhung tam thoi get het ra da
            var domainDb = await db.Domains.AsNoTracking()
                .Where(t => t.NguoiDungId != dashBoardViewModel.NguoiDungViewModel.Id || t.NguoiDungId == 1).ToListAsync();

            dashBoardViewModel.DomainViewModel = domainDb;

            return View(dashBoardViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> NguoiDungAddDomain([Bind(Include = "NguoiDungId,DomainName,DomainId,GoiCuocId")] NguoiDungThemDomainViewModel addDomainData)
        {
            db = new ApplicationContext();

            var existDomain = await db.Domains.AsNoTracking()
                .Where(t => t.URL.Equals("https://wwww." + addDomainData.DomainName) && t.NguoiDungId == 1).FirstOrDefaultAsync();
            if (existDomain != null)
            {
                var currentUSer = db.NguoiDungs.Where(t => t.Id == dashBoardViewModel.NguoiDungViewModel.Id).FirstOrDefault();
                bool isEnoughMoney = false;
                switch (addDomainData.GoiCuocId)
                {
                    case 1:
                        {
                            if (currentUSer.MoneyInAccount > Constant.GoiCuoc.Ngay)
                            {
                                isEnoughMoney = true;
                                currentUSer.MoneyInAccount -= Constant.GoiCuoc.Ngay;
                            }

                            break;
                        }
                    case 2:
                        {
                            if (currentUSer.MoneyInAccount > Constant.GoiCuoc.Gio)
                            {
                                isEnoughMoney = true;
                                currentUSer.MoneyInAccount -= Constant.GoiCuoc.Gio;
                            }

                            break;
                        }
                    case 3:
                        {
                            if (currentUSer.MoneyInAccount > Constant.GoiCuoc.Phut)
                            {
                                isEnoughMoney = true;
                                currentUSer.MoneyInAccount -= Constant.GoiCuoc.Phut;
                            }

                            break;
                        }
                    default:
                        break;
                }
                if (!isEnoughMoney)
                {
                    dashBoardViewModel.NguoiDungViewModel.Message = "Bạn không đủ tiền, vui lòng nạp thêm hoặc chọn gói cước phù hợp !";
                    System.Web.HttpContext.Current.Session["user_login"] = dashBoardViewModel.NguoiDungViewModel;
                    db.Historys.Add(new Entities.History
                    {
                        MieuTa = "Đăng ký domain " + existDomain.URL + " thất bại. Tài khoản không đủ cước",
                        NguoiDungId = currentUSer.Id,
                        ThoiGian = DateTime.Now
                    });
                    await db.SaveChangesAsync();
                    return RedirectToAction("ChooseDomain");
                }
                existDomain.NguoiDungId = dashBoardViewModel.NguoiDungViewModel.Id;
                db.Domains.AddOrUpdate(existDomain);

                currentUSer.DomainAssign++;
                db.NguoiDungs.AddOrUpdate(currentUSer);

                db.Historys.Add(new Entities.History {
                    MieuTa = "Đăng ký domain " + existDomain.URL + ". " + "" + "Tài khoản còn lại: " + currentUSer.MoneyInAccount,
                    NguoiDungId = currentUSer.Id,
                    ThoiGian = DateTime.Now
                });

                await db.SaveChangesAsync();
                dashBoardViewModel.NguoiDungViewModel.Message = "Đăng ký thành công, Tài khoản hiện tại còn: " + currentUSer.MoneyInAccount;
                // get lai 
                this.ViewData["DashBoardViewModel"] = dashBoardViewModel;
                dashBoardViewModel.NguoiDungViewModel = new NguoiDungViewModel
                {
                    Id = currentUSer.Id,
                    Email = currentUSer.Email,
                    Name = currentUSer.Ten,
                    DomainAssign = currentUSer.DomainAssign,
                    MoneyInAccount = currentUSer.MoneyInAccount,
                    PercentOfDocumentViewer = currentUSer.PercentOfDocumentViewer,
                    PercentOfProfileInfo = currentUSer.PercentOfProfileInfo,
                    Phone = currentUSer.Phone,
                    ImageUrl = currentUSer.ImageUrl
                };

                System.Web.HttpContext.Current.Session["user_login"] = dashBoardViewModel.NguoiDungViewModel;
            }
            return RedirectToAction("DomainAsync");
        }
        [HttpGet]
        public async Task<ActionResult> DomainAsync()
        {
            db = new ApplicationContext();
            // get list domain cua nguoi dung khai bao trong profile, nhung tam thoi get het ra da
            var domainDb = await db.Domains.AsNoTracking()
                .Where(t => t.NguoiDungId == dashBoardViewModel.NguoiDungViewModel.Id).ToListAsync();

            dashBoardViewModel.DomainViewModel = domainDb;

            return View(dashBoardViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> LinkAsync()
        {
            db = new ApplicationContext();
    
            var linkDb = await db.Links.AsNoTracking()
                .Where(t => t.Domain.NguoiDungId == dashBoardViewModel.NguoiDungViewModel.Id).ToListAsync();

            dashBoardViewModel.LinkViewModel = linkDb;
            return View(dashBoardViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> ProfileAsync()
        {
            db = new ApplicationContext();
            
            var domainAssignToMeDb = await db.Domains.AsNoTracking()
                .Where(t => t.NguoiDungId == dashBoardViewModel.NguoiDungViewModel.Id).ToListAsync();
            // count list link cua nguoi dung tu domain khai bao trong profile, nhung tam thoi get het ra da
            var linkCount = await db.Links.AsNoTracking()
                .Where(t => t.Domain.NguoiDungId == dashBoardViewModel.NguoiDungViewModel.Id).CountAsync();
            // sau nay dua vao properties day de tinh tien

            dashBoardViewModel.NguoiDungViewModel.DomainAssign = linkCount;
            dashBoardViewModel.DomainViewModel = domainAssignToMeDb;
            return View(dashBoardViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> PayMethodAsync()
        {
            db = new ApplicationContext();
            // count list link cua nguoi dung tu domain khai bao trong profile, nhung tam thoi get het ra da
            var linkCount = await db.Links.AsNoTracking()
                .Where(t => t.Domain.NguoiDungId == dashBoardViewModel.NguoiDungViewModel.Id).CountAsync();
            // sau nay dua vao day de tinh tien

            return View(dashBoardViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> WordAsync()
        {
            db = new ApplicationContext();
            // get list word cua nguoi dung tu link tu domain khai bao trong profile, nhung tam thoi get het ra da
            var wordOfMyLinkDb = await db.Words.AsNoTracking()
             
                .Select(t=>t.Text)
                //.Where(t => t.Link.DomainId == dashBoardViewModel.NguoiDungViewModel.Id).Select(t => t.Word)
                .ToListAsync();

            dashBoardViewModel.WordViewModel =   wordOfMyLinkDb;
            return View(dashBoardViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> HistoryAsync()
        {
            db = new ApplicationContext();
            // get list  history
            var historyDb = await db.Historys.AsNoTracking()
                .Where(t => t.NguoiDungId == dashBoardViewModel.NguoiDungViewModel.Id).ToListAsync();

            dashBoardViewModel.HistoryViewModel = historyDb;
            return View(dashBoardViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> DocumentAsync()
        {

            return View(dashBoardViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> BackLinkAndWordAsync()
        {
            db = new ApplicationContext();
            // get list back link and word 
            var backLinkAndWordDb = await db.BackLinkAndWords.AsNoTracking()
                .Where(t => db.Links.Where(k => k.Domain.NguoiDungId == dashBoardViewModel.NguoiDungViewModel.Id).Select(k => t.Id).ToList().Contains(t.LinkToId)).ToListAsync();

            dashBoardViewModel.BackLinkWordViewModel = backLinkAndWordDb;
            return View(dashBoardViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> LinkAndWordAsync()
        {
            db = new ApplicationContext();
            // get list  link and word of my domains
            var linkAndWordDb = await db.LinkAndWords.AsNoTracking()
                .Where(t => db.Links.Where(k => k.Domain.NguoiDungId == dashBoardViewModel.NguoiDungViewModel.Id).Select(k => t.Id).ToList().Contains(t.LinkId)).ToListAsync();

            dashBoardViewModel.LinkWordViewModel = linkAndWordDb;
            return View(dashBoardViewModel);
        }
    }
}
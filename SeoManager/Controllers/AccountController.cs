using SeoManager.Constants;
using SeoManager.Dal;
using SeoManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SeoManager.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        // GET: LogIn
        [HttpGet]
        public ActionResult LogInAsync()
        {
            try
            {
                var userCurrent = (NguoiDungViewModel)System.Web.HttpContext.Current.Session["user_login"];
                if (userCurrent != null)
                {
                    db.Historys.Add(new Entities.History { NguoiDungId = userCurrent.Id, ThoiGian = DateTime.Now, MieuTa = "Comback to server" });
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            catch
            {
            }

            return View();
        }
        public ActionResult LogOutAsync()
        {
            try
            {
                var userCurrent = (NguoiDungViewModel)System.Web.HttpContext.Current.Session["user_login"];
                db.Historys.Add(new Entities.History { NguoiDungId = userCurrent.Id, ThoiGian = DateTime.Now, MieuTa = "Log out Server" });
                System.Web.HttpContext.Current.Session.Clear();
            }
            catch
            {


            }
            return RedirectToAction("LogInAsync");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogInAsync([Bind(Include = "Email,Password")] NguoiDungViewModel logInVm)
        {
            if (ModelState.IsValid)
            {
                var user = db.NguoiDungs
                    .Where(t => t.Email.Equals(logInVm.Email, StringComparison.OrdinalIgnoreCase) && t.Password.Equals(logInVm.Password, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (user != null)
                {
                    //set session 
                    System.Web.HttpContext.Current.Session.Add("user_login", new NguoiDungViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Ten,
                        DomainAssign = user.DomainAssign,
                        MoneyInAccount = user.MoneyInAccount,
                        PercentOfDocumentViewer = user.PercentOfDocumentViewer,
                        PercentOfProfileInfo = user.PercentOfProfileInfo,
                        Phone = user.Phone,
                        ImageUrl = user.ImageUrl

                    });
                    db.Historys.Add(new Entities.History { NguoiDungId = user.Id, ThoiGian = DateTime.Now, MieuTa = "LogIn to server" });
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    // temp
                    Session.Add("user_login_failed_count", 1);
                }

            }
            return View();
        }

        public ActionResult RegisterAsync()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync([Bind(Include = "Name,Phone,Email,Password")] NguoiDungViewModel logInVm)
        {
            if (ModelState.IsValid)
            {
                var userInDb = db.NguoiDungs
                    .Where(t => t.Email.Equals(logInVm.Email, StringComparison.InvariantCultureIgnoreCase));

                if (!userInDb.Any())
                {
                    var newUser = new Entities.NguoiDung
                    {
                        Email = logInVm.Email,
                        Password = logInVm.Password,
                        NgayKichHoat = DateTime.Now,
                        Ten = logInVm.Name,
                        TrangThai = true,
                        VaiTroId = Constant.VaiTro.NguoiDung,
                        MoneyInAccount = 20,
                        ImageUrl = null,
                        Phone = logInVm.Phone,
                        PercentOfProfileInfo = 40,
                        PercentOfDocumentViewer = 0,
                        DomainAssign = 0,
                        FeedBackMessage = null

                    };
                    db.NguoiDungs.Add(newUser);
                    db.SaveChanges();
                    // represent user
                    newUser = db.NguoiDungs.Where(t => t.Email == newUser.Email).FirstOrDefault();
                    //set session , check already Id?
                    System.Web.HttpContext.Current.Session.Add("user_login", new NguoiDungViewModel
                    {
                        Id = newUser.Id,
                        Email = newUser.Email,
                        Name = newUser.Ten,
                        DomainAssign = newUser.DomainAssign,
                        MoneyInAccount = newUser.MoneyInAccount,
                        PercentOfDocumentViewer = newUser.PercentOfDocumentViewer,
                        PercentOfProfileInfo = newUser.PercentOfProfileInfo,
                        Phone = newUser.Phone,
                        ImageUrl = newUser.ImageUrl
                    });
                    db.Historys.Add(new Entities.History { NguoiDungId = newUser.Id, ThoiGian = DateTime.Now, MieuTa = "Register to server" });
                }
                else
                {
                    return View("This email have already used, Have you fogot a password ?");
                }

            }
            return RedirectToAction("LogInAsync", "Account");
        }
    }
}
using System;

namespace SeoManager.Entities
{
    public class NguoiDung : BaseEntities
    {
        public string Ten { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string FeedBackMessage { get; set; }
        public int VaiTroId { get; set; }
        public virtual VaiTro VaiTro { get; set; }


        public DateTime? NgayKichHoat { get; set; }
        public bool? TrangThai { get; set; }

        public int PercentOfProfileInfo
        {
            get
            {
                int i = 40;
                if (!string.IsNullOrWhiteSpace(ImageUrl))
                {
                    i += 20;
                }
                if (!string.IsNullOrWhiteSpace(FeedBackMessage))
                {
                    i += 10;
                }
                if (PercentOfDocumentViewer > 80)
                {
                    i += 10;
                }
                if (DomainAssign > 0)
                {
                    i += 10;
                }
                if (MoneyInAccount > 0)
                {
                    i += 10;
                }
                return i;
            }
            set {   }

        }
        public float MoneyInAccount { get; set; }
        public int DomainAssign { get; set; }
        public int PercentOfDocumentViewer { get; set; }
    }
}
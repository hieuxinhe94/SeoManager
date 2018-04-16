using System;
using System.Collections.Generic;

namespace SeoManager.Entities
{
    public class Domain: BaseEntities
    {
        public string Ten { get; set; }
        public string URL { get; set; }
        public string MieuTa { get; set; }
        public int ? NguoiDungId { get; set; }
        public virtual NguoiDung  NguoiDung { get; set; }
        public DateTime ? NgayKichHoat { get; set; }
        public bool ? TrangThai { get; set; }

    //    public virtual ICollection<Link> Link { get; set; }
    }
}
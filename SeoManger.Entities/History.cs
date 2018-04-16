using System;

namespace SeoManager.Entities
{
    public class History : BaseEntities
    {
        public int NguoiDungId { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }

        public string  MieuTa { get; set; }
        public DateTime ? ThoiGian { get; set; }
    }
}
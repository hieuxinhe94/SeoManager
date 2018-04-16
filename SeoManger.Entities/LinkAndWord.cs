using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeoManager.Entities
{
    public class LinkAndWord: BaseEntities
    {
        [Key, Column(Order = 2)]
        public int LinkId { get; set; }
        public virtual Link Link { get; set; }

        [Key, Column(Order = 3)]
        public int WordId { get; set; }
        public virtual Word Word { get; set; }

        public int XepHang { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public bool TrangThai { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeoManager.Entities
{
    public class Link: BaseEntities
    {
        [Key, Column(Order = 2)]
        public int DomainId { get; set; }
        public virtual Domain Domain { get; set; }

        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public DateTime ? NgayTao { get; set; }
        public bool ? TrangThai { get; set; }

        //public virtual ICollection<BackLinkAndWord> BacklinkAndWord { get; set; }
        //public virtual ICollection<LinkAndWord> LinkAndWord { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeoManager.Entities
{
    public class BackLinkAndWord : BaseEntities
    {
        
        public int  LinkId { get; set; }
        public virtual  Link Link { get; set; }

        
        public int  LinkToId { get; set; }
        public virtual Link LinkTo { get; set; }

        
        public int  WordId { get; set; }
        public virtual Word Word { get; set; }

        public DateTime ? NgayCapNhat { get; set; }
        public bool ? TrangThai { get; set; }

 
    }
}
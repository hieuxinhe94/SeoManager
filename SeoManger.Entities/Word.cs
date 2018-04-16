using System;
using System.Collections.Generic;

namespace SeoManager.Entities
{
    public class Word: BaseEntities
    {
        public string Text  { get; set; }
        public DateTime ? NgayTao { get; set; }

        //public virtual ICollection<BackLinkAndWord> BacklinkAndWord { get; set; }
        //public virtual ICollection<LinkAndWord> LinkAndWord { get; set; }
    }
}


using SeoManager.Entities;
using System.Collections.Generic;

namespace SeoManager.Models
{
    public class DashBoardViewModel
    {
        public NguoiDungViewModel NguoiDungViewModel { get; set; }
        public IEnumerable<Domain> DomainViewModel { get; set; }
        public IEnumerable<string> WordViewModel { get; set; }
        public IEnumerable<History> HistoryViewModel { get; set; }
        public IEnumerable<Link> LinkViewModel { get; set; }
        public IEnumerable<LinkAndWord> LinkWordViewModel { get; set; }
        public IEnumerable<BackLinkAndWord> BackLinkWordViewModel { get; set; }
        // new properties
        public int ProfilePercentCompleted { get; set; }
        public int DocumentercentCompleted { get; set; }

      
    }
}
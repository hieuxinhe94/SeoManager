using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoManager.Models
{
    public class AnalysicPerDay
    {
        public int UserId { get; set; }
        public int CountWord { get; set; }
        public int CountLink { get; set; }
        public int CountBackLink { get; set; }
        public int Day { get; set; }
    }

    public class AnalysisViewModel
    {
        public ICollection<AnalysicPerDay> AnalysicPerWeeks { get; set; }
    }
}
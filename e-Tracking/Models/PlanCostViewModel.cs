using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Tracking.Models
{
    public class PlanCostViewModel
    {
        public IEnumerable<SelectListItem> All_Currencies { get; set; }
    }
}
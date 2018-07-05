using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_Tracking.Models;

namespace e_Tracking.Controllers
{
    public class PlanCostController : Controller
    {
        // GET: PlanCost
        public ActionResult Index()
        {
            PlanCostViewModel model = new PlanCostViewModel();

            IEnumerable<SelectListItem> currencies = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(ci => ci.LCID).Distinct()
                .Select(id => new RegionInfo(id))
                .GroupBy(r => r.ISOCurrencySymbol)
                .Select(g => g.First())
                .Select(r => new SelectListItem
                {
                    Text = r.ISOCurrencySymbol,
                    Value = r.ISOCurrencySymbol
                });

            model.All_Currencies = currencies;

            return View(model);
        }
        public ActionResult Master()
        {
            return View();
        }
    }
}
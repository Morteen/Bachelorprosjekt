using APMKommune.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;



namespace APMKommune.Controllers
{
    public class HomeController : Controller
    {

        private APMContext db = new APMContext();
        public ActionResult Index()
        {
            calculateValues(db.Applications.ToList());
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Filtrer(int? SegmentId)
        {
            var applications = db.Applications.Include(a => a.Segment);

            if (SegmentId == null)
            {
                applications = null;
            }
            else
            {
                applications = applications.Where(a => a.SegmentId == SegmentId);
            }

            // Filter list
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name", SegmentId);
            
            if (applications != null)
            {
                calculateValues(applications.ToList());
            } else
            {
                calculateValues(db.Applications.ToList());
            }

            return View("Index");
        }

        private void calculateValues(List<Application> List)
        {
            Double yearly = 0;
            Double initial = 0;
            Double users = 0;
            Double tempBusinessValueScore = 0;
            Double tempArchitectureFitsScore = 0;
            Double tempApplicationSpeedScore = 0;
            Double tempApplicationRiskScore = 0;
            
            foreach (var item in List)
            {
                yearly += item.CostYearly;
                users += item.NumberOfUsers;
                initial += item.CostInitial;
                tempBusinessValueScore += item.BusinessValueScore;
                tempArchitectureFitsScore += item.ArchitectureFitsScore;
                tempApplicationSpeedScore += item.ApplicationSpeedScore;
                tempApplicationRiskScore += item.ApplicationRiskScore;
            }
            
            if (List.Count() != 0)
            {
                ViewBag.totYearly = yearly;
                ViewBag.averageYearly = Math.Round(yearly / List.Count(), 2);
                ViewBag.totInitial = initial;
                ViewBag.AverageInitial = Math.Round(initial / List.Count(), 2);
                ViewBag.totUsers = users;
                ViewBag.ApplicationList = List;
                ViewBag.ApplicationCount = List.Count();
                ViewBag.AverageBusinessValueScore = Math.Round(tempBusinessValueScore / List.Count(), 1);
                ViewBag.AverageArchitectureFitsScore = Math.Round(tempArchitectureFitsScore / List.Count(), 1);
                ViewBag.AverageApplicationSpeedScore = Math.Round(tempApplicationSpeedScore / List.Count(), 1);
                ViewBag.AverageApplicationRiskScore = Math.Round(tempApplicationRiskScore / List.Count(), 1);

                // Sorting list by yearly cost
                List<Application> sortByYearly_AppList = List;
                sortByYearly_AppList.Sort(delegate (Application x2, Application y2) {
                    return x2.CostYearly.CompareTo(y2.CostYearly);
                });
                ViewBag.MostExpensiveYearlyName = sortByYearly_AppList[List.Count() - 1].Name;
                ViewBag.MostExpensiveYearlyCost = sortByYearly_AppList[List.Count() - 1].CostYearly;
                ViewBag.LeastExpensiveYearlyName = sortByYearly_AppList[0].Name;
                ViewBag.LeastExpensiveYearlyCost = sortByYearly_AppList[0].CostYearly;

                // Sorting list by initial cost
                List<Application> sortByInitial_AppList = List;
                sortByInitial_AppList.Sort(delegate (Application x1, Application y1) {
                    return x1.CostInitial.CompareTo(y1.CostInitial);
                });
                ViewBag.MostExpensiveInitialName = sortByInitial_AppList[List.Count() - 1].Name;
                ViewBag.MostExpensiveInitialCost = sortByInitial_AppList[List.Count() - 1].CostInitial;
                ViewBag.LeastExpensiveInitialName = sortByInitial_AppList[0].Name;
                ViewBag.LeastExpensiveInitialCost = sortByInitial_AppList[0].CostInitial;

                
            } else
            {
                ViewBag.totYearly = null;
                ViewBag.averageYearly = null;
                ViewBag.totInitial = null;
                ViewBag.AverageInitial = null;
                ViewBag.totUsers = null;
                ViewBag.ApplicationList = null;
                ViewBag.ApplicationCount = null;
                ViewBag.AverageBusinessValueScore = null;
                ViewBag.AverageArchitectureFitsScore = null;
                ViewBag.AverageApplicationSpeedScore = null;
                ViewBag.AverageApplicationRiskScore = null;

                ViewBag.MostExpensiveYearlyName = null;
                ViewBag.MostExpensiveYearlyCost = null;
                ViewBag.LeastExpensiveYearlyName = null;
                ViewBag.LeastExpensiveYearlyCost = null;

                ViewBag.MostExpensiveInitialName = null;
                ViewBag.MostExpensiveInitialCost = null;
                ViewBag.LeastExpensiveInitialName = null;
                ViewBag.LeastExpensiveInitialCost = null;

                
            }
            
        }

    }
}
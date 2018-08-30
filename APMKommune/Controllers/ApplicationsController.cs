using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APMKommune.Models;

namespace APMKommune.Controllers
{
    public class ApplicationsController : Controller
    {
        private APMContext db = new APMContext();

        // GET: Applications
        public async Task<ActionResult> Index()
        {
            var applications = db.Applications.Include(a => a.Company).Include(a => a.Segment).Include(a => a.Service).Include(a => a.Supplier);

            // Filter lists
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name");

            List < Application > appList = applications.ToList();
            ViewBag.ApplicationList = appList;

            return View();
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Application> appList = db.Applications.Include(a => a.Company).Include(a => a.Segment).Include(a => a.Service).Include(a => a.Supplier).ToList();
                        Application application = appList.Find(a=>a.ApplicationId==id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name");
            ViewBag.ServiceId = 0;
            ViewBag.ServiceList = db.Services.ToList();
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "ApplicationId,Name,Description,NumberOfUsers,OperatedBy,ContractInformation,InfoLink,Status,Administrator,SuperUsers,Type,LastUpgraded,ExternalUsers,CostYearly,CostInitial,UsesSharedComponents,Strengths,Weaknesses,ContractResignation,BusinessValueScore,ArchitectureFitsScore,ApplicationRiskScore,ApplicationSpeedScore,CompanyId,SegmentId,ServiceId,SupplierId")] Application application)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Application application)
        {
            if (ModelState.IsValid && application.ServiceId != 0)
            {
                db.Applications.Add(application);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (application.ServiceId == 0)
            {
                ViewBag.ServiceIdError = "Tjeneste field is required";
            } else
            {
                ViewBag.ServiceIdError = "";
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", application.CompanyId);
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name", application.SegmentId);
            ViewBag.ServiceId = application.ServiceId;
            ViewBag.ServiceList = db.Services.ToList();
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", application.SupplierId);
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = await db.Applications.FindAsync(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", application.CompanyId);
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name", application.SegmentId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "Name", application.ServiceId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", application.SupplierId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ApplicationId,Name,Description,NumberOfUsers,OperatedBy,ContractInformation,InfoLink,Status,Administrator,SuperUsers,Type,LastUpgraded,ExternalUsers,CostYearly,CostInitial,UsesSharedComponents,Strengths,Weaknesses,ContractResignation,BusinessValueScore,ArchitectureFitsScore,ApplicationRiskScore,ApplicationSpeedScore,CompanyId,SegmentId,ServiceId,SupplierId")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", application.CompanyId);
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name", application.SegmentId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "Name", application.ServiceId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", application.SupplierId);
            return View(application);
        }

        [HttpPost]
        public JsonResult Delete(int ApplicationId)
        {
            Application app = db.Applications.Find(ApplicationId);
            db.Applications.Remove(app);
            db.SaveChanges();

            //Check to see if Application was actually deleted
            if (db.Applications.Find(ApplicationId) == null)
                return Json(true, JsonRequestBehavior.AllowGet); // yes
            else
                return Json(null, JsonRequestBehavior.AllowGet); // no
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public async Task<ActionResult> Filtrer(int? SegmentId, int? SupplierId)
        {
            var applications = db.Applications.Include(a => a.Company).Include(a => a.Segment).Include(a => a.Service).Include(a => a.Supplier);

            if (SegmentId == null)
            {
                if (SupplierId != null)
                {
                    applications = db.Applications.Include(a => a.Company).Include(a => a.Segment).Include(a => a.Service).Include(a => a.Supplier)
                        .Where(a => a.SupplierId == SupplierId);
                }   
            } else
            {
                if (SupplierId == null)
                {
                    applications = db.Applications.Include(a => a.Company).Include(a => a.Segment).Include(a => a.Service).Include(a => a.Supplier)
                        .Where(a => a.SegmentId == SegmentId);
                }
                else
                {
                    applications = db.Applications.Include(a => a.Company).Include(a => a.Segment).Include(a => a.Service).Include(a => a.Supplier)
                        .Where(a => a.SegmentId == SegmentId && a.SupplierId == SupplierId);
                }
            }

            // Filter lists
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name", SegmentId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", SupplierId);

            List<Application> appList = applications.ToList();
            ViewBag.ApplicationList = appList;

            return View("Index");
        }
        
        public String SelectColumns(int selectedSegmentId)
        {
            ViewBag.ServiceId = new SelectList(db.Services.Where(s => s.SegmentId == selectedSegmentId), "ServiceId", "Name");
            return "blablabla";
        }
    }
}

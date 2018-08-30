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
    public class ServiceController : Controller
    {
        private APMContext db = new APMContext();

        // GET: Service
        public ActionResult Index()
        {
            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name");
            List<Service> List = db.Services.Include(a => a.Segment).ToList();
            ViewBag.Serviceliste = List;
            return View();
        }

        // GET: Service/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Service/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(Service model)
        {

            ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name", model.SegmentId);
            List<Service> List = db.Services.ToList();
            ViewBag.Serviceliste = new SelectList(List, "ServiceId", "ServiceNavn");
            if (model.ServiceId > 0)
            {

                if (ModelState.IsValid)
                {
                   Service service = db.Services.Find(model.ServiceId);

                    service.Name = model.Name;
                    service.SegmentId = model.SegmentId;

                    db.Entry(service).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            else
            {

                Service service = new Service();
                service.Name = model.Name;
                service.SegmentId = model.SegmentId;

                db.Services.Add(service);
                db.SaveChanges();

            }

            return View(model);
        }


        // GET: Service/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ServiceId,Name")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Service/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }
        
        [HttpPost]
        public JsonResult Delete(int ServiceId)
        {
            Service service= db.Services.Find(ServiceId);
            db.Services.Remove(service);
            db.SaveChanges();

            //Check to see if Service was actually deleted
            if (db.Services.Find(ServiceId) == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(null, JsonRequestBehavior.AllowGet);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult ShowSegment(int ServiceId)
        {
            Service service = db.Services.Find(ServiceId);
            List<Service> List = new List<Service>();
            List.Add(service);
            ViewBag.Segmentlist = List; ;

            return PartialView("Partial1");
        }


        //AddEditService
        public ActionResult AddEditService(int ServiceId)
        {
            List<Segment> list = db.Segments.ToList();
            ViewBag.segmentList = list;

            Service model = new Service();
            if (ServiceId > 0)
            {
                
                Service ser = db.Services.Find(ServiceId);
                model.ServiceId = ser.ServiceId;
                model.Name = ser.Name;
                model.SegmentId = ser.SegmentId;
                ViewBag.SegmentId = new SelectList(db.Segments, "SegmentId", "Name", model.SegmentId);

            }

            return PartialView("_AddEditService", model);


        }

    }
}

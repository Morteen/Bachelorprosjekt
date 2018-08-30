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
    public class SegmentController : Controller
    {
        private APMContext db = new APMContext();



        // GET: Segment
        public ActionResult Index()
        {
            
            List<Segment> List = db.Segments.ToList();// await db.Segments.FindAsync
            ViewBag.Segmentliste = List;
            return View();
        }


        [HttpPost]
        public ActionResult Index(Segment model)
        {

            Segment segment = new Segment();
            segment.Name = model.Name;
           
            db.Segments.Add(segment);
            db.SaveChanges();
            int segId = segment.SegmentId;

            return View(model);
        }




        // GET: Segment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Segment segment = await db.Segments.FindAsync(id);
            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(segment);
        }

        // GET: Segment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Segment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(Segment model)
        {


            List<Segment> List = db.Segments.ToList();
            ViewBag.Segmentliste = new SelectList(List, "SegmentId", "SegmentNavn");
            if (model.SegmentId > 0)
            {

                if (ModelState.IsValid)
                {
                    Segment segment = db.Segments.Find(model.SegmentId);
                                       
                    segment.Name = model.Name;
                   
                    db.Entry(segment).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            else
            {
                Segment segment = new Segment();
                segment.Name = model.Name;
              
                db.Segments.Add(segment);
                db.SaveChanges();

            }

            return View(model);
        }


        // GET: Segment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Segment segment = await db.Segments.FindAsync(id);
            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(segment);
        }

        // POST: Segment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SegmentId,Name")] Segment segment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(segment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(segment);
        }

        // POST: Segments/Delete/5
        [HttpPost]
        public JsonResult Delete(int SegmentId)
        {
            Segment segment = db.Segments.Find(SegmentId);
            db.Segments.Remove(segment);
            db.SaveChanges();

            //Check to see if Segment was actually deleted
            if (db.Segments.Find(SegmentId) == null)
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


        public ActionResult ShowSegment(int SegmentId)
        {
            Segment segment = db.Segments.Find(SegmentId);
            List<Segment> List = new List<Segment>();
            List.Add(segment);
            ViewBag.Segmentlist = List; ;

            return PartialView("Partial1");
        }




        public ActionResult AddEditSegment(int SegmentId)
        {
            Segment model = new Segment();
            if (SegmentId > 0)
            {
                Segment seg = db.Segments.Find(SegmentId);
                model.SegmentId = seg.SegmentId;
                model.Name = seg.Name;
               

            }
            return PartialView("_AddEditPartial", model);


        }


    }
}

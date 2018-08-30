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
    public class DatasetController : Controller
    {
        private APMContext db = new APMContext();
        
        // GET: Dataset
        public  ActionResult Index()
        {
           
            List<Dataset> list= db.Datasets.Include(d => d.Application).ToList();
            ViewBag.Datasetliste = list;
            return View();
          
        }
        [HttpPost]
        public ActionResult Index(Dataset model)
        {
            Dataset data = new Dataset();
            data.Name = model.Name;
            data.Description = model.Description;
            data.Application.Name = model.Application.Name;
            data.isAccessible = model.isAccessible;
            data.isAccessible = model.isAccessible;

            db.Datasets.Add(data);
            db.SaveChanges();
            int dataId = data.DatasetId;

            return View(model);
        }

        

        // GET: Dataset/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dataset dataset = await db.Datasets.FindAsync(id);
            if (dataset == null)
            {
                return HttpNotFound();
            }
            return View(dataset);
        }

        // GET: Dataset/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "Name");
            return View();
        }

        // POST: Dataset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(Dataset model)
        {

            List<Dataset> List = db.Datasets.ToList();
            ViewBag.Datasetliste = new SelectList(List, "DatasetId ", "Name");
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "Name", model.ApplicationId);

            if (ModelState.IsValid)
            {
                if (model.DatasetId >0)
                {

                    Dataset data = db.Datasets.Find(model.DatasetId);
                    data.Name = model.Name;
                    data.Description = model.Description;
                    data.ApplicationId = model.ApplicationId;
                    data.isAccessible = model.isAccessible;
                    data.isMasterData = model.isMasterData;
                    
    {

    }
                    db.Entry(data).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    

                }
                else
                {
                    Dataset data = new Dataset();
                    
                    data = model;

                    db.Datasets.Add(data);
                    db.SaveChanges();
                }
            }
            return View(model);
        }

        // GET: Dataset/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dataset dataset = await db.Datasets.FindAsync(id);
            if (dataset == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "Name", dataset.ApplicationId);
            return View(dataset);
        }

        // POST: Dataset/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DatasetId,Name,Description,isMasterData,isAccessible,ApplicationId")] Dataset dataset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dataset).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "Name", dataset.ApplicationId);
            return View(dataset);
        }

        // GET: Dataset/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dataset dataset = await db.Datasets.FindAsync(id);
            if (dataset == null)
            {
                return HttpNotFound();
            }
            return View(dataset);
        }

        
        [HttpPost]

        public JsonResult Delete(int DatasetId)
        {
            Dataset data = db.Datasets.Find(DatasetId);
            db.Datasets.Remove(data);
            db.SaveChanges();
            //Check to see if Dataset was actually deleted
            if (db.Datasets.Find(DatasetId) == null)
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


        public ActionResult ShowSegment(int DatasetId)
        {
            Dataset data = db.Datasets.Find(DatasetId);
            List<Dataset> List = new List<Dataset>();
            List.Add(data);
            ViewBag.Datasetliste = List; ;

            return PartialView("_PartialDetails");
        }

        
        public ActionResult AddEditDataset(int DatasetId)
        {
            List<Application> list= db.Applications.ToList();

            ViewBag.appliste = list;
            
            Dataset model = new Dataset();
          
            if (DatasetId != 0)
            {
                Dataset data = db.Datasets.Find(DatasetId);
                model = data;
            }

            return PartialView("_AddEditPartial", model);
        }
    }
}

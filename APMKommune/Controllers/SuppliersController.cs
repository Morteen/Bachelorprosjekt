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
    public class SuppliersController : Controller
    {
        private APMContext db = new APMContext();
        

           

        
        // GET: Suppliers
        public ActionResult Index()

        {

            List<Supplier> List = db.Suppliers.ToList();
            ViewBag.Supplierliste = List;
            return View();
        }



        [HttpPost]
        public ActionResult Index(Supplier model)

        {
            
            Supplier supplier = new Supplier();
            supplier.Name = model.Name;

            db.Suppliers.Add(supplier);
            db.SaveChanges();
            int supId = supplier.SupplierId;

            return View(model);
        }





        // GET: Suppliers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = await db.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier model)
        {


            List<Supplier> List = db.Suppliers.ToList();
            ViewBag.Segmentliste = new SelectList(List, "SupplierId", "Name");
            if (model.SupplierId > 0)
            {

                if (ModelState.IsValid)
                {
                    Supplier supplier = db.Suppliers.Find(model.SupplierId);

                    supplier.Name = model.Name;

                    db.Entry(supplier).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            else
            {
                Supplier supplier = new Supplier();
                supplier.Name = model.Name;

                db.Suppliers.Add(supplier);
                db.SaveChanges();

            }

            return View(model);
        }

        // GET: Suppliers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = await db.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SupplierId,Name")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = await db.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        

        [HttpPost]

        public JsonResult Delete(int SupplierId)
        {
            Supplier supplier = db.Suppliers.Find(SupplierId);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();

            //Check to see if Supplier was actually deleted
            if (db.Suppliers.Find(SupplierId) == null)
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


        public ActionResult ShowSegment(int SupplierId)
        {
           Supplier supplier = db.Suppliers.Find(SupplierId);
            List<Supplier> List = new List<Supplier>();
            List.Add(supplier);
            ViewBag.Segmentlist = List; ;

            return PartialView("Partial1");
        }




        public ActionResult AddEditSegment(int SupplierId)
        {
            Supplier model = new Supplier();
            if (SupplierId > 0)
            {
                Supplier sup = db.Suppliers.Find(SupplierId);
                model.SupplierId = sup.SupplierId;
                model.Name = sup.Name;


            }
            return PartialView("_AddEditSupplier", model);


        }


    }
}

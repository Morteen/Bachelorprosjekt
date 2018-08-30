using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APMKommune.Models;

namespace APMKommune.Controllers
{
    public class CompanyController : Controller
    {
        private APMContext db = new APMContext();

        // GET: Company
        public ActionResult Index()
        {

            List<Company> List = db.Companies.ToList();
            ViewBag.Companylist = List;
            return View(db.Companies.ToList());
        }

        
        // GET: Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult Create(Company model)
        {
            List<Company> List = db.Companies.ToList();
            ViewBag.Companylist = new SelectList(List, "CompanyId", "Name", "CompanyNr");

            if (model.CompanyId != 0)
            {

                if (ModelState.IsValid)
                {
                    Company comp = db.Companies.Find(model.CompanyId);

                    //
                   comp.Name = model.Name;
                    comp.CompanyNr = model.CompanyNr;
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            else
            {
                Company comp = new Company();
                comp.Name = model.Name;
               comp.CompanyNr = model.CompanyNr;
                db.Companies.Add(comp);
                db.SaveChanges();

            }

            return View(model);
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,Name,CompanyNr")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        [HttpPost]
        public JsonResult Delete(int CompanyId)
        {
            Company comp = db.Companies.Find(CompanyId);
            db.Companies.Remove(comp);
            db.SaveChanges();

            //Check to see if Company was actually deleted
            if (db.Companies.Find(CompanyId) == null)
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



        public ActionResult ShowSegment(int CompanyId)
        {
            Company company= db.Companies.Find(CompanyId);
            List<Company> List = new List<Company>();
            List.Add(company);
            ViewBag.Companylist = List; ;

            return PartialView("_PartialDitales");
        }


        public ActionResult AddEditSegment(int CompanyId)
        {
            Company model = new Company();
            if (CompanyId > 0)
            {
                Company comp = db.Companies.Find(CompanyId);
                model.CompanyId = comp.CompanyId;
                model.Name = comp.Name;
                model.CompanyNr = comp.CompanyNr;

            }
            return PartialView("_AddEditPartial", model);
            
        }

    }
}

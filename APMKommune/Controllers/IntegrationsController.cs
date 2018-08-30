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
using System.Collections;

namespace APMKommune.Controllers
{
    public class IntegrationsController : Controller
    {
        private APMContext db = new APMContext();

        // GET: Integrations
        public ActionResult Index()
        {


            List<Application> list = db.Applications.ToList();
            ViewBag.ShowApp = list;
            List<Integration> List = db.Integrations.Include(i => i.Application).ToList();
            ViewBag.Integrasjonsliste = List;
            return View();
        }



        [HttpPost]
        public ActionResult Index(Integration model)

        {




            Integration integration = new Integration();
            integration.Name = model.Name;

            db.Integrations.Add(integration);
            db.SaveChanges();
            int InteId = integration.IntegrationId;

            return View(model);
        }







        // GET: Integrations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integration integration = await db.Integrations.FindAsync(id);
            if (integration == null)
            {
                return HttpNotFound();
            }
            return View(integration);
        }

        // GET: Integrations/Create
        public ActionResult Create()
        {
            ViewBag.Appliste = new SelectList(db.Applications, "ApplicationId", "Name");
            ViewBag.TargetAppliste = new SelectList(db.Applications, "ApplicationId", "Name");
            return View();
        }

        // POST: Integrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(Integration model)
        {
            
            List<Integration> List = db.Integrations.Include(i => i.Application).ToList();
            ViewBag.Integrasjonsliste = new SelectList(List, "IntegrationId", "Navn");
            ViewBag.Appliste = new SelectList(db.Applications, "ApplicationId", "Name", model.ApplicationId);
            ViewBag.TargetAppliste = new SelectList(db.Applications, "ApplicationId", "Name", model.TargetApplicationId);

            if (ModelState.IsValid)
            {
                if (model.IntegrationId != 0)
                {
                    Integration integration = db.Integrations.Find(model.IntegrationId);

                    integration.Name = model.Name;
                    integration.Type = model.Type;
                    integration.Description = model.Description;
                    integration.ApplicationId = model.ApplicationId;
                    integration.TargetApplicationId = model.TargetApplicationId;

                    db.Entry(integration).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    Integration integration = model;
                    db.Integrations.Add(integration);
                    db.SaveChanges();

                }
            }
            
            return View(model);
        }

        // GET: Integrations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integration integration = await db.Integrations.FindAsync(id);
            if (integration == null)
            {
                return HttpNotFound();
            }
            return View(integration);
        }

        // POST: Integrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IntegrationId,Name,Type,Description")] Integration integration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(integration).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(integration);
        }

        // GET: Integrations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integration integration = await db.Integrations.FindAsync(id);
            if (integration == null)
            {
                return HttpNotFound();
            }
            return View(integration);
        }

        [HttpPost]

        public JsonResult Delete(int IntegrationId)
        {
            Integration integration = db.Integrations.Find(IntegrationId);
            db.Integrations.Remove(integration);
            db.SaveChanges();

            //Check to see if Integrations was actually deleted
            if (db.Integrations.Find(IntegrationId) == null)
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


        //AddEditIntegrasjon
        public ActionResult AddEditIntegration(int IntegrationId)
        {
            List<Application> list = db.Applications.ToList();

            ViewBag.Appliste = list;
            ViewBag.TargetAppliste = list;
            
            Integration model = new Integration();
            if (IntegrationId != 0)
            {
                model = db.Integrations.Find(IntegrationId);
            }
            return PartialView("AddEditIntegrasjon", model);
        }

    }
}

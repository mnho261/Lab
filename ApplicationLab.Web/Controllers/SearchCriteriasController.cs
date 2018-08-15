using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicationLab.Models.AirportData;

namespace ApplicationLab.Web.Controllers
{
    public class SearchCriteriasController : Controller
    {
        private AirportDataDB db = new AirportDataDB();

        // GET: SearchCriterias
        public ActionResult Index()
        {
            return View(db.SearchCriterias.ToList());
        }

        // GET: SearchCriterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchCriteria searchCriteria = db.SearchCriterias.Find(id);
            if (searchCriteria == null)
            {
                return HttpNotFound();
            }
            return View(searchCriteria);
        }

        // GET: SearchCriterias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchCriterias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FilteredItem,Model,View,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate")] SearchCriteria searchCriteria)
        {
            if (ModelState.IsValid)
            {
                db.SearchCriterias.Add(searchCriteria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(searchCriteria);
        }

        // GET: SearchCriterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchCriteria searchCriteria = db.SearchCriterias.Find(id);
            if (searchCriteria == null)
            {
                return HttpNotFound();
            }
            return View(searchCriteria);
        }

        // POST: SearchCriterias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FilteredItem,Model,View,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate")] SearchCriteria searchCriteria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(searchCriteria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(searchCriteria);
        }

        // GET: SearchCriterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchCriteria searchCriteria = db.SearchCriterias.Find(id);
            if (searchCriteria == null)
            {
                return HttpNotFound();
            }
            return View(searchCriteria);
        }

        // POST: SearchCriterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SearchCriteria searchCriteria = db.SearchCriterias.Find(id);
            db.SearchCriterias.Remove(searchCriteria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

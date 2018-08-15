using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicationLab.Models.AirportData;
using ApplicationLab.Web.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace ApplicationLab.Web.Controllers
{
    public class AirportCodesController : Controller
    {
        private AirportDataDB db = new AirportDataDB();

        // GET: AirportCodes
        public ActionResult Index(int? id, int? page, string currentFilter, string searchString, string filterOption, string sortBy, string sortOrder, string sortActive, string pSort, string pagingSort)
        {
            #region Initialize Variables
            AirportCodeIndexData viewModel = new AirportCodeIndexData();
            // To use Custom Stored Procedures use the following code instead:
            // db.Database.SqlQuery<AirportCode>("dbo.AirportCode_SelectAll").AsQueryable();
            IQueryable<AirportCode> qListAirportCodes = db.AirportCodes.AsQueryable().OrderBy(s => s.IATACode);
            string sortValue = string.Empty;
            #endregion
 
            #region search

            qListAirportCodes = SearchResult(qListAirportCodes, searchString, currentFilter, filterOption);

            #endregion

            #region Sorting
            if (string.IsNullOrEmpty(sortOrder))
            {
                ViewBag.sortOrder = string.IsNullOrEmpty(sortOrder) ? "_desc" : "";
            }

            if (sortActive == "1")
            {
                ViewBag.pSort = sortOrder;
            }
            else
            {
                ViewBag.pSort = pSort;
            }
            if (pagingSort == "1")
            {
                sortOrder = ViewBag.pSort;
                ViewBag.sortOrder = (sortOrder == "_desc" ? "" : "_desc");
            }

            ViewBag.sortBy = sortBy;
            
            sortValue = sortBy + sortOrder;

            switch (sortValue.ToLower())
            {
                case "iatacode":
                    qListAirportCodes = qListAirportCodes.OrderBy(s => s.IATACode);
                    break;
                case "iatacode_desc":
                    qListAirportCodes = qListAirportCodes.OrderByDescending(s => s.IATACode);
                    break;
                case "airportname":
                    qListAirportCodes = qListAirportCodes.OrderBy(s => s.AirportName);
                    break;
                case "airportname_desc":
                    qListAirportCodes = qListAirportCodes.OrderByDescending(s => s.AirportName);
                    break;
                default:
                    qListAirportCodes = qListAirportCodes.OrderBy(s => s.IATACode);
                    break;
            }
            #endregion

            #region Build Search Criteria Dropdown List

            List<SelectListItem> listFitlerOption = new List<SelectListItem>();
            listFitlerOption.Add(new SelectListItem { Value = "0", Text = "Search All..." });
            bool selectedValue = false;
            foreach (SearchCriteria sc in db.SearchCriterias)
            {
                if (filterOption != null)
                {
                    if (sc.ID.ToString().ToLower() == filterOption.ToLower())
                    {
                        selectedValue = true;
                    }
                }
                listFitlerOption.Add(new SelectListItem { Value = sc.ID.ToString(), Text = sc.FilteredItem, Selected = selectedValue });
                viewModel.ListSearchCriteria = listFitlerOption;
            }
            #endregion

            #region Paging
            int pageSize = 11;
            int pageNumber = (page ?? 1);
            ViewBag.ResultsCount = qListAirportCodes.Count();
            ViewBag.pageNumber = pageNumber;
            viewModel.ListAirportCode = qListAirportCodes.ToPagedList(pageNumber, pageSize);
            #endregion

            return View(viewModel);
        }

        // GET: AirportCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirportCode airportCode = db.AirportCodes.Find(id);
            if (airportCode == null)
            {
                return HttpNotFound();
            }
            return View(airportCode);
        }

        // GET: AirportCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AirportCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AirportCodeID,IATACode,AirportName,City,Country")] AirportCode airportCode)
        {
            if (ModelState.IsValid)
            {
                db.AirportCodes.Add(airportCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(airportCode);
        }

        // GET: AirportCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirportCode airportCode = db.AirportCodes.Find(id);
            if (airportCode == null)
            {
                return HttpNotFound();
            }
            return View(airportCode);
        }

        // POST: AirportCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AirportCodeID,IATACode,AirportName,City,Country")] AirportCode airportCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airportCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(airportCode);
        }

        // GET: AirportCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirportCode airportCode = db.AirportCodes.Find(id);
            if (airportCode == null)
            {
                return HttpNotFound();
            }
            return View(airportCode);
        }

        // POST: AirportCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AirportCode airportCode = db.AirportCodes.Find(id);
            db.AirportCodes.Remove(airportCode);
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

        private IQueryable<AirportCode> FilteredResults(IQueryable<AirportCode> qListAirportCodes, string searchString, string filterOption)
        {
            List<AirportCode> listAirportCode = new List<AirportCode>();

            if (filterOption == "0")
            {
                listAirportCode = qListAirportCodes.Where(s => s.AirportName.Contains(searchString)
                                                                || s.City.Contains(searchString)
                                                                || s.Country.Contains(searchString)
                                                                || s.IATACode.Contains(searchString)

                ).ToList();
            }
            else if (filterOption == "1")
            {
                listAirportCode = qListAirportCodes.Where(s => s.IATACode.Contains(searchString)
                ).ToList();
            }
            else if (filterOption == "2")
            {
                listAirportCode = qListAirportCodes.Where(s => s.AirportName.Contains(searchString)
                ).ToList();
            }
            else if (filterOption == "3")
            {
                listAirportCode = qListAirportCodes.Where(s => s.City.Contains(searchString)
                ).ToList();
            }
            else if (filterOption == "4")
            {
                listAirportCode = qListAirportCodes.Where(s => s.Country.Contains(searchString)
                ).ToList();
            }
            return listAirportCode.AsQueryable();
        }

        private IQueryable<AirportCode> SearchResult(IQueryable<AirportCode> qListAirportCodes, string searchString, string currentFilter, string filterOption)
        {
            #region Search Feature
            if (searchString == null)
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilterOption = filterOption;
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                return FilteredResults(qListAirportCodes, searchString, filterOption);
            }
            else
            {
                return qListAirportCodes;
            }
            #endregion
        }

        //private IQueryable<AirportCode> SortResults(IQueryable<AirportCode> qListAirportCodes, )
        //{

        //}

    }
}

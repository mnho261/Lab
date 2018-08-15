using ApplicationLab.Models.AirportData;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ApplicationLab.Web.ViewModels
{
    public class AirportCodeIndexData
    {
        public IPagedList<AirportCode> ListAirportCode { get; set; }
        public IEnumerable<SelectListItem> ListSearchCriteria { get; set; }
    }
}
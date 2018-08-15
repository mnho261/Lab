using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApplicationLab.Models.AirportData;
using ApplicationLab.Web.ViewModels;

namespace ApplicationLab.Web.WebAPIControllers
{
    public class AirportCodesAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<AirportCodeSearch> Get(string query = "", string filterOption = null)
        {
           string newfilterOptions = null;
            using (var db = new AirportDataDB())
            {
                if (filterOption == "0")
                {
                    newfilterOptions = null;
                }
                else
                {
                    newfilterOptions = filterOption;
                }
                List<AirportCodeSearch> eItem = db.Database.SqlQuery<AirportCodeSearch>("[dbo].[AirportCode_SelectAC] @p0, @p1", query, newfilterOptions).ToList();
                return eItem.ToList();
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
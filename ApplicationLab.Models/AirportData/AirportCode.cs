namespace ApplicationLab.Models.AirportData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AirportCode")]
    public partial class AirportCode
    {
        public int AirportCodeID { get; set; }

        [StringLength(3)]
        public string IATACode { get; set; }

        [StringLength(100)]
        public string AirportName { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }
    }
}

namespace ApplicationLab.Models.AirportData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SearchCriteria")]
    public partial class SearchCriteria
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string FilteredItem { get; set; }

        [StringLength(150)]
        public string Model { get; set; }

        [StringLength(150)]
        public string View { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}

namespace ApplicationLab.Models.AirportData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AirportDataDB : DbContext
    {
        public AirportDataDB()
            : base("name=AirportDataDB")
        {
        }

        public virtual DbSet<AirportCode> AirportCodes { get; set; }
        public virtual DbSet<SearchCriteria> SearchCriterias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirportCode>()
                .Property(e => e.IATACode)
                .IsUnicode(false);

            modelBuilder.Entity<AirportCode>()
                .Property(e => e.AirportName)
                .IsUnicode(false);

            modelBuilder.Entity<AirportCode>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<AirportCode>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<SearchCriteria>()
                .Property(e => e.FilteredItem)
                .IsUnicode(false);

            modelBuilder.Entity<SearchCriteria>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<SearchCriteria>()
                .Property(e => e.View)
                .IsUnicode(false);

            modelBuilder.Entity<SearchCriteria>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SearchCriteria>()
                .Property(e => e.LastModifiedBy)
                .IsUnicode(false);
        }
    }
}

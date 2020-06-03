namespace BiljettService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BiljettModel : DbContext
    {
        public BiljettModel()
            : base("name=BiljettModel")
        {
        }

        public virtual DbSet<BokadePlatser> BokadePlatser { get; set; }
        public virtual DbSet<Bokningar> Bokningar { get; set; }
        public virtual DbSet<VisningsSchema> VisningsSchema { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

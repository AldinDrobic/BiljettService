namespace BiljettService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bokningar")]
    public partial class Bokningar
    {
        public int Id { get; set; }

        public int VisningsSchemaId { get; set; }

        public int KundId { get; set; }
    }
}

namespace BiljettService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BokadePlatser")]
    public partial class BokadePlatser
    {
        public int Id { get; set; }

        public int VisningsSchemaId { get; set; }

        public int AntalBokadePlatser { get; set; }
    }
}

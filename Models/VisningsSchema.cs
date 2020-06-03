namespace BiljettService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VisningsSchema")]
    public partial class VisningsSchema
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FilmTitel { get; set; }

        [Required]
        [StringLength(50)]
        public string SalongsNamn { get; set; }

        [Required]
        public DateTime Visningstid { get; set; }
    }
}

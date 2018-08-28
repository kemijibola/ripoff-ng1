using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ripoffnigeria.DTO
{
    [Table("Rebuttal")]
    public class Rebuttal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        [Required]
        public string RebuttalText { get; set; }
        public string address { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<RebuttalImage> RebuttalImage { get; set; }

    }
}

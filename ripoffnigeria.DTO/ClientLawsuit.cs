using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ripoffnigeria.DTO
{
    [Table("ClientLawsuit")]
    public class ClientLawsuit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }

        [Required]
        public int LawfirmId { get; set; }
        [ForeignKey("LawfirmId")]
        public virtual LawFirm LawFirm { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartCreated { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndCreated { get; set; }
        [Required]
        [StringLength(250)]
        public string Resolution { get; set; }

    }
}
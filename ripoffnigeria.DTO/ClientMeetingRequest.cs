using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ripoffnigeria.DTO
{
    [Table("ClientMeetingRequest")]
    public class ClientMeetingRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int FirmRegionId { get; set; }
        [ForeignKey("FirmRegionId")]
        public virtual FirmRegion FirmRegion { get; set; }
        [Required]
        public int AlternateRegionId { get; set; }
        [ForeignKey("AlternateRegionId")]
        public virtual FirmRegion AltFirmRegion { get; set; }
        [Required]
        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }
        public int? PaymentTypeId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public virtual PaymentType PaymentType { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public bool AssignedToFirm { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OpenCreated { get; set; }
        public bool isValid { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace ripoffnigeria.DTO
{
    [Table("LawFirm")]
    public class LawFirm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirmName { get; set; }
        public string firmHolder { get; set; }
        public int regionId { get; set; }
        [ForeignKey("regionId")]
        public virtual FirmRegion FirmRegion { get; set; }
        public string includeLocation { get; set; }
        public string firmAddress { get; set; }
        public string Country { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public bool isInHouse { get; set; }
        public DateTime registrationDate { get; set; }


    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ripoffnigeria.DTO
{
    [Table("Report")]
    
    public class Report
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Company { get; set; }
        public string Alias { get; set; }
        public string WebSite { get; set; }
        public int LocationTypeId { get; set; }
        [ForeignKey("LocationTypeId")]
        public virtual LocationType LocationType { get; set; }
        public string Street { get; set; }
       
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Ignore, IsReference = true)]
        public virtual City City { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Ignore, IsReference = true)]
        public virtual State State { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string fax { get; set; }

        [Required]
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [Required]
        public string ReportText { get; set; }
        public bool OnlineTransaction { get; set; }
        public bool CreditCard { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }
        public bool ripTerms { get; set; }
        public bool? contactbyMedia { get; set; }

        public virtual ICollection<ReportImage> ReportImages { get; set; }
        public virtual ICollection<Rebuttal> Rebuttals { get; set; }
        public virtual ICollection<ClientLawsuit> ClientLawsuits { get; set; }
        public virtual ICollection<CaseUpdate> CaseUpdates { get; set; }


    }
}

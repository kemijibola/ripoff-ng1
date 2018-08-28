using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ripoffnigeria.DTO;

namespace ripoffnigeriaonline.Models
{
    public class ReportModel
    {
        public Report Report { get; set; }
        public PhotoViewModel PhotoViewModel { get; set; }
    }
    public class ReportUpdateModel
    {
        [Required]
        [Display(Name = "ReportId")]
        public int ReportId { get; set; }
        [Required]
        [Display(Name = "Status")]
        public bool Status { get; set; }
    }
}
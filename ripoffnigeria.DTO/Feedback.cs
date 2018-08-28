using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("Feedback")]
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string userName { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {10} characters long.")]
        [Display(Name = "FeedbackMssg")]
        public string FeedbackMssg { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
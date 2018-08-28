using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("FirmComment")]
    public class FirmComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string userName { get; set; }
        public int firmId { get; set; }
        [ForeignKey("firmId")]
        public virtual RipOffFirm RipOffFirm { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {10} characters long.")]
        [Display(Name = "review")]
        public string review { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("FirmImage")]
    public class FirmImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int RipOffFirmId { get; set; }
        [ForeignKey("RipOffFirmId")]
        public virtual RipOffFirm RipOffFirm { get; set; }
        public string ImagePath { get; set; }
    }
}
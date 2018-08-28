using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("FirmCategory")]
    public class FirmCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int RipOffFirmId { get; set; }
        [ForeignKey("RipOffFirmId")]
        public virtual RipOffFirm RipOffFirm { get; set; }

        public int lawCategoryId { get; set; }
        [ForeignKey("lawCategoryId")]
        public virtual LawCategory LawCategory { get; set; }
    }
}
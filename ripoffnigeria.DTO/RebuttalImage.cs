using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("RebuttalImage")]
    public class RebuttalImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int RebuttalId { get; set; }
        [ForeignKey("RebuttalId")]
        public virtual Rebuttal Rebuttal { get; set; }
        public string ImagePath { get; set; }
        public string rebuttalImageCaption { get; set; }
    }
}

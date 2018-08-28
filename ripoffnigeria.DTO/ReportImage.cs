using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("ReportImage")]
    public class ReportImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }
        public string ImagePath { get; set; }
        public string ImageCaption { get; set; }
    }
}

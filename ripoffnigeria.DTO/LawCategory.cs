using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("LawCategory")]
    public class LawCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string areaOfPreference { get; set; }

        public string generalCategory {get;set;}

        public int LawTypeCategoryId { get; set; }
    }
}
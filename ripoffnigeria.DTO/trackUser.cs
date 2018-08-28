using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("trackUser")]
    public class trackUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string userName { get; set; }

        public int firmId { get; set; }
        [ForeignKey("firmId")]
        public virtual RipOffFirm RipOffFirm { get; set; }
        public bool hasLiked { get; set; }
    }
}

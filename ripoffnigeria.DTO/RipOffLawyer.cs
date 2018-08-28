using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ripoffnigeria.DTO
{
    [Table("RipOffLawyer")]
    public class RipOffLawyer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RipOffFirmId { get; set; }
        [ForeignKey("RipOffFirmId")]
        public virtual RipOffFirm RipOffFirm { get; set; }
        public string LawyerName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNo { get; set; }
        public string fax { get; set; }
        public string Email { get; set; }
        public string workHours { get; set; }
    }
}

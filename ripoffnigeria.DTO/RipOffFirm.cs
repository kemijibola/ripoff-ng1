using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace ripoffnigeria.DTO
{
    [Table("RipOffFirm")]
    public class RipOffFirm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirmName { get; set; }
        public string FirmHeader { get; set; }
        public string FirmDescription { get; set; }
        public string Website { get; set; }
        public bool isFeatured { get; set; }
        public string firmHolder { get; set; }
        public string faceBookId { get; set; }
        public string twitterHandle { get; set; }
        public string googleId { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public int profileView { get; set; }
        public int firmlike { get; set; }
        public DateTime registrationDate { get; set; }

        public virtual ICollection<FirmCategory> FirmCategories { get; set; }

        public virtual ICollection<FirmImage> FirmImages { get; set; }

        public virtual ICollection<RipOffLawyer> RipOffLawyer { get; set; }

    }
}
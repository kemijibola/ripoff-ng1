using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace ripoffnigeria.DTO
{
    [Table("Transaction")]
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ClientMeetingRequestId{ get; set; }
        [ForeignKey("ClientMeetingRequestId")]
        public virtual ClientMeetingRequest ClientMeetingRequest { get; set; }

        [Required]
        [StringLength(120)]
        public string AccountName { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public int BankId { get; set; }
        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }

        [Required]
        [JsonIgnore]
        public bool hasPaid { get; set; }

        [StringLength(120)]
        public string TransactionMessage { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
    }
}
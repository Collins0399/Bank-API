using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string AccountNumber { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string AccountHolder { get; set; }
        [Required]

        public int BankId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string IFSC { get; set; }
    }
}

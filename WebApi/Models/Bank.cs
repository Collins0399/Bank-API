using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public required string BankName { get; set; }
    }
}

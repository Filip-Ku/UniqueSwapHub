using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }

        public string HashedPassword { get; set; }

        public string email { get; set; }
        public string Role { get; set; }

        public string firstName { get; set; }
        public string? lastName { get; set; }
        public DateTimeOffset createdAt { get; set; }

        public ICollection<ItemDetails> Items { get; set; } = new List<ItemDetails>();
    }
}
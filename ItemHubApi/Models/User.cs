using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubApi.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int userId { get; set; }

        public required string name { get; set; }

        public required string hashedPassword { get; set; }

        public required string email { get; set; }

        public required string role { get; set; }

        public required string firstName { get; set; }

        public string? lastName { get; set; }

        public DateTime createdAt { get; set; }

        public ICollection<ItemDetails> items { get; set; } = new List<ItemDetails>();
    }
}

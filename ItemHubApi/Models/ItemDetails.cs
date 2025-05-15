using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubApi.Models
{
    [Table("items")]
    public class ItemDetails
    {
        [Key]
        public int itemId { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public required string name { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        public string? description { get; set; }

        [Required]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        [Required]
        public bool activated { get; set; } = false;

        public DateTime? reservationTime { get; set; }

        [Required]
        public DateTime expireDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od 0")]
        public float pricePerDay { get; set; }

        public bool available { get; set; }

        [Required(ErrorMessage = "Link do zdjęcia jest wymagany")]
        [Url(ErrorMessage = "Nieprawidłowy format URL")]
        public required string imageUrl { get; set; }
    }
}

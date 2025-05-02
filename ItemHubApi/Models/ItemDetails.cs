using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubApi.Models
{
    [Table("Items", Schema = "itemhub")]
    public class ItemDetails
    {
        [Key]
        public int itemId { get; set; }

        public int? userId { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public required string name { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        public string? description { get; set; }

        [Required]
        public DateTimeOffset createdAt { get; set; }

        [Required]
        public DateTimeOffset reservationTime { get; set; }

        [Required]
        public DateTimeOffset expireDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od 0")]
        public float pricePerDay { get; set; }

        public bool available { get; set; }

        [Required(ErrorMessage = "Link do zdjęcia jest wymagany")]
        [Url(ErrorMessage = "Nieprawidłowy format URL")]
        public required string imageUrl { get; set; }
    }
}

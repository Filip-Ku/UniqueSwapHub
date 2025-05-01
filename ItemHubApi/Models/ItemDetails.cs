using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubApi.Models
{
    [Table("Items", Schema = "itemhub")]
    public class ItemDetails
    {
        [Key]
        public int ItemId { get; set; }

        public int? UserId { get; set; } = null;

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        public string? Description { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset reservationTime { get; set; }

        [Required]
        public DateTimeOffset ExpireDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od 0")]
        public float PricePerDay { get; set; }

        public bool Available { get; set; }

        [Required(ErrorMessage = "Link do zdjęcia jest wymagany")]
        [Url(ErrorMessage = "Nieprawidłowy format URL")]
        public required string ImageUrl { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

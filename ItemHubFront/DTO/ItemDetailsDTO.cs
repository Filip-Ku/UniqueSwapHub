namespace ItemHubFront.DTO
{
    public class ItemDto
    {
        public int itemId { get; set; }
        public int userId { get; set; }
        public string name { get; set; } = "";
        public string? description { get; set; }
        public DateTime createdAt { get; set; }
        public bool activated { get; set; }
        public DateTime? reservationTime { get; set; }
        public DateTime expireDate { get; set; }
        public float pricePerDay { get; set; }
        public bool available { get; set; }
        public string imageUrl { get; set; } = "";
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubApi.Models
{
    [Table("reservations", Schema = "itemhub")]
    public class Reservation 
    {
        [Key]
        public int reservationId {get; set;}

        public int userId {get; set;}
        public int itemId {get; set;}

        public DateTimeOffset startDate {get; set; }
        public DateTimeOffset endDate {get; set; }
    }
}
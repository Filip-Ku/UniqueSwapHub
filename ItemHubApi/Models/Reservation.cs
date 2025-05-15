using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubApi.Models
{
    [Table("reservations")]
    public class Reservation 
    {
        [Key]
        public int reservationId {get; set;}

        public int userId {get; set;}
        public int itemId {get; set;}

        public DateTime startDate {get; set; }
        public DateTime endDate {get; set; }
    }
}
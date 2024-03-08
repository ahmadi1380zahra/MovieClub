using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Entities
{
    public class Rent
    {
        public int Id { get; set; }
        public DateTime RentAt { get; set; }
        public Decimal FilmRate { get; set; }
        public DateTime? GiveBackAt { get; set; }
        public Decimal FilmDailyPrice { get; set; }
        public Decimal FilmPenaltyPrice { get; set; }
        public Decimal Cost { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Film Film { get; set; }
        public int FilmId { get; set; }
    }
}

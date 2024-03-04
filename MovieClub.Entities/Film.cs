using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Director { get; set; }
        public int Stock { get; set; }
        public int MinAgeLimit { get; set; }
        public DateTime PublishYear { get; set; }
        public Decimal DailyPriceRent { get; set; }
        public Decimal PenaltyPriceRent { get; set; }
        public Decimal Duration { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
    }
}

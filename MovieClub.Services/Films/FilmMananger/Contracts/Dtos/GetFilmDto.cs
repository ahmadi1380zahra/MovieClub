using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Films.FilmMananger.Contracts.Dtos
{
    public class GetFilmDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateTime PublishYear { get; set; }

        public Decimal DailyPriceRent { get; set; }

        public string MinAgeLimit { get; set; }

        public Decimal PenaltyPriceRent { get; set; }

        public Decimal Duration { get; set; }

        public string Director { get; set; }

       
    }
}

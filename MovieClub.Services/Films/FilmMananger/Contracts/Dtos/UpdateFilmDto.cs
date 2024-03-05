using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Films.FilmMananger.Contracts.Dtos
{
    public class UpdateFilmDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int PublishYear { get; set; }
        [Required]
        public Decimal DailyPriceRent { get; set; }
        [Required]
        public int MinAgeLimit { get; set; }
        [Required]
        public Decimal PenaltyPriceRent { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        [MaxLength(50)]
        public string Director { get; set; }
        [Required]
        public int GenreId { get; set; }
    }
}

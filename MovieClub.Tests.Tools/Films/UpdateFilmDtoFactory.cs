using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Films
{
    public  class UpdateFilmDtoFactory
    {
        public static UpdateFilmDto Create(int genreId)
        {
            return new UpdateFilmDto
            {
                Name = "Update_dummy-film-name",
                Description = null,
                PublishYear = new DateTime(2000, 12, 1),
                DailyPriceRent = 100.12M,
                MinAgeLimit = 14,
                PenaltyPriceRent = 0.10M,
                Duration = 2,
                Director = "updated_jonney_depp",
                GenreId = genreId
            };
        }
    }
}

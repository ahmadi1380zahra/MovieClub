using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Films
{
    public static class AddFilmDtoFactory
    {
        public static AddFilmDto Create(int genreId)
        {
            return new AddFilmDto
            {
                Name = "dummy-film-name",
                Description = "dummy-film-Description",
                PublishYear = 1380,
                DailyPriceRent = 100.12M,
                MinAgeLimit = 14,
                PenaltyPriceRent = 0.10M,
                Duration = 2,
                Director = "jonney_depp",
                GenreId = genreId
            };
        }
    }
}

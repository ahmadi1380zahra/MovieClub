using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Films
{
    public class FilmBuilder
    {
        private readonly Film _film;
        public FilmBuilder()
        {
            _film = new Film
            {
                Name = "dummy-film-name",
                Description = "dummy-film-Description",
                PublishYear = 1998,
                DailyPriceRent = 100.12M,
                MinAgeLimit = 14,
                PenaltyPriceRent = 0.10M,
                Duration = 2,
                Director = "jonney_depp",
            };
        }

        public FilmBuilder WithGenreId(int genreId)
        {
            _film.GenreId = genreId;
            return this;
        }
        public FilmBuilder WithName(string name)
        {
            _film.Name = name;
            return this;
        }
        public FilmBuilder WithDirector(string director)
        {
            _film.Director = director;
            return this;
        }
        public FilmBuilder WithDuration(int duration)
        {
            _film.Duration = duration;
            return this;
        }
        public FilmBuilder WithDailyPriceRent(decimal dailyPriceRent)
        {
            _film.DailyPriceRent = dailyPriceRent;
            return this;
        }
        public FilmBuilder WithPenaltyPriceRent(decimal penaltyPriceRent)
        {
            _film.PenaltyPriceRent = penaltyPriceRent;
            return this;
        }
        public FilmBuilder WithPublishYear(int publishYear)
        {
            _film.PublishYear = publishYear;
            return this;
        }
        public FilmBuilder WithMinAgeLimit(int ageLimit)
        {
            _film.MinAgeLimit = ageLimit;
            return this;
        }
        public Film Build()
        {
            return _film;
        }
    }
}

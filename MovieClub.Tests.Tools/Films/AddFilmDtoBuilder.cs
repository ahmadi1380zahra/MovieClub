using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Films
{
    public class AddFilmDtoBuilder
    {
        private readonly AddFilmDto _dto;
        public AddFilmDtoBuilder(int genreId)
        {
            _dto= new AddFilmDto
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
        public AddFilmDtoBuilder WithName(string name)
        {
            _dto.Name = name;
            return this;
        }
        public AddFilmDtoBuilder WithDirector(string director)
        {
            _dto.Director = director;
            return this;
        }
        public AddFilmDtoBuilder WithDuration(int duration)
        {
            _dto.Duration = duration;
            return this;
        }
        public AddFilmDtoBuilder WithDailyPriceRent(decimal dailyPriceRent)
        {
            _dto.DailyPriceRent = dailyPriceRent;
            return this;
        }
        public AddFilmDtoBuilder WithPenaltyPriceRent(decimal penaltyPriceRent)
        {
            _dto.PenaltyPriceRent = penaltyPriceRent;
            return this;
        }
        public AddFilmDtoBuilder WithPublishYear(int publishYear)
        {
            _dto.PublishYear = publishYear;
            return this;
        }
        public AddFilmDtoBuilder WithMinAgeLimit(int ageLimit)
        {
            _dto.MinAgeLimit = ageLimit;
            return this;
        }
        public AddFilmDto Build()
        {
            return _dto;
        }
            
    }
}

using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Persistence.EF;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Services.Films.FilmMananger.Exceptions;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Films
{
    public class FilmUpdateTests:BusinessUnitTest
    {
        
        private readonly FilmService _sut;
        public FilmUpdateTests()
        {
            _sut = FilmServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Update_updates_a_film_peoperly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var film = new FilmBuilder().WithGenreId(genre.Id).Build();
            DbContext.Save(film);
            var dto = UpdateFilmDtoFactory.Create(genre.Id);

            await _sut.Update(film.Id, dto);

            var actual = ReadContext.Films.SingleOrDefault();
            actual.Name.Should().Be(dto.Name);
            actual.Description.Should().Be(dto.Description);
            actual.PublishYear.Should().Be(dto.PublishYear);
            actual.DailyPriceRent.Should().Be(dto.DailyPriceRent);
            actual.MinAgeLimit.Should().Be(dto.MinAgeLimit);
            actual.PenaltyPriceRent.Should().Be(dto.PenaltyPriceRent);
            actual.Duration.Should().Be(dto.Duration);
            actual.Director.Should().Be(dto.Director);
            actual.GenreId.Should().Be(dto.GenreId);
        }
        [Fact]
        public async Task Update_throw_GenreIsNotExistException()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var film = new FilmBuilder().WithGenreId(genre.Id).Build();
            DbContext.Save(film);
            var dummyUpdateGenreId = 12;
            var dto = UpdateFilmDtoFactory.Create(dummyUpdateGenreId);

            var actual = () => _sut.Update(film.Id, dto);

            await actual.Should().ThrowExactlyAsync<GenreIsNotExistException>();
        }
        [Fact]
        public async Task Update_throw_FilmIsNotExistException()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var dto = UpdateFilmDtoFactory.Create(genre.Id);
            var dummyFilmId = 13;

            var actual = () => _sut.Update(dummyFilmId, dto);

            await actual.Should().ThrowExactlyAsync<FilmIsNotExistException>();
        }
    }
}


using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Films;
using MovieClub.Services.Films.FilmMananger;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Services.Films.FilmMananger.Exceptions;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Films
{
    public class AddFilmTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly FilmService _sut;
        public AddFilmTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = FilmServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Add_adds_a_new_film_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dto = AddFilmDtoFactory.Create(genre.Id);

            await _sut.Add(dto);

            var actual = _readContext.Films.Single();
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
        public async Task Add_throw_GenreIsNotExistException()
        {
            var dummyGenreId = 4;
            var dto = AddFilmDtoFactory.Create(dummyGenreId);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<GenreIsNotExistException>();
        }
        [Fact]
        public async Task Add_throw_DailyPriceRentShouldBeMoreThanZeroException()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dummyDailyPriceRent = -10;
            var dto = new AddFilmDtoBuilder(genre.Id).WithDailyPriceRent(dummyDailyPriceRent).Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<DailyPriceRentShouldBeMoreThanZeroException>();
        }
        [Fact]
        public async Task Add_throw_DailyPenaltyRentShouldBeMoreThanZeroException()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dummyPenaltyPriceRent = 0;
            var dto = new AddFilmDtoBuilder(genre.Id).WithPenaltyPriceRent(dummyPenaltyPriceRent).Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<DailyPenaltyRentShouldBeMoreThanZeroException>();
        }
        [Fact]
        public async Task Add_throw_PublishYearShouldBeMoreThanZeroException()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dummyPublishYear = 0;
            var dto = new AddFilmDtoBuilder(genre.Id).WithPublishYear(dummyPublishYear).Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<PublishYearShouldBeMoreThanZeroException>();
        }
        [Fact]
        public async Task Add_throw_MinAgeLimitShouldBeMoreThanZeroException()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dummyAgeLimit= -18;
            var dto = new AddFilmDtoBuilder(genre.Id).WithMinAgeLimit(dummyAgeLimit).Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<MinAgeLimitShouldBeMoreThanZeroException>();
        }
        [Fact]
        public async Task Add_throw_DurationShouldBeMoreThanZeroException()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dummyduration = -1;
            var dto = new AddFilmDtoBuilder(genre.Id).WithDuration(dummyduration).Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<DurationShouldBeMoreThanZeroException>();
        }
    }
}

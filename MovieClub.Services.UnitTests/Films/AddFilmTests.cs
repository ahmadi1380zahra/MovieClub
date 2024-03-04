using DoctorAppointment.Persistance.EF;
using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Films;
using MovieClub.Services.Films.FilmMananger;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
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
    }
}

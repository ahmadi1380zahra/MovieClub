using FluentAssertions;
using MovieClub.Persistence.EF;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Films
{
    public class FilmGetTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly FilmService _sut;
        public FilmGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = FilmServiceFactory.Create(_context);
        }
        [Fact]
        public async Task GetAll_gets_all_films()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var film1 = new FilmBuilder().WithGenreId(genre.Id).Build();
            _context.Save(film1);
            var film2 = new FilmBuilder().WithGenreId(genre.Id).Build();
            _context.Save(film2);
            var film3 = new FilmBuilder().WithGenreId(genre.Id).Build();
            _context.Save(film3);
            var dto = GetFilmFilterDtoFactory.Create();

            var films = await _sut.GetAll(dto);

            films.Count.Should().Be(3);
        }
        [Fact]
        public async Task GetAll_gets_films_according_to_filter()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var film1 = new FilmBuilder().WithGenreId(genre.Id).WithName("film").Build();
            _context.Save(film1);
            var film2 = new FilmBuilder().WithGenreId(genre.Id).WithName("film").Build();
            _context.Save(film2);
            var film3 = new FilmBuilder().WithGenreId(genre.Id).WithName("movie").Build();
            _context.Save(film3);
            var nameFilter = "film";
            var dto = GetFilmFilterDtoFactory.Create(nameFilter);

            var films = await _sut.GetAll(dto);

            films.Count.Should().Be(2);
        }
        [Fact]
        public async Task Get_gets_a_film_and_check_for_valid_data()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var film1 = new FilmBuilder().WithGenreId(genre.Id).Build();
            _context.Save(film1);
            var dto = GetFilmFilterDtoFactory.Create();

            var genres = await _sut.GetAll(dto);

            var actual = genres.Single();
            
            actual.Name.Should().Be(film1.Name);
            actual.Description.Should().Be(film1.Description);
            actual.PublishYear.Should().Be(film1.PublishYear);
            actual.DailyPriceRent.Should().Be(film1.DailyPriceRent);
            actual.MinAgeLimit.Should().Be("+ "+film1.MinAgeLimit.ToString());
            actual.PenaltyPriceRent.Should().Be(film1.PenaltyPriceRent);
            actual.Duration.Should().Be(film1.Duration);
            actual.Director.Should().Be(film1.Director);
       

        }
    }
}

using FluentAssertions;
using MovieClub.Persistence.EF;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Genres
{
    public class GenreGetTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly GenreService _sut;
        public GenreGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Get_gets_all_genres()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var genre1 = new GenreBuilder().Build();
            _context.Save(genre1);
            var genre2 = new GenreBuilder().Build();
            _context.Save(genre2);
            var dto = GetGenreFilterDtoFactory.Create(null);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(3);
        }
        [Fact]
        public async Task Get_gets_the_genres_according_to_filter()
        {
            var genre = new GenreBuilder().WithTitle("scary").Build();
            _context.Save(genre);
            var genre1 = new GenreBuilder().WithTitle("fantastic").Build();
            _context.Save(genre1);
            var genre2 = new GenreBuilder().WithTitle("foolly").Build();
            _context.Save(genre2);
            var filter = "s";
            var dto = GetGenreFilterDtoFactory.Create(filter);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(2);
        }
        [Fact]
        public async Task Get_gets_a_genre_and_check_for_valid_data()
        {
            var genre = new GenreBuilder().WithTitle("scary").Build();
            _context.Save(genre);
            var dto = GetGenreFilterDtoFactory.Create(null);

            var genres = await _sut.GetAll(dto);

            var actual = genres.Single();
            actual.Id.Should().Be(genre.Id);
            actual.Title.Should().Be(genre.Title);
            actual.Rate.Should().Be(genre.Rate);

        }
    }
}

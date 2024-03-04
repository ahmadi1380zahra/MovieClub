using FluentAssertions;
using MovieClub.Persistence.EF;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Genres
{
    public class GenreDeleteTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly GenreService _sut;
        public GenreDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Delete_deletes_a_genre_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);

            await _sut.Delete(genre.Id);

            var actual = _readContext.Genres.FirstOrDefault(_ => _.Id == genre.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_GenreIsNotExistException()
        {
            var dummyId = 11;

            var actual = () => _sut.Delete(dummyId);

            await actual.Should().ThrowExactlyAsync<GenreIsNotExistException>();
        }
        [Fact]
        public async Task Delete_throws_GenreCantBeDeletedItHasFilmsException()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var film = new FilmBuilder().WithGenreId(genre.Id).Build();
            _context.Save(film);

            var actual = () => _sut.Delete(genre.Id);

            await actual.Should().ThrowExactlyAsync<GenreCantBeDeletedItHasFilmsException>();
        }
    }
}

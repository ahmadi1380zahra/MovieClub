using FluentAssertions;
using MovieClub.Persistence.EF;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Exceptions;
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
    public class FilmDeleteTests:BusinessUnitTest
    {
        private readonly FilmService _sut;
        public FilmDeleteTests()
        {
            _sut = FilmServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Delete_deletes_a_film_properly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var film = new FilmBuilder().WithGenreId(genre.Id).Build();
            DbContext.Save(film);

            await _sut.Delete(film.Id);

            var actual = ReadContext.Films.FirstOrDefault(_ => _.Id == film.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throw_FilmIsNotExistException()
        {
            var dummyId = 12;

            var actual = () => _sut.Delete(dummyId);

            await actual.Should().ThrowExactlyAsync<FilmIsNotExistException>();
        }
    }
}

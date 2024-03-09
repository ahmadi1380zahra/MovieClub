using FluentAssertions;
using Moq;
using MovieClub.Contracts.Interfaces;
using MovieClub.Persistence.EF;
using MovieClub.Services.Genres.GenreManagers;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Genres.GenreMananger
{
    public class GenreManangerDeleteTests : BusinessUnitTest
    {

        private readonly GenreManangerService _sut;
        public GenreManangerDeleteTests()
        {
            _sut = GenreManangerServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Delete_deletes_a_genre_properly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);

            await _sut.Delete(genre.Id);

            var actual = ReadContext.Genres.FirstOrDefault(_ => _.Id == genre.Id);
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
            DbContext.Save(genre);
            var film = new FilmBuilder().WithGenreId(genre.Id).Build();
            DbContext.Save(film);

            var actual = () => _sut.Delete(genre.Id);

            await actual.Should().ThrowExactlyAsync<GenreCantBeDeletedItHasFilmsException>();
        }
        [Fact]
        public async Task Delete_deletes_a_genre_properly_mock()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var mockRepository = new Mock<GenreMananagerRepository>();
            var mockUnitOfwork = new Mock<UnitOfWork>();
            var sut = new GenreManangerAppService(mockRepository.Object, mockUnitOfwork.Object);
            mockRepository.Setup(_=>_.Find(It.Is<int>(_=>_==genre.Id))).ReturnsAsync(genre);
            mockRepository.Setup(_ => _.IsExistFilmForThisGenre(It.Is<int>(_ => _ == genre.Id))).ReturnsAsync(false);

            await sut.Delete(genre.Id);

            mockRepository.Verify(_ => _.Delete(It.Is<Entities.Genre>
                (_ => _.Id == genre.Id && _.Title == genre.Title)), Times.Once);
            mockUnitOfwork.Verify(_ => _.Complete(), Times.Once);
        }
    }
}

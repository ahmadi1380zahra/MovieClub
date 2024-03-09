using FluentAssertions;
using Moq;
using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Persistence.EF;
using MovieClub.Services.Genres.GenreManagers;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Genres.GenreMananger
{
    public class GenreManangerUpdateTests : BusinessUnitTest
    {

        private readonly GenreManangerService _sut;
        public GenreManangerUpdateTests()
        {

            _sut = GenreManangerServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Update_updates_a_genre_properly()
        {
            var title = "dummyTitle";
            var genre = new GenreBuilder().WithTitle(title).Build();
            DbContext.Save(genre);
            var updatedTitle = "updatedTitle";
            var dto = UpdateGenreManangerDtoFactory.Create(updatedTitle);

            await _sut.Update(genre.Id, dto);

            var actual = ReadContext.Genres.First();
            actual.Title.Should().Be(dto.Title);
        }
        [Fact]
        public async Task Update_throws_GenreIsNotExistException()
        {
            var dummyId = 15;
            var dto = UpdateGenreManangerDtoFactory.Create();

            var actual = () => _sut.Update(dummyId, dto);

            await actual.Should().ThrowExactlyAsync<GenreIsNotExistException>();
        }
        [Fact]
        public async Task Update_updates_a_genre_properly_mock()
        {
            var title = "dummyTitle";
            var genre = new GenreBuilder().WithTitle(title).Build();
            DbContext.Save(genre);
            var updatedTitle = "updatedTitle";
            var dto = UpdateGenreManangerDtoFactory.Create(updatedTitle);
            var mockRepository = new Mock<GenreMananagerRepository>();
            var mockUnitOfwork = new Mock<UnitOfWork>();
            var sut = new GenreManangerAppService(mockRepository.Object, mockUnitOfwork.Object);
            mockRepository.Setup(_ => _.Find(It.Is<int>(_ => _ == genre.Id))).ReturnsAsync(genre);

            await sut.Update(genre.Id, dto);

            mockRepository.Verify(_ => _.Update(It.Is<Entities.Genre>
                                                (_ => _.Id == genre.Id &&
                                                 _.Title == dto.Title)), Times.Once);
            mockUnitOfwork.Verify(_ => _.Complete(), Times.Once);

        }
    }
}

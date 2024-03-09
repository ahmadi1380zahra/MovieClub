using MovieClub.Persistence.EF;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieClub.Services.Genres.GenreManagers;

using MovieClub.Persistence.EF.Genres;

using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using FluentAssertions;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using Microsoft.EntityFrameworkCore;
using MovieClub.Tests.Tools.Genres;
using Moq;
using MovieClub.Contracts.Interfaces;

namespace MovieClub.Services.UnitTests.Genres.GenreMananger
{
    public class GenreManangerAddTests : BusinessUnitTest
    {

        private readonly GenreManangerService _sut;
        public GenreManangerAddTests()
        {
            _sut = GenreManangerServiceFactory.Create(SetupContext);
        }

        [Fact]
        public async Task Add_adds_a_new_genre_properly()
        {
            var dto = AddGenreManangerDtoFactory.Create();

            await _sut.Add(dto);

            var actual = ReadContext.Genres.First();
            actual.Title.Should().Be(dto.Title);
        }
        [Fact]
        public async Task Add_adds_a_new_genre_properly_Moq()
        {
            var dto = AddGenreManangerDtoFactory.Create();
            var repositoryMock = new Mock<GenreMananagerRepository>();
            var unitOfWorkMock = new Mock<UnitOfWork>();
            var sut = new GenreManangerAppService(repositoryMock.Object, unitOfWorkMock.Object);

            await sut.Add(dto);

            repositoryMock.Verify(_ => _.Add(It.Is<Entities.Genre>(_ => _.Title == dto.Title)),Times.Once);
            unitOfWorkMock.Verify(_ => _.Complete(), Times.Once);
        }
    }
}

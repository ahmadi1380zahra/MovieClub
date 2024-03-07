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

namespace MovieClub.Services.UnitTests.Genres.GenreMananger
{
    public class GenreManangerAddTests:BusinessUnitTest
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
    }
}

using FluentAssertions;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Genres;
using MovieClub.Services.Genres.Genre;
using MovieClub.Services.Genres.Genre.Contracts;
using MovieClub.Services.Genres.Genre.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Genres.Genre
{
    public class GenreServiceGetTests:BusinessUnitTest
    {
        private readonly GenreService _sut;
        public GenreServiceGetTests()
        {
       _sut = GenreServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Get_gets_all_genres()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var genre2 = new GenreBuilder().Build();
            DbContext.Save(genre2);
            var dto = GetGenreFilterDtoFactory.Create();

            var genres = await _sut.GetAll(dto);

            genres.Count().Should().Be(2);
            var actual = genres.FirstOrDefault();
            actual.Id.Should().Be(genre.Id);
            actual.Title.Should().Be(genre.Title);
        }
        [Fact]
        public async Task Get_gets_the_genres_by_nameFilter()
        {
            var genre = new GenreBuilder().WithTitle("scary").Build();
            DbContext.Save(genre);
            var genre1 = new GenreBuilder().WithTitle("fantastic").Build();
            DbContext.Save(genre1);
            var genre2 = new GenreBuilder().WithTitle("foolly").Build();
            DbContext.Save(genre2);
            var filter = "s";
            var dto = GetGenreFilterDtoFactory.Create(filter);

            var genres = await _sut.GetAll(dto);

            genres.Count().Should().Be(2);
            var actual = genres[1];
            actual.Id.Should().Be(genre1.Id);
            actual.Title.Should().Be(genre1.Title);
        }
        [Fact]
        public async Task Get_gets_a_genre_and_check_for_valid_data()
        {
            var genre = new GenreBuilder().WithTitle("scary").Build();
            DbContext.Save(genre);
            var dto = GetGenreFilterDtoFactory.Create(null);

            var genres = await _sut.GetAll(dto);

            var actual = genres.Single();
            actual.Id.Should().Be(genre.Id);
            actual.Title.Should().Be(genre.Title);
        }
    }
}

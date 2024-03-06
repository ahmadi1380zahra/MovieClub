using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Persistence.EF;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Genres.GenreMananger
{
    public class GenreManangerUpdateTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly GenreManangerService _sut;
        public GenreManangerUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreManangerServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Update_updates_a_genre_properly()
        {
            var title = "dummyTitle";
            var genre = new GenreBuilder().WithTitle(title).Build();
            _context.Save(genre);
            var updatedTitle = "updatedTitle";
            var dto = UpdateGenreManangerDtoFactory.Create(updatedTitle);

            await _sut.Update(genre.Id, dto);

            var actual = _readContext.Genres.First();
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
    }
}

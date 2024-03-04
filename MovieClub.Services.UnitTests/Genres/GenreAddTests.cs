using MovieClub.Persistence.EF;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieClub.Services.Genres.GenreManagers;

using MovieClub.Persistence.EF.Genres;
using DoctorAppointment.Persistance.EF;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using FluentAssertions;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using Microsoft.EntityFrameworkCore;
using MovieClub.Tests.Tools.Genres;

namespace MovieClub.Services.UnitTests.Genres
{
    public class GenreAddTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly GenreService _sut;
        public GenreAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreServiceFactory.Create(_context);
        }

        [Fact]
        public async Task Add_adds_a_new_genre_properly()
        {
            var dto = AddGenreDtoFactory.Create();

            await _sut.Add(dto);

            var actual = _readContext.Genres.First();
            actual.Title.Should().Be(dto.Title);
        }
    }
}

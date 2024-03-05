using DoctorAppointment.Persistance.EF;
using FluentAssertions;
using Moq;
using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Users;
using MovieClub.Services.Users.UserMananger;
using MovieClub.Services.Users.UserMananger.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClub.Tests.Tools.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Users
{
    public class UserServiceAddTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly UserMananengerService _sut;
        private readonly DateTime _fakeTime;
        public UserServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _fakeTime = new DateTime(2018, 2, 4);
            _sut = UserMananengerServiceFactory.Create(_context, _fakeTime);
        }
        [Fact]
        public async Task Add_adds_a_new_user_properly()
        {
            var dto = AddUserManangerDtoFactory.Create();

            await _sut.Add(dto);

            var actual = _readContext.Users.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.PhoneNumber.Should().Be(dto.PhoneNumber);
            actual.Age.Should().Be(dto.Age);
            actual.Gender.Should().Be(dto.Gender);
            actual.Address.Should().Be(dto.Address);
            actual.CreateAt.Should().Be(_fakeTime);

        }

    }
}

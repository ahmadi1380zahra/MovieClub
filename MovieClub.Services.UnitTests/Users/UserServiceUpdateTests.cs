using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Persistence.EF;
using MovieClub.Services.Users.UserMananger.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using MovieClub.Services.Users.UserMananger.Exceptions;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClub.Tests.Tools.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.UnitTests.Users
{
    public class UserServiceUpdateTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly UserMananengerService _sut;
        public UserServiceUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();

            _sut = UserMananengerServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Update_updates_a_user_properly()
        {
            var user = new UserBuilder().Build();
            _context.Save(user);
            var dto = UpdateUserManangerDtoFactory.Create();

            await _sut.Update(user.Id, dto);

            var actual = _context.Users.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.PhoneNumber.Should().Be(dto.PhoneNumber);
            actual.Age.Should().Be(dto.Age);
            actual.Gender.Should().Be(dto.Gender);
            actual.Address.Should().Be(dto.Address);
          
        }
        [Fact]
        public async Task Update_throws_UserIsNotExistException()
        {
            var dto = UpdateUserManangerDtoFactory.Create();
            var dummyId = 10;

            var actual=()=> _sut.Update(dummyId, dto);

           await actual.Should().ThrowExactlyAsync<UserIsNotExistException>();
        }
    }
}

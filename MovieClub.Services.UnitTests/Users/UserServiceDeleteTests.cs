using FluentAssertions;
using MovieClub.Persistence.EF;
using MovieClub.Services.Users.UserMananger.Contracts;
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
    public class UserServiceDeleteTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly UserMananengerService _sut;
        private readonly DateTime _fakeTime;
        public UserServiceDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _fakeTime = new DateTime(2018, 2, 4);
            _sut = UserMananengerServiceFactory.Create(_context, _fakeTime);
        }
        [Fact]
        public async Task Delete_deletes_a_user_properly()
        {
            var user = new UserBuilder().Build();
            _context.Save(user);

            await _sut.Delete(user.Id);

            var actual = _readContext.Users.FirstOrDefault(_ => _.Id == user.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_UserIsNotExistException()
        {
            var dummyUserId = 12;

            var actual=()=> _sut.Delete(dummyUserId);

         await   actual.Should().ThrowExactlyAsync<UserIsNotExistException>();
        }
    }
}

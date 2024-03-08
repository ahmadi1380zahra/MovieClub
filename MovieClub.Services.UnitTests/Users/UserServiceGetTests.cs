using FluentAssertions;
using MovieClub.Services.Users.UserMananger.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClub.Tests.Tools.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace MovieClub.Services.UnitTests.Users
{
    public class UserServiceGetTests : BusinessUnitTest
    {
        private readonly UserMananengerService _sut;
        public UserServiceGetTests()
        {
            _sut = UserMananengerServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task GetAll_gets_all_users()
        {
            var user = new UserBuilder().Build();
            DbContext.Save(user);
            var user1 = new UserBuilder().Build();
            DbContext.Save(user1);
            var dto = GetUserManangerFilterDtoFactory.Create(null);

            var users = await _sut.GetAll(dto);

            users.Count().Should().Be(2);
        }
        [Fact]
        public async Task Get_gets_a_user_and_check_for_valid_data()
        {
            var user = new UserBuilder().Build();
            DbContext.Save(user);
            var dto = GetUserManangerFilterDtoFactory.Create(null);

            var users = await _sut.GetAll(dto);

            var actual = users.Single();
            actual.Id.Should().Be(user.Id);
            actual.FullName.Should().Be(user.FirstName + user.LastName);
            actual.Age.Should().Be((DateTime.UtcNow - user.Age).Days / 365);
        }
        [Fact]
        public async Task GetAll_get_user_by_name_filter()
        {
            var user0 = new UserBuilder().WithFirstName("zahra").WithLastName("ahmadi").Build();
            DbContext.Save(user0);
            var user1 = new UserBuilder().WithFirstName("sara").WithLastName("haqiqat").Build();
            DbContext.Save(user1);
            var filter = "s";
            var dto = GetUserManangerFilterDtoFactory.Create(filter);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(1);
            var user = actual.FirstOrDefault(_=>_.Id==user1.Id);
            user.Id.Should().Be(user1.Id);
            user.FullName.Should().Be(user1.FirstName + user1.LastName);
            user.Age.Should().Be((DateTime.UtcNow - user1.Age).Days / 365);
        }
    }
}

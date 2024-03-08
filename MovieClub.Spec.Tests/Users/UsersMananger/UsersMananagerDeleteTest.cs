using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Services.Users.UserMananger.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using MovieClub.Tests.Tools.Users;
using Xunit;

namespace MovieClub.Spec.Tests.Users.UsersMananger;

[Scenario("حذف کردن کاربر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "کاربری که در فهرست کاربرها وجود دارد را حذف کنم ",
    InOrderTo = "فقط کاربرهای معتبر را در فهرست کاربرها داشته باشم")]

public class UsersMananagerDeleteTest : BusinessIntegrationTest
{
    private readonly UserMananengerService _sut;
    private User _user;
    public UsersMananagerDeleteTest()
    {
        _sut = UserMananengerServiceFactory.Create(SetupContext);
    }
    [Given("فقط یک کاربر با نام زهرا و نام خانوادگی احمدی و تاریخ تولد 1380/2/10 وجنسیت خانم وادرس شیراز - یاوران و شماره تلفن 09027651510 وجود دارد.")]
    private void Given()
    {
        _user = new UserBuilder()
            .WithFirstName("زهرا")
            .WithLastName("احمدی")
            .WithAge(new DateTime(1380, 2, 10))
            .WithGender(Gender.Female)
            .WithPhone("09027651510")
            .WithAddress("shz yavaran...")
            .Build();
       Save(_user);
    }

    [When("من کاربر مذکور را حذف میکنم.")]
    private async Task When()
    {
        await _sut.Delete(_user.Id);

    }

    [Then("باید در فهرست کاربر ها هیچ کاربری وجود نداشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Users.FirstOrDefault(_ => _.Id == _user.Id);
        actual.Should().BeNull();
    }


    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _ => Given(),
            _ => When().Wait(),
            _ => Then());
    }
}
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

[Scenario("ثبت کردن کاربر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "کاربر جدید به  فهرست کاربرها اضافه کنم",
    InOrderTo = "فیلم ها را به کاربران اجاره دهم")]

public class RentMananagerAddTest : BusinessIntegrationTest
{
    private readonly UserMananengerService _sut;
    private readonly DateTime _fakeTime;
    public RentMananagerAddTest()
    {
        _fakeTime = new DateTime(2018, 2, 4);
        _sut = UserMananengerServiceFactory.Create(SetupContext,_fakeTime);
    }
    [Given("کاربری در فهرست کاربرها وجود ندارد")]
    private void Given()
    {

    }

    [When("من کاربری با نام زهرا و نام خانوادگی احمدی و تاریخ تولد 1380/2/10 وجنسیت خانم وادرس شیراز - یاوران و شماره تلفن 09027651510 ثبت میکنم.")]
    private async Task When()
    {
        var dto = new AddUserManangerDto
        {
            FirstName = "زهرا",
            LastName = "احمدی",
            Age = new DateTime(1380, 2, 10),
            Gender = Gender.Female,
            Address = "shz yavaran...",
            PhoneNumber = "09027651510"
        };

        await _sut.Add(dto);

    }

    [Then("باید در فهرست کاربرها فقط یک کاربر با نام زهرا و نام خانوادگی احمدی و تاریخ تولد 1380/2/10 وجنسیت خانم وادرس شیراز - یاوران و شماره تلفن 09027651510 وجود داشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Users.Single();
        actual.FirstName.Should().Be("زهرا");
        actual.LastName.Should().Be("احمدی");
        actual.PhoneNumber.Should().Be("09027651510");
        actual.Age.Should().Be(new DateTime(1380, 2, 10));
        actual.Gender.Should().Be(Gender.Female);
        actual.Address.Should().Be("shz yavaran...");
        actual.CreateAt.Should().Be(_fakeTime);

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
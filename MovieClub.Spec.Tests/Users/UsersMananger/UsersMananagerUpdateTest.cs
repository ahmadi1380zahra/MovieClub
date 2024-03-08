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

[Scenario("ویرایش کردن کاربر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = " کاربری که در فهرست کاربرها وجود دارد را ویرایش کنم",
    InOrderTo = "بتوانم کاربرهای معتبری در فهرست کاربرها داشته باشم")]

public class UsersMananagerUpdateTest : BusinessIntegrationTest
{
    private readonly UserMananengerService _sut;
    private User _user;
    public UsersMananagerUpdateTest()
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

    [When("من کاربر مذکور را به  نام ارتمیس  و نام خانوادگی احمدی و تاریخ تولد 1384/2/10 وجنسیت خانم وادرس شیراز - یاوران و شماره تلفن 09027651010  ویرایش میکنم.")]
    private async Task When()
    {
        var dto = new UpdateUserManangerDto
        {
            FirstName = "ارتمیس",
            LastName = "احمدی",
            Age = new DateTime(1384, 2, 10),
            Gender = Gender.Female,
            Address = "شیراز - یاوران",
            PhoneNumber = "09027651010"
        };

        await _sut.Update(_user.Id, dto);

    }

    [Then("باید در فهرست کاربران فقط یک کاربر به  نام ارتمیس  و نام خانوادگی احمدی و تاریخ تولد 1384/2/10 وجنسیت خانم وادرس شیراز - یاوران و شماره تلفن 09027651010  وجود داشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Users.Single();
        actual.FirstName.Should().Be("ارتمیس");
        actual.LastName.Should().Be("احمدی");
        actual.PhoneNumber.Should().Be("09027651010");
        actual.Age.Should().Be(new DateTime(1384, 2, 10));
        actual.Gender.Should().Be(Gender.Female);
        actual.Address.Should().Be("شیراز - یاوران");
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
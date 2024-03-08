using FluentAssertions;
using Moq;
using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Films;
using MovieClub.Persistence.EF.Rents;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Services.Rents.RentManangers;
using MovieClub.Services.Rents.RentManangers.Contracts;
using MovieClub.Services.Rents.RentManangers.Contracts.Dtos;
using MovieClub.Services.Users.UserMananger.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using MovieClub.Tests.Tools.Users;
using System;
using Xunit;

namespace MovieClub.Spec.Tests.Rents.RentMananger;

[Scenario("اجاره دادن فیلم به کاربر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "به کاربری که در فهرست کاربرها وجود دارد فیلمی که در فهرست فیلم ها وجود دارد را اجاره دهم",
    InOrderTo = "کسب درامد کنم")]

public class RentMananagerAddTest : BusinessIntegrationTest
{
    private readonly RentManangerService _sut;
    private User _user;
    private Genre _genre;
    private Film _film;
    private readonly DateTime _fakeTime;
    //private Film _film2;
    public RentMananagerAddTest()
    {
        _fakeTime = new DateTime(2018, 2, 4);
        var mockDateTimeService = new Mock<DateTimeService>();
        mockDateTimeService.Setup(_ => _.UtcNow()).Returns(_fakeTime);
        _sut = new RentManangerAppService(new EFRentManangerRepository(SetupContext),
                   new EFUnitOfWork(SetupContext)
                   ,new EFFilmRepository(SetupContext)
                   , mockDateTimeService.Object
                   );
    }
    [Given("فقط یک کاربر با نام زهرا و نام خانوادگی احمدی و تاریخ تولد 1380/2/10 وجنسیت خانم وادرس شیراز - یاوران و شماره تلفن 09027651510وجود دارد.")]
    [And("یک ژانر به اسم جنگی در فهرست ژانرها وجود دارد")]
    [And("فیلم با عنوان گات و کارگردان نولان وحداقل سن  18 وسال انتشار 1380 و قیمت روزانه 2000  و قیمت جریمه 10درصد   ")]
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

        _genre = new GenreBuilder()
       .WithTitle("جنگی")
       .Build();
        DbContext.Save(_genre);

        _film = new FilmBuilder()
       .WithName("گات")
       .WithDirector("نولان")
       .WithMinAgeLimit(18)
       .WithPublishYear(1380)
       .WithDailyPriceRent(200)
       .WithPenaltyPriceRent(0.10M)
       .WithDuration(160)
       .WithGenreId(_genre.Id)
       .Build();
        DbContext.Save(_film);  
    }

    [When("کاربر با نام زهرا دو فیلم مذکور را اجاره میبرد  ")]
    private async Task When()
    {
        var dto = new AddRentManangerDto
        {
            UserId = _user.Id,
            FilmId = _film.Id,
        };

        await _sut.Add(dto);

    }

    [Then("باید در فهرست اجاره ها فیلم مذکور که توسط زهرا اجاره شده  قابل مشاهده باشد.")]
    private void Then()
    {
        var actual = ReadContext.Rents.Single();
        actual.UserId.Should().Be(_user.Id);
        actual.FilmId.Should().Be(_film.Id);
        actual.RentAt.Should().Be(_fakeTime);
        actual.FilmDailyPrice.Should().Be(200);
        actual.FilmPenaltyPrice.Should().Be(0.10M);

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
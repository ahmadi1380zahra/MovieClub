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
using MovieClub.Tests.Tools.Rents;
using MovieClub.Tests.Tools.Users;
using System;
using Xunit;

namespace MovieClub.Spec.Tests.Rents.RentMananger;

[Scenario("ویرایش کردن اطلاعات اجاره بر اساس نرخ جریمه به علت تاخیر بیش از 7 روز")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "میخواهم  اطلاعات اجاره را ویرایش کنم",
    InOrderTo = "تاریخ تحویل فیلم و امتیاز کاربر را مدیریت کنم")]

public class RentManangerUpdateTestWithPenaltyPrice : BusinessIntegrationTest
{
    private readonly RentManangerService _sut;
    private readonly DateTime _fakeDate;
    private User _user;
    private Genre _genre;
    private Film _film;
    private Rent _rent;

    public RentManangerUpdateTestWithPenaltyPrice()
    {
        _fakeDate = new DateTime(2024 , 3 , 20);
        _sut = RentManangerServiceFactory.Create(SetupContext,_fakeDate);
    }
    [Given("فقط یک کاربر با نام زهرا وجود دارد.")]
    [And("یک ژانر به اسم جنگی در فهرست ژانرها وجود دارد")]
    [And("یک فیلم  با عنوان گات در فهرست فیلم ها وجود دارد  ")]
    [And(" زهرا این فیلم را در تاریخ  2024/3/10 اجاره کرده است ")]
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
       .WithDailyPriceRent(20000)
       .WithPenaltyPriceRent(0.10M)
       .WithDuration(160)
       .WithGenreId(_genre.Id)
       .Build();
        DbContext.Save(_film);

        _rent = new RentBuilder(_film.Id, _user.Id)
            .WithRentAt(new DateTime(2024,3,10))
            .WithFilmPenaltyPrice(_film.PenaltyPriceRent)
            .WithFilmDailyPrice(_film.DailyPriceRent)
            .Build();
        DbContext.Save(_rent);
    }

    [When("کاربر با نام زهرا فیلم مذکور را در تاریخ2024/3/20 برمیگرداند .  ")]
    private async Task When()
    {
        var dto = new UpdateRentManangerDto
        {
            FilmRate = 4,
           
        };

        await _sut.Update(_rent.Id,dto);

    }

    [Then("باید در فهرست اجاره ها برای زهرا  تاریخ بازگشت و امتیازو هزینه کل بر اساس نرخ جریمه  ثبت شده باشد.")]
    private void Then()
    {
        var actual = ReadContext.Rents.Single();
        actual.GiveBackAt.Should().Be(_fakeDate);
        actual.Cost.Should().Be(206000);
        actual.FilmRate.Should().Be(4);
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
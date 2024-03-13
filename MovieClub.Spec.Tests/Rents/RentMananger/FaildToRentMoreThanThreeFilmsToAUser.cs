using FluentAssertions;
using Moq;
using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Persistence.EF.Films;
using MovieClub.Persistence.EF.Rents;
using MovieClub.Persistence.EF;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Services.Rents.RentManangers;
using MovieClub.Services.Rents.RentManangers.Contracts;
using MovieClub.Services.Rents.RentManangers.Contracts.Dtos;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using MovieClub.Tests.Tools.Users;
using Xunit;
using MovieClub.Tests.Tools.Rents;
using MovieClub.Services.Rents.RentManangers.Exceptions;

namespace MovieClub.Spec.Tests.Rents.RentMananger;

[Scenario("عدم اجاره دادن بیش از 3 فیلم به یک کاربر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "فیلم جدید اضافه کنم ",
    InOrderTo = "فیلم ها را اجاره دهم")]

public class FaildToRentMoreThanThreeFilmsToAUser : BusinessIntegrationTest
{

    private readonly RentManangerService _sut;
    private readonly DateTime _fakeTime;
    private Func<Task>? _actual;
    private Genre _genre;
    private Genre _genre2;
    private Film _film;
    private Film _film1;
    private Film _film2;
    private Film _film3;
    private User _user;
    public FaildToRentMoreThanThreeFilmsToAUser()
    {
        _fakeTime = new DateTime(2018, 2, 4);
        _sut = RentManangerServiceFactory.Create(SetupContext, _fakeTime);
    }
    [Given(" که زهرا این فیلم را کرایه کرده است. با عنوان گات  در فهرست فیلم ها وجود دارد")]
    [And(" فیلم با عنوان گیم اف ترونز  در فهرست فیلم ها وجود داردکه زهرا این فیلم را کرایه کرده است.\r\n")]
    [And("فیلم با عنوان game of thrones  در فهرست فیلم ها وجود داردکه زهرا این فیلم را کرایه کرده است.\r\n ")]
    [And("فیلم با عنوان got  در فهرست فیلم ها وجود دارد")]
    [And("یک ژانر به اسم جنگی در فهرست ژانرها وجود دارد")]
    [And("یک ژانر به اسم درام در فهرست ژانرها وجود دارد")]
    [And("فقط یک کاربر با نام زهرا و نام خانوادگی احمدی و تاریخ تولد 1380/2/10 وجنسیت خانم وادرس شیراز - یاوران و شماره تلفن 09027651510وجود دارد.")]
    private void Given()
    {
        _genre = new GenreBuilder()
       .WithTitle("جنگی")
       .Build();
        DbContext.Save(_genre);
        _genre2 = new GenreBuilder()
        .WithTitle("درام")
        .Build();
        DbContext.Save(_genre2);
        _user = new UserBuilder()
    .WithFirstName("زهرا")
     .WithLastName("احمدی")
   .WithAge(new DateTime(1380, 2, 10))
      .WithGender(Gender.Female)
     .WithPhone("09027651510")
      .WithAddress("shz yavaran...")
     .Build();
        Save(_user);

        _film = new FilmBuilder()
            .WithName("گات")
           .WithGenreId(_genre2.Id)
            .Build();
        DbContext.Save(_film);
        var rent = new RentBuilder(_film.Id, _user.Id)
            .WithFilmDailyPrice(_film.DailyPriceRent)
            .WithFilmPenaltyPrice(_film.PenaltyPriceRent)
            .Build();
        DbContext.Save(rent);
        _film1 = new FilmBuilder()
              .WithName("گیم اف ترونز")
             .WithGenreId(_genre2.Id)
              .Build();
        DbContext.Save(_film1);
        var rent2 = new RentBuilder(_film1.Id, _user.Id)
         .WithFilmDailyPrice(_film1.DailyPriceRent)
         .WithFilmPenaltyPrice(_film1.PenaltyPriceRent)
         .Build();
        DbContext.Save(rent2);
        _film2 = new FilmBuilder()
                 .WithName("game of thrones")
                .WithGenreId(_genre.Id)
                 .Build();
        DbContext.Save(_film2);
        var rent3 = new RentBuilder(_film2.Id, _user.Id)
         .WithFilmDailyPrice(_film2.DailyPriceRent)
         .WithFilmPenaltyPrice(_film2.PenaltyPriceRent)
         .Build();
        DbContext.Save(rent3);

        _film3 = new FilmBuilder()
         .WithName("got")
        .WithGenreId(_genre.Id)
         .Build();
        DbContext.Save(_film3);


    }

    [When("کاربر با نام زهرا فیلم got را اجاره میبرد. ")]
    private void When()
    {
        var dto = new AddRentManangerDto
        {
            UserId = _user.Id,
            FilmId = _film3.Id,
        };
        _actual = () => _sut.Add(dto);

    }

    [Then("خطای عدم کرایه دادن بیش از سه فیلم به یک کاربر نمایش داده میشود. ")]
    [And("باید در فهرست اجاره ها برای کاربر زهرا فقط فیلم های گات و game of thrones وگیم اف ترونز وجود داشته باشد.\r\nو فیلم چهارم got اضافه نشده باشد\r\n. ")]
    private async Task Then()
    {
        await _actual.Should().ThrowExactlyAsync<UserCantRentMoreThanThreeFilmsException>();
    }


    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _ => Given(),
            _ => When(),
            _ => Then().Wait());
    }
}
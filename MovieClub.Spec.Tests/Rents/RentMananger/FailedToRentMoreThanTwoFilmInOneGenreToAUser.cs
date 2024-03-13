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

[Scenario("عدم اجاره دادن بیش از 2 فیلم از یک ژانر به یک کاربر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "فیلم جدید اضافه کنم ",
    InOrderTo = "فیلم ها را اجاره دهم")]

public class FailedToRentMoreThanTwoFilmInOneGenreToAUser : BusinessIntegrationTest
{

    private readonly RentManangerService _sut;
    private readonly DateTime _fakeTime;
    private Func<Task>? _actual;
    private Genre _genre;
    private Film _film;
    private Film _film1;
    private Film _film2;
    private Film _film3;
    private User _user;
    public FailedToRentMoreThanTwoFilmInOneGenreToAUser()
    {
        _fakeTime = new DateTime(2018, 2, 4);
        _sut = RentManangerServiceFactory.Create(SetupContext, _fakeTime);
    }
    [Given("فقط یک کاربر با نام زهرا وجود دارد.")]
    [And("یک ژانر به اسم جنگی در فهرست ژانرها وجود دارد")]
    [And(" و یک فیلم  با عنوان گات از ژانر جنگی  در فهرست فیلم ها وجود دارد.که زهرا این فیلم را کرایه کرده است.")]
    [And("و یک فیلم  با عنوان گیم اف ترونز از ژانر جنگی در فهرست فیلم ها وجود دارد.که زهرا این فیلم را کرایه کرده است.\r\n")]
    [And("و یک فیلم  با عنوان game of thrones از ژانر جنگی در فهرست  فیلم ها وجود دارد.")]

    private void Given()
    {
        _genre = new GenreBuilder()
       .WithTitle("جنگی")
       .Build();
        DbContext.Save(_genre);

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
           .WithGenreId(_genre.Id)
            .Build();
        DbContext.Save(_film);
        var rent = new RentBuilder(_film.Id, _user.Id)
            .WithFilmDailyPrice(_film.DailyPriceRent)
            .WithFilmPenaltyPrice(_film.PenaltyPriceRent)
            .Build();
        DbContext.Save(rent);
        _film1 = new FilmBuilder()
              .WithName("گیم اف ترونز")
             .WithGenreId(_genre.Id)
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

    }

    [When("من فیلم game of thrones را به کاربری  با نام زهرا را اجاره می دهم.  ")]
    private void When()
    {
        var dto = new AddRentManangerDto
        {
            UserId = _user.Id,
            FilmId = _film2.Id,
        };
        _actual = () => _sut.Add(dto);

    }

    [Then("خطای عدم اجاره دادن بیش از 2 فیلم از یک ژانر به من نمایش داده می شود. ")]
    [And("باید در فهرست اجاره ها برای کاربر زهرا فقط فیلم های گات  وگیم اف ترونز وجود داشته باشد.\r\nو فیلمgame of thrones  اضافه نشده باشد\r\n")]
    private async Task Then()
    {
        await _actual.Should().ThrowExactlyAsync<UserCantHaveMoreThanTwoFilmFromOneGenreException>();
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
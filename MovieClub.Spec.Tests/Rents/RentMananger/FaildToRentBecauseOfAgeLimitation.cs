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

[Scenario("عدم اجاره دادن فیلم به علت غیر مجاز بودن سن کاربر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "فیلم جدید اضافه کنم ",
    InOrderTo = "فیلم ها را اجاره دهم")]

public class FaildToRentBecauseOfAgeLimitation : BusinessIntegrationTest
{

    private readonly RentManangerService _sut;
    private readonly DateTime _fakeTime;
    private Func<Task>? _actual;
    private Genre _genre;
    private Film _film;
    private User _user;
    public FaildToRentBecauseOfAgeLimitation()
    {
        _fakeTime = new DateTime(2018, 2, 4);
        _sut = RentManangerServiceFactory.Create(SetupContext, _fakeTime);
    }
    [Given(" فقط یک کاربر با نام زهراو سن 22 در فهرست کاربران  وجود دارد.")]
    [And(" یک ژانر تحت عنوان جنگی در فهرست ژانر ها وجود دارد.")]
    [And(" یک فیلم  با عنوان گات از ژانر جنگی  و رده سنی 25 به بالا در فهرست فیلم ها وجود دارد ")]
    private void Given()
    {
        _genre = new GenreBuilder()
       .WithTitle("جنگی")
       .Build();
        DbContext.Save(_genre);

        _user = new UserBuilder()
      .WithFirstName("زهرا")
       .WithLastName("احمدی")
     .WithAge(new DateTime(2024, 2, 10))
        .WithGender(Gender.Female)
       .WithPhone("09027651510")
        .WithAddress("shz yavaran...")
       .Build();
        Save(_user);

        _film = new FilmBuilder()
            .WithName("گات")
            .WithMinAgeLimit(25)
           .WithGenreId(_genre.Id)
            .Build();
        DbContext.Save(_film);

    }

    [When("من فیلم گات را به کاربری  با نام زهرا را اجاره می دهم")]
    private void When()
    {
        var dto = new AddRentManangerDto
        {
            UserId = _user.Id,
            FilmId = _film.Id,
        };
        _actual = () => _sut.Add(dto);

    }

    [Then("خطای عدم اجاره دادن فیلم به علت غیر مجاز بودن سن کاربر به من نمایش داده می شود. ")]
    [And("باید در فهرست اجاره ها برای کاربر زهرا فیلم گات ثبت نشده باشد ")]
    private async Task Then()
    {
        await _actual.Should().ThrowExactlyAsync<AgeLimitationForThisFilmException>();
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
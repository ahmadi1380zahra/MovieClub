using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace MovieClub.Spec.Tests.Films.FilmMananger;

[Scenario("ویرایش کردن فیلم")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = " فیلمی که در فهرست فیلم ها وجود دارد را ویرایش کنم ",
    InOrderTo = " فیلم های معتبری در فهرست فیلم ها داشته باشد")]

public class FilmMananagerUpdateTest : BusinessIntegrationTest
{
    private readonly FilmService _sut;
    private Genre _genre;
    private Film _film;

    public FilmMananagerUpdateTest()
    {
        _sut = FilmServiceFactory.Create(SetupContext);
    }
    [Given("یک فیلم  با عنوان گات و کارگردان نولان وحداقل سن 18 وسال انتشار 1380 و قیمت روزانه 2000  و قیمت جریمه  10 درصد و مدت زمان 130  دقیقه در فهرست فیلم ها وجود دارد")]
    [And("یک ژانر به اسم جنگی در فهرست ژانرها وجود دارد")]
    private void Given()
    {
        _genre = new GenreBuilder()
            .WithTitle("جنگی")
            .Build();
        DbContext.Save(_genre);
        _film = new FilmBuilder()
            .WithName("گات")
            .WithDirector("نولان")
            .WithMinAgeLimit(18)
            .WithPublishYear(2018)
            .WithDailyPriceRent(2000)
            .WithPenaltyPriceRent(0.10M)
            .WithDuration(160)
            .WithGenreId(_genre.Id)
            .Build();
        DbContext.Save(_film);
    }

    [When(" من فیلم مذکور را به فیلم با عنوان گیم اف ترونز کارگردان زهرا وحداقل سن  22 وسال انتشار 1384 و قیمت روزانه 20000  و قیمت جریمه  15%و مدت زمان 140  دقیقه ویرایش می کنم. ")]
    private async Task When()
    {
     
        var dto = new UpdateFilmDto
        {
            Name = "گیم اف ترونز",
            Description = null,
            PublishYear = 1384,
            DailyPriceRent = 20000,
            MinAgeLimit = 22,
            PenaltyPriceRent = 0.15M,
            Duration = 140,
            Director = "زهرا",
            GenreId = _genre.Id,
        };

        await _sut.Update(_film.Id,dto);

    }

    [Then("باید در فهرست فیلم ها فقط یک فیلم با عنوان گیم اف ترونز و کارگردان زهرا وحداقل سن  22 وسال انتشار 1384 و قیمت روزانه 20000  و قیمت جریمه  15%  و مدت زمان 140 دقیقه وجود داشته باشد. ")]
    private void Then()
    {
        var actual = ReadContext.Films.Single();
        actual.Name.Should().Be("گیم اف ترونز");
        actual.PublishYear.Should().Be(1384);
        actual.DailyPriceRent.Should().Be(20000);
        actual.MinAgeLimit.Should().Be(22);
        actual.PenaltyPriceRent.Should().Be(0.15M);
        actual.Duration.Should().Be(140);
        actual.Director.Should().Be("زهرا");
        actual.GenreId.Should().Be(_genre.Id);
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
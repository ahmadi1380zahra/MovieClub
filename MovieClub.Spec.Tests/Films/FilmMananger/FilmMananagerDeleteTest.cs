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

[Scenario("حذف کردن فیلم")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = " فیلمی که در فهرست فیلم ها وجود دارد را حذف کنم ",
    InOrderTo = "فقط  فیلم های  معتبر را  در فهرست فیلم ها داشته باشم")]

public class FilmMananagerDeleteTest : BusinessIntegrationTest
{
    private readonly FilmService _sut;
    private Genre _genre;
    private Film _film;

    public FilmMananagerDeleteTest()
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

    [When("من فیلم مذکور  را حذف میکنم.")]
    private async Task When()
    {


        await _sut.Delete(_film.Id);

    }

    [Then("باید در فهرست فیلم ها هیچ فیلمی وجود نداشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Films.FirstOrDefault(_ => _.Id == _film.Id);
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
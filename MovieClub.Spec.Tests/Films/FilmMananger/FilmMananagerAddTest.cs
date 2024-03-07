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

[Scenario("اضافه شدن فیلم")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "فیلم جدید اضافه کنم ",
    InOrderTo = "فیلم ها را اجاره دهم")]

public class FilmMananagerAddTest : BusinessIntegrationTest
{
    private readonly FilmService _sut;
    private Genre _genre;

    public FilmMananagerAddTest()
    {
        _sut = FilmServiceFactory.Create(SetupContext);
    }
    [Given("هیچ فیلمی در فهرست فیلم ها وجود ندارد")]
    [And("یک ژانر به اسم جنگی در فهرست ژانرها وجود دارد")]
    private void Given()
    {
        _genre = new GenreBuilder()
            .WithTitle("جنگی")
            .Build();
        DbContext.Save(_genre);
    }

    [When("من فیلم با عنوان گات و کارگردان نولان وحداقل سن 18 وسال انتشار 1380 و قیمت روزانه 2000  و قیمت جریمه  10 درصد و مدت زمان 130  دقیقه ثبت میکنم")]
    private async Task When()
    {
        var dto = new AddFilmDtoBuilder(_genre.Id)
            .WithName("گات")
            .WithDirector("نولان")
            .WithMinAgeLimit(18)
            .WithPublishYear(2018)
            .WithDailyPriceRent(2000)
            .WithPenaltyPriceRent(0.10M)
            .WithDuration(160)
            .Build();

        await _sut.Add(dto);

    }

    [Then("باید در فهرست فیلم ها فقط یک فیلم با عنوان گات و کارگردان نولان وحداقل سن  18 وسال انتشار 1380 و قیمت روزانه 2000  و قیمت جریمه  10%  و مدت زمان 130 دقیقه وجود داشته باشد. ")]
    private void Then()
    {
        var actual = ReadContext.Films.Single();
        actual.Name.Should().Be("گات");
        actual.PublishYear.Should().Be(2018);
        actual.DailyPriceRent.Should().Be(2000);
        actual.MinAgeLimit.Should().Be(18);
        actual.PenaltyPriceRent.Should().Be(0.10M);
        actual.Duration.Should().Be(160);
        actual.Director.Should().Be("نولان");
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
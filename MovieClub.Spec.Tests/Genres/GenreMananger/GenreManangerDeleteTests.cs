using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace MovieClub.Spec.Tests.Genres.GenreMananger;

[Scenario("حذف کردن  ژانر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "ژانری که در فهرست ژانر ها وجود دارد را حذف کنم  ",
    InOrderTo = "فقط  ژانرهای معتبررا  در فهرست ژانر ها داشته باشم")]

public class GenreManangerDeleteTests : BusinessIntegrationTest
{
    private readonly GenreManangerService _sut;
    private Genre _genre;

    public GenreManangerDeleteTests()
    {
        _sut = GenreManangerServiceFactory.Create(SetupContext);
    }
    [Given("فقط یک ژانر با عنوان ترسناک در فهرست ژانر ها وجود دارد ")]

    private void Given()
    {
        _genre = new GenreBuilder().WithTitle("ترسناک").Build();
        DbContext.Save(_genre);
    }

    [When("من ژانر مذکور  را حذف میکنم.")]
    private async Task When()
    {
        await _sut.Delete(_genre.Id);

    }

    [Then("باید در فهرست ژانرها هیچ ژانری وجود نداشته باشد. ")]
    private void Then()
    {
        var actual = ReadContext.Genres.FirstOrDefault(_=>_.Id==_genre.Id);
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
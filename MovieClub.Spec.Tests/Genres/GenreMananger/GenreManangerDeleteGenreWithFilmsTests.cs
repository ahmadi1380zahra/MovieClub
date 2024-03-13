using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace MovieClub.Spec.Tests.Genres.GenreMananger;

[Scenario("عدم امکان حذف ژانر به علت وجود داشتن فیلم در ژانر مربوطه")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "ژانری که در فهرست ژانر ها وجود دارد را حذف کنم  ",
    InOrderTo = "فقط  ژانرهای معتبررا  در فهرست ژانر ها داشته باشم")]

public class GenreManangerDeleteGenreWithFilmsTests : BusinessIntegrationTest
{
    private readonly GenreManangerService _sut;
    private Genre _genre;
    private Func<Task>? _actual;
    public GenreManangerDeleteGenreWithFilmsTests()
    {
        _sut = GenreManangerServiceFactory.Create(SetupContext);
    }
    [Given("فقط یک ژانر با عنوان ترسناک در فهرست ژانر ها وجود دارد ")]
    [And("و یک فیلم با عنوان گات و سال انتشار 1380 ومدت زمان  140 دقیقه و ژانر ترسناک وجود دارد.")]
    private void Given()
    {
        _genre = new GenreBuilder().WithTitle("ترسناک").Build();
        DbContext.Save(_genre);
        var film = new FilmBuilder().WithGenreId(_genre.Id).WithName("گات").Build();
        DbContext.Save(film);
    }

    [When("من ژانر با عنوان ترسناک را حذف میکنم.")]
    private void When()
    {
        _actual= ()=> _sut.Delete(_genre.Id);

    }

    [Then("باید خطای عدم امکان  حذف ژانر به علت وجود داشتن فیلم در ژانر مربوطه  را مشاهده کنم ")]
    private async Task Then()
    {
     await _actual.Should().ThrowAsync<GenreCantBeDeletedItHasFilmsException>();
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
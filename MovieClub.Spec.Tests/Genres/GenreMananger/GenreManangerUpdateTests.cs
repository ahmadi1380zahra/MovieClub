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

[Scenario("ویرایش کردن ژانر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "ژانری که در فهرست ژانر ها وجود دارد را ویرایش کنم ",
    InOrderTo = "بتوانم ژانرهای معتبری در فهرست ژانر ها داشته باشم")]

public class GenreManangerUpdateTests : BusinessIntegrationTest
{
    private readonly GenreManangerService _sut;
    private Genre _genre;

    public GenreManangerUpdateTests()
    {
        _sut = GenreManangerServiceFactory.Create(SetupContext);
    }
    [Given("فقط یک ژانر با عنوان ترسناک در فهرست ژانر ها وجود دارد. ")]

    private void Given()
    {
        _genre = new GenreBuilder()
           .WithTitle("ترسناک")
           .Build();
        DbContext.Save(_genre);
    }

    [When("من ژانر مذکور را به ژانر با عنوان وحشت ناک ویرایش میکنم.")]
    private async Task When()
    {
        var dto = UpdateGenreManangerDtoFactory.Create("وحشت ناک");

        await _sut.Update(_genre.Id, dto);

    }

    [Then("باید در فهرست ژانرها فقط یک ژانر با عنوان وحشت ناک وجود داشته باشد ")]
    private void Then()
    {
        var actual = ReadContext.Genres.First();
        actual.Title.Should().Be("وحشت ناک");
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
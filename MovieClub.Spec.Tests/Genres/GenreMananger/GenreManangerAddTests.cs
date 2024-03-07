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

[Scenario("ثبت کردن ژانر")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "ژانر جدید اضافه کنم ",
    InOrderTo = "  فیلم ها را به ژانر مربوطه اضافه کنم")]

public class GenreManangerAddTests : BusinessIntegrationTest
{
    private readonly GenreManangerService _sut;
   

    public GenreManangerAddTests()
    {
        _sut = GenreManangerServiceFactory.Create(SetupContext);
    }
    [Given("ژانری در فهرست ژانر ها وجود ندارد ")]
 
    private void Given()
    {
      
    }

    [When("من ژانری با عنوان ترسناک ثبت میکنم")]
    private async Task When()
    {
        var dto=AddGenreManangerDtoFactory.Create("ترسناک");


        await _sut.Add(dto);

    }

    [Then("باید در فهرست ژانرها فقط یک ژانر با عنوان ترسناک وجود داشته باشد ")]
    private void Then()
    {
        var actual = ReadContext.Genres.Single();
        actual.Title.Should().Be("ترسناک");
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
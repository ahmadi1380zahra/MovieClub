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
using Xunit.Sdk;

namespace MovieClub.Spec.Tests.Genres.GenreMananger;

[Scenario("عدم ثبت ژانر با نام تکراری")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "ژانر جدید اضافه کنم ",
    InOrderTo = "  فیلم ها را به ژانر مربوطه اضافه کنم")]

public class GenreManangerAddReduplicateTitleTests : BusinessIntegrationTest
{
    private readonly GenreManangerService _sut;
    private Func<Task>? _actual;

    public GenreManangerAddReduplicateTitleTests()
    {
        _sut = GenreManangerServiceFactory.Create(SetupContext);
    }
    [Given("فقط یک ژانر با عنوان ترسناک در فهرست ژانر ها وجود دارد.")]
 
    private void Given()
    {
        var genre = new GenreBuilder().WithTitle("ترسناک").Build();
        DbContext.Save(genre);
    }

    [When("من ژانری با عنوان ترسناک ثبت میکنم")]
    private async Task When()
    {
        var dto=AddGenreManangerDtoFactory.Create("ترسناک");


        _actual = async () => await _sut.Add(dto);

    }

    [Then("باید خطای عدم امکان ثبت ژانر با عنوان تکراری را ببینم. ")]
    private void Then()
    {
        _actual.Should().ThrowExactlyAsync<GenreIsNotExistException>();
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
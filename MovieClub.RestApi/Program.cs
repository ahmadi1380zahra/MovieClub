using Microsoft.EntityFrameworkCore;
using MovieClub.Contracts.Interfaces;
using MovieClub.Infrastructure;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Films;
using MovieClub.Persistence.EF.Genres;
using MovieClub.Persistence.EF.Users;
using MovieClub.Services.Films.FilmMananger;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Genres.Genre;
using MovieClub.Services.Genres.Genre.Contracts;
using MovieClub.Services.Genres.GenreManagers;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Users.UserMananger;
using MovieClub.Services.Users.UserMananger.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EFDataContext>(
    options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<EFDataContext>();
builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<GenreManangerService, GenreManangerAppService>();
builder.Services.AddScoped<GenreService, GenreAppService>();
builder.Services.AddScoped<GenreMananagerRepository, EFGenreMananagerRepository>();
builder.Services.AddScoped<FilmService, FilmAppService>();
builder.Services.AddScoped<FilmRepository, EFFilmRepository>();
builder.Services.AddScoped<UserMananengerService, UserMananengerAppService>();
builder.Services.AddScoped<UserMananengerRepository, EFUserMananengerRepository>();
builder.Services.AddScoped<DateTimeService, DateTimeAppService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Persistence.EF.Films
{
    public class EFFilmRepository : FilmRepository
    {
        private readonly DbSet<Film> _films;
        public EFFilmRepository(EFDataContext context)
        {
            _films = context.Set<Film>();
        }

        public void Add(Film film)
        {
            _films.Add(film);
        }

        public void Delete(Film film)
        {
            _films.Remove(film);
        }

        public async Task<Film?> Find(int id)
        {
            return await _films.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetFilmDto>?> GetAll(GetFilmFilterDto? dto)
        {
            var films = _films.Select(film => new GetFilmDto()
            {
                Name = film.Name,
                Description = film.Description,
                PublishYear = film.PublishYear,
                DailyPriceRent = film.DailyPriceRent,
                PenaltyPriceRent = film.PenaltyPriceRent,
                Director = film.Director,
                Duration = film.Duration,
                MinAgeLimit = "+ " + film.MinAgeLimit,

            });
            if (dto.Name != null)
            {
                films = films.Where(_ => _.Name.Replace(" ", string.Empty).Contains(dto.Name.Replace(" ", string.Empty)));
            }
            return await films.ToListAsync();
        }

        public void Update(Film film)
        {
            _films.Update(film);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MovieClub.Entities;
using MovieClub.Services.Genres.Genre.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Persistence.EF.Genres
{
    public class EFGenreMananagerRepository : GenreMananagerRepository
    {
        private readonly DbSet<Genre> _genres;
        public EFGenreMananagerRepository(EFDataContext context)
        {
            _genres = context.Genres;
        }

        public void Add(Genre genre)
        {
            _genres.Add(genre);
        }

        public void Delete(Genre genre)
        {
            _genres.Remove(genre);
        }

        public async Task<Genre?> Find(int id)
        {
            return await _genres.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<GetGenreManangerDto>?> GetAll(GetGenreManangerFilterDto? dto)
        {
            IQueryable<Genre> query = _genres;
            if (dto.Title != null)
            {
                query = query.Where(_ => _.Title.Replace(" ", string.Empty).Contains(dto.Title.Replace(" ", string.Empty)));
            };
            List<GetGenreManangerDto> genres = await query.Select(x => new GetGenreManangerDto
            {
                Id = x.Id,
                Title = x.Title,
                //Rate = x.Rate,
            }).ToListAsync();
            return genres;
        }

        public async Task<List<GetGenreDto>?> GetAllUser(GetGenreFilterDto? dto)
        {
            IQueryable<Genre> query = _genres;
            if (dto.Title!=null)
            {
                query = query.Where(_ => _.Title.Replace(" ",string.Empty).Contains(dto.Title.Replace(" ",string.Empty)));
            }
            List<GetGenreDto> genres = await query.Select(genre => new GetGenreDto
            {
                Id = genre.Id,
                Title = genre.Title,
            }).ToListAsync();
            return genres;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _genres.AnyAsync(_ => _.Id == id);
        }

        public async Task<bool> IsExistFilmForThisGenre(int id)
        {
            return await _genres.Include(_ => _.Films).AnyAsync(_ => _.Films.Any(_ => _.GenreId == id));
        }

        public void Update(Genre genre)
        {
            _genres.Update(genre);
        }
    }
}

using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Films.FilmMananger.Contracts
{
    public interface FilmRepository
    {
        void Add(Film film);
        void Delete(Film film);
        Task<Film?> Find(int id);
        Task<List<GetFilmDto>?> GetAll(GetFilmFilterDto? dto);
        void Update(Film film);
    }
}

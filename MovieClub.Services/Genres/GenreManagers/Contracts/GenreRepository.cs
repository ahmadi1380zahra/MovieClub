using MovieClub.Entities;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Genres.GenreManagers.Contracts
{
    public interface GenreRepository
    {
        void Add(Genre genre);
        void Delete(Genre genre);
        Task<Genre?> Find(int id);
        Task<List<GetGenreDto>?> GetAll(GetGenreFilterDto? dto);
        void Update(Genre genre);
        Task<bool> IsExist(int id);
        Task<bool> IsExistFilmForThisGenre(int id);
    }
}

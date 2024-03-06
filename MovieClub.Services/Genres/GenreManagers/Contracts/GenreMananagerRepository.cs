using MovieClub.Entities;
using MovieClub.Services.Genres.Genre.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Genres.GenreManagers.Contracts
{
    public interface GenreMananagerRepository
    {
        void Add(Entities.Genre genre);
        void Delete(Entities.Genre genre);
        Task<Entities.Genre?> Find(int id);
        Task<List<GetGenreManangerDto>?> GetAll(GetGenreManangerFilterDto? dto);
        void Update(Entities.Genre genre);
        Task<bool> IsExist(int id);
        Task<bool> IsExistFilmForThisGenre(int id);
        Task<List<GetGenreDto>?> GetAllUser(GetGenreFilterDto? dto);
    }
}

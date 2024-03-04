using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Genres.GenreManagers.Contracts
{
    public interface GenreService
    {
        Task Add(AddGenreDto dto);
        Task Delete(int id);
       Task< List<GetGenreDto>?> GetAll(GetGenreFilterDto? dto);
        Task Update(int id, UpdateGenreDto dto);
    }
}

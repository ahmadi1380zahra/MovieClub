using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Genres.GenreManagers.Contracts
{
    public interface GenreManangerService
    {
        Task Add(AddGenreManangerDto dto);
        Task Delete(int id);
       Task< List<GetGenreManangerDto>?> GetAll(GetGenreManangerFilterDto? dto);
        Task Update(int id, UpdateGenreManangerDto dto);
    }
}

using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Films.FilmMananger.Contracts
{
    public interface FilmService
    {
        Task Add(AddFilmDto dto);
        Task Delete(int id);
        Task<List<GetFilmDto>?> GetAll(GetFilmFilterDto? dto);
        Task Update(int id, UpdateFilmDto dto);
    }
}

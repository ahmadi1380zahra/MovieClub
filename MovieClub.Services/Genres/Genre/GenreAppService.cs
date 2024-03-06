using MovieClub.Contracts.Interfaces;
using MovieClub.Services.Genres.Genre.Contracts;
using MovieClub.Services.Genres.Genre.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Genres.Genre
{
    public class GenreAppService : GenreService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly GenreMananagerRepository _repository;
        public GenreAppService(GenreMananagerRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetGenreDto>?> GetAll(GetGenreFilterDto? dto)
        {
            return await _repository.GetAllUser(dto);
        }
    }
}

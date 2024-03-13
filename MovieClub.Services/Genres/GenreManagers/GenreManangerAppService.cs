using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Genres.GenreManagers
{
    public class GenreManangerAppService : GenreManangerService
    {
        private readonly GenreMananagerRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public GenreManangerAppService(GenreMananagerRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddGenreManangerDto dto)
        {
            if (await _repository.IsReduplicate(dto.Title))
            {
                throw new GenreTitleIsReduplicted();
            }
            var genre = new Entities.Genre
            {
                Title = dto.Title,
                //Rate = 0,
            };
            _repository.Add(genre);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var genre = await _repository.Find(id);
            if (genre is null)
            {
                throw new GenreIsNotExistException();
            }
            if (await _repository.IsExistFilmForThisGenre(id))
            {
                throw new GenreCantBeDeletedItHasFilmsException();
            }
            _repository.Delete(genre);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetGenreManangerDto>?> GetAll(GetGenreManangerFilterDto? dto)
        {
            return await _repository.GetAll(dto);
        }

        public async Task Update(int id, UpdateGenreManangerDto dto)
        {
            var genre = await _repository.Find(id);
            if (genre is null)
            {
                throw new GenreIsNotExistException();
            }
            genre.Title = dto.Title;
            _repository.Update(genre);
            await _unitOfWork.Complete();
        }
    }
}

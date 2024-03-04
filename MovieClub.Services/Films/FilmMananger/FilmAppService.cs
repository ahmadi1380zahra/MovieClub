using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Services.Films.FilmMananger.Exceptions;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Films.FilmMananger
{
    public class FilmAppService : FilmService
    {
        private readonly FilmRepository _repository;
        private readonly GenreRepository _genreRepository;
        private readonly UnitOfWork _unitOfWork;
        public FilmAppService(FilmRepository repository,
                             UnitOfWork unitOfWork,
                             GenreRepository genreRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _genreRepository = genreRepository;
        }

        public async Task Add(AddFilmDto dto)
        {
            if (! await _genreRepository.IsExist(dto.GenreId))
            {
                throw new GenreIsNotExistException();
            }
            var film = new Film
            {
                Name = dto.Name,
                Description = dto.Description,
                PublishYear = dto.PublishYear,
                DailyPriceRent = dto.DailyPriceRent,
                MinAgeLimit = dto.MinAgeLimit,
                PenaltyPriceRent = dto.PenaltyPriceRent,
                Duration = dto.Duration,
                Director = dto.Director,
                GenreId = dto.GenreId,
                Stock = 1,
               
            };
            _repository.Add(film);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var film =await _repository.Find(id);
            if (film == null)
            {
                throw new FilmIsNotExistException();
            }
            _repository.Delete(film);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetFilmDto>?> GetAll(GetFilmFilterDto? dto)
        {
            return await  _repository.GetAll(dto);
        }

        public async Task Update(int id, UpdateFilmDto dto)
        {
            if (!await _genreRepository.IsExist(dto.GenreId))
            {
                throw new GenreIsNotExistException();
            }
            var film =await _repository.Find(id);
            if (film == null)
            {
                throw new FilmIsNotExistException();
            }
            film.Name=dto.Name;
            film.Description=dto.Description;
            film.PublishYear=dto.PublishYear;
            film.DailyPriceRent=dto.DailyPriceRent;
            film.MinAgeLimit=dto.MinAgeLimit;
            film.PenaltyPriceRent=dto.PenaltyPriceRent;
            film.Duration = dto.Duration;   
            film.Director= dto.Director;
            film.GenreId=dto.GenreId;
            _repository.Update(film);
            await _unitOfWork.Complete();
        }
    }
}

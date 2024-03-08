using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Rents.RentManangers.Contracts;
using MovieClub.Services.Rents.RentManangers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Rents.RentManangers
{
    public class RentManangerAppService : RentManangerService
    {
        private readonly RentManangerRepository _repository;
        private readonly FilmRepository _filmRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly DateTimeService _dateTimeService;
        public RentManangerAppService(RentManangerRepository repository,
            UnitOfWork unitOfWork
            , FilmRepository filmRepository
           , DateTimeService dateTimeService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dateTimeService = dateTimeService;
            _filmRepository = filmRepository;
        }

        public async Task Add(AddRentManangerDto dto)
        {
            var film =await _filmRepository.Find(dto.FilmId);
            var rent = new Rent
            {
                UserId = dto.UserId,
                FilmId = dto.FilmId,
                RentAt = _dateTimeService.UtcNow(),
                FilmDailyPrice=film.DailyPriceRent,
                FilmPenaltyPrice=film.PenaltyPriceRent,
                FilmRate = 0,
                GiveBackAt = null,
                Cost = 0
            };
            _repository.Add(rent);
            await _unitOfWork.Complete();
        }
    }
}

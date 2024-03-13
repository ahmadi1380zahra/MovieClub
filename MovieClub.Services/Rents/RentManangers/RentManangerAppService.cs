using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Rents.RentManangers.Contracts;
using MovieClub.Services.Rents.RentManangers.Contracts.Dtos;
using MovieClub.Services.Rents.RentManangers.Exceptions;
using MovieClub.Services.Users.UserMananger.Contracts;
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
        private readonly UserMananengerRepository _userManangerRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly DateTimeService _dateTimeService;
        public RentManangerAppService(RentManangerRepository repository,
            UnitOfWork unitOfWork
            , FilmRepository filmRepository
           , DateTimeService dateTimeService
            , UserMananengerRepository userMananengerRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dateTimeService = dateTimeService;
            _filmRepository = filmRepository;
            _userManangerRepository = userMananengerRepository;
        }

        public async Task Add(AddRentManangerDto dto)
        {
            var genreId = await _filmRepository.GenreIdIs(dto.FilmId);

            if (await _repository.FilmGenreRentCounts(dto.UserId, genreId) >= 2)
            {
                throw new UserCantHaveMoreThanTwoFilmFromOneGenreException();
            }
            var film = await _filmRepository.Find(dto.FilmId);
            if (await _repository.FilmRentCounts(dto.UserId) >= 3)
            {
                throw new UserCantRentMoreThanThreeFilmsException();
            }
            var user = await _userManangerRepository.Find(dto.UserId);
            if (film.MinAgeLimit > (DateTime.UtcNow - user.Age).Days / 365)
            {
                throw new AgeLimitationForThisFilmException();
            }
            var rent = new Rent
            {
                UserId = dto.UserId,
                FilmId = dto.FilmId,
                RentAt = _dateTimeService.UtcNow(),
                FilmDailyPrice = film.DailyPriceRent,
                FilmPenaltyPrice = film.PenaltyPriceRent,
                FilmRate = 0,
                GiveBackAt = null,
                Cost = 0
            };
            _repository.Add(rent);
            await _unitOfWork.Complete();
        }

        public async Task Update(int id, UpdateRentManangerDto dto)
        {
            var rent = await _repository.Find(id);
            rent.FilmRate = dto.FilmRate;
            rent.GiveBackAt = _dateTimeService.UtcNow();
            if ((_dateTimeService.UtcNow() - rent.RentAt).Days > 7)
            {
                rent.Cost = ((_dateTimeService.UtcNow() - rent.RentAt).Days * rent.FilmDailyPrice)
                    + (((_dateTimeService.UtcNow() - rent.RentAt).Days - 7) *( rent.FilmDailyPrice*rent.FilmPenaltyPrice));

            }
            else
            {
                rent.Cost = (rent.GiveBackAt.Value - rent.RentAt).Days * rent.FilmDailyPrice;
            }
            _repository.Update(rent);
            await _unitOfWork.Complete();
        }
    }
}

using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using MovieClub.Services.Users.UserMananger.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using MovieClub.Services.Users.UserMananger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Users.UserMananger
{
    public class UserMananengerAppService : UserMananengerService
    {
        private readonly UserMananengerRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly DateTimeService _dateTimeService;
        public UserMananengerAppService(UserMananengerRepository repository
                                      , UnitOfWork unitOfWork
                                      , DateTimeService dateTimeService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dateTimeService = dateTimeService;
        }

        public async Task Add(AddUserManangerDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                Age = dto.Age,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
                CreateAt = _dateTimeService.UtcNow()
            };
            _repository.Add(user);
            await _unitOfWork.Complete();
        }

        public async Task Update(int id, UpdateUserManangerDto dto)
        {
            var user = await _repository.Find(id);
            if (user == null)
            {
                throw new UserIsNotExistException();
            }
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Address = dto.Address;
            user.Age = dto.Age;
            user.Gender = dto.Gender;
            _repository.Update(user);
            await _unitOfWork.Complete();
        }
    }
}

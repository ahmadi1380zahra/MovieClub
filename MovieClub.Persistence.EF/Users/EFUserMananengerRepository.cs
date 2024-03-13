using Azure;
using Microsoft.EntityFrameworkCore;
using MovieClub.Entities;
using MovieClub.Services.Users.UserMananger.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Persistence.EF.Users
{
    public class EFUserMananengerRepository : UserMananengerRepository
    {
        private readonly DbSet<User> _users;
        public EFUserMananengerRepository(EFDataContext context)
        {
            _users = context.Users;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public async Task<User?> Find(int id)
        {
            return await _users.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetUserManangerDto>?> GetAll(GetUserManangerFilterDto? dto)
        {
            IQueryable<User> query = _users;
            if (dto.Name != null)
            {
                query = query.Where(_ => (_.FirstName + _.LastName).Replace(" ", string.Empty).Contains(dto.Name.Replace(" ", string.Empty)));
            }
            List<GetUserManangerDto> users = await query.Include(_=>_.Rents).Select(user => new GetUserManangerDto
            {
                Id = user.Id,
                FullName = user.FirstName + user.LastName,
                Age = (DateTime.UtcNow - user.Age).Days / 365,
                UserRate = user.Rents.Where(_=>_.GiveBackAt != null).Count(_ => ((_.GiveBackAt!  ) - _.RentAt).Value.Days <= 7)
                         - user.Rents.Count(_ => ((_.GiveBackAt !) - _.RentAt).Value.Days > 7)
                //UserRate = user.Rents.Count(_ => ((_.GiveBackAt?? _.RentAt) - _.RentAt).Days <= 7)
                //         - user.Rents.Count(_ => ((_.GiveBackAt?? _.RentAt) - _.RentAt).Days > 7)

            }).ToListAsync();
            return users;
        }

        public void Update(User user)
        {
            _users.Update(user);
        }
    }
}

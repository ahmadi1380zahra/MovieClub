using Microsoft.EntityFrameworkCore;
using MovieClub.Entities;
using MovieClub.Services.Users.UserMananger.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Persistence.EF.Users
{
    public class EFUserMananengerRepository: UserMananengerRepository
    {
        private readonly DbSet<User> _users;
        public EFUserMananengerRepository(EFDataContext context)
        {
                _users=context.Users;
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
          return await _users.FirstOrDefaultAsync(_=>_.Id == id);
        }

        public void Update(User user)
        {
           _users.Update(user);
        }
    }
}

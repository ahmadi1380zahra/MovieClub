using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Users.UserMananger.Contracts
{
    public interface UserMananengerRepository
    {
        void Add(User user);
        void Delete(User user);
        Task<User?> Find(int id);
        void Update(User user);
    }
}

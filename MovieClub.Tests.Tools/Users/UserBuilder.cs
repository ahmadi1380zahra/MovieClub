using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Users
{
    public class UserBuilder
    {
        private readonly User _user;
        public UserBuilder()
        {
            _user = new User
            {
                FirstName = "missy-firstname",
                LastName = "missy-lastname",
                Age = new DateTime(2000, 4, 5),
                Gender = Gender.Female,
                Address = "shz ...",
                PhoneNumber = "09027651552",
                CreateAt = DateTime.UtcNow
            };
        }
        public User Build()
        {
            return _user;
        }

    }
}

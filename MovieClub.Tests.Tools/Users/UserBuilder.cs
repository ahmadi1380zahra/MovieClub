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
        public UserBuilder WithFirstName(string name)
        {
        _user.FirstName = name;
            return this;
        }
        public UserBuilder WithLastName(string lastName)
        {
            _user.LastName = lastName;
            return this;
        }
        public UserBuilder WithAge(DateTime date)
        {
            _user.Age = date;
            return this;
        }
        public UserBuilder WithGender(Gender gender)
        {
            _user.Gender = gender;
            return this;
        }
        public UserBuilder WithAddress(string address)
        {
            _user.Address = address;
            return this;
        }
        public UserBuilder WithPhone(string phone)
        {
            _user.PhoneNumber = phone;
            return this;
        }
        public User Build()
        {
            return _user;
        }

    }
}

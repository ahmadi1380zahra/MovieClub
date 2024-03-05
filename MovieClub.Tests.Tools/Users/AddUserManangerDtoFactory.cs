using MovieClub.Entities;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Users
{
    public static class AddUserManangerDtoFactory
    {
        public static AddUserManangerDto Create()
        {
            return new AddUserManangerDto
            {
                FirstName = "missy-firstname",
                LastName = "missy-lastname",
                Age = new DateTime(2000, 4, 5),
                Gender = Gender.Female,
                Address = "shz ...",
                PhoneNumber = "09027651552"
            };
        }
    }
}

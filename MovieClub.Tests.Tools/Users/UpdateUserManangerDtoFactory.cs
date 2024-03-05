using MovieClub.Entities;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Users
{
    public static class UpdateUserManangerDtoFactory
    {
        public static UpdateUserManangerDto Create()
        {
            return new UpdateUserManangerDto
            {
                FirstName = "updated-missy-firstname",
                LastName = "updated-missy-lastname",
                Age = new DateTime(2010, 4, 5),
                Gender = Gender.Female,
                Address = "updated-_shz ...",
                PhoneNumber = "09027651552",

            };
        }
    }
}

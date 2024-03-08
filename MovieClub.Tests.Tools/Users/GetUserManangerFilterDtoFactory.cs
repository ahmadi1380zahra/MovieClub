using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Users
{
    public static class GetUserManangerFilterDtoFactory
    {
        public static GetUserManangerFilterDto Create(string? name = null)
        {
            return new GetUserManangerFilterDto
            {
                Name = name ?? null

            };
        }
    }
}

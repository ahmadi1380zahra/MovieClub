using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Genres
{
    public static class AddGenreManangerDtoFactory
    {
        public static AddGenreManangerDto Create()
        {
            return new AddGenreManangerDto
            {
                Title = "dummy-title",
                //Rate = 0
            };
        }
    }
}

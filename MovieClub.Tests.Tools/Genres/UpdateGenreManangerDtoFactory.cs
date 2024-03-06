using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Genres
{
    public static class UpdateGenreManangerDtoFactory
    {
        public static UpdateGenreManangerDto Create(string? title=null)
        {
            return new UpdateGenreManangerDto
            {
                Title = title ?? "updated-dummy-title",
                //Rate = 0
            };
        }
    }
}

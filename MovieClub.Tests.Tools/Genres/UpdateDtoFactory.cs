using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Genres
{
    public static class UpdateDtoFactory
    {
        public static UpdateGenreDto Create(string? title=null)
        {
            return new UpdateGenreDto
            {
                Title = title ?? "updated-dummy-title",
                //Rate = 0
            };
        }
    }
}

using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Genres
{
    public static class GetGenreManangerFilterDtoFactory
    {
        public static GetGenreManangerFilterDto Create(string? title = null)
        {
            return new GetGenreManangerFilterDto
            {
                Title = title ?? null
            };
        }
    }
}

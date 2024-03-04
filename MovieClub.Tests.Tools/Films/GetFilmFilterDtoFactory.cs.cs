using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Films
{
    public static class GetFilmFilterDtoFactory
    {
        public static GetFilmFilterDto Create(string? name = null)
        {
            return new GetFilmFilterDto
            {
                Name = name ?? "dummy-film-name",
            };
        }
    }
}

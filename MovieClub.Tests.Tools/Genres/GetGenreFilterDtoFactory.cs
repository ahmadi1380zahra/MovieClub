﻿using MovieClub.Services.Genres.Genre.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Genres
{
    public class GetGenreFilterDtoFactory
    {
        public static GetGenreFilterDto Create(string? name=null)
        {
            return new GetGenreFilterDto { 
                Title =name ?? null
            };

        }
    }
}

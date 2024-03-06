
using Microsoft.EntityFrameworkCore;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Films;
using MovieClub.Persistence.EF.Genres;
using MovieClub.Services.Films.FilmMananger;
using MovieClub.Services.Films.FilmMananger.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Films
{
    public static class FilmServiceFactory
    {
        public static FilmService Create(EFDataContext context)
        {
            return new FilmAppService(new EFFilmRepository(context), new EFUnitOfWork(context),new EFGenreMananagerRepository(context));
        }
    }
}

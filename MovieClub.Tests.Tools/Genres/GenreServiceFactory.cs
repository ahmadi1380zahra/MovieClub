using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Genres;
using MovieClub.Services.Genres.Genre;
using MovieClub.Services.Genres.Genre.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Genres
{
    public static class GenreServiceFactory
    {
        public static GenreService Create(EFDataContext context)
        {
            return new GenreAppService(new EFGenreMananagerRepository(context),new EFUnitOfWork(context));
        }
    }
}

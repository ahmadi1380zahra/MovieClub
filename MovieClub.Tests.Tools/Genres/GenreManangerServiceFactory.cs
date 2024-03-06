
using MovieClub.Contracts.Interfaces;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Genres;
using MovieClub.Services.Genres.GenreManagers;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Genres
{
    public static class GenreManangerServiceFactory
    {
        public static GenreManangerService Create(EFDataContext context)
        {
            return new GenreManangerAppService(new EFGenreMananagerRepository(context), new EFUnitOfWork(context));
        }
    }
}

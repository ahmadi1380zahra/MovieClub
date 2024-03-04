using DoctorAppointment.Persistance.EF;
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
    public static class GenreServiceFactory
    {
        public static GenreService Create(EFDataContext context)
        {
            return new GenreAppService(new EFGenreRepository(context), new EFUnitOfWork(context));
        }
    }
}

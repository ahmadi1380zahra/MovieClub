using Microsoft.EntityFrameworkCore;
using MovieClub.Entities;
using MovieClub.Services.Rents.RentManangers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Persistence.EF.Rents
{
    public class EFRentManangerRepository : RentManangerRepository
    {
        private readonly DbSet<Rent> _rents;
        public EFRentManangerRepository(EFDataContext context)
        {
            _rents = context.Set<Rent>();
        }

        public void Add(Rent rent)
        {
            _rents.Add(rent);
        }

        public async Task<int> FilmGenreRentCounts(int userId, int genreId)
        {         
            return await _rents.Include(_ => _.Film)
                .CountAsync(_ => _.UserId == userId
            && _.GiveBackAt == null
             && _.Film.GenreId == genreId);
            //nashod ba id film bekesham biron
            //return await _films.Where(_ => _.Id == filmId)
            //                       .Select(_ => _.GenreId)
            //                       .FirstOrDefaultAsync();
            //return _rents.Include(_ => _.Film)
            //    .CountAsync(_ => _.UserId == userId
            //&& _.GiveBackAt == null
            // && _.Film.GenreId == );
        }

        public async Task<int> FilmRentCounts(int userId)
        {
            return await _rents.CountAsync(_ => _.UserId == userId && _.GiveBackAt == null);
        }

        public async Task<Rent?> Find(int id)
        {
            return  await _rents.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public void Update(Rent rent)
        {
            _rents.Update(rent);
        }
    }
}

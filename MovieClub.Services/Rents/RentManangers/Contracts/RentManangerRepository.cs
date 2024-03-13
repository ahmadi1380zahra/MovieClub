using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Rents.RentManangers.Contracts
{
    public interface RentManangerRepository
    {
        void Add(Rent rent);
        Task<int> FilmGenreRentCounts(int userId, int genreId);
        Task<int> FilmRentCounts(int userId);
        Task<Rent?> Find(int id);
        void Update(Rent rent);
    }
}

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
    }
}

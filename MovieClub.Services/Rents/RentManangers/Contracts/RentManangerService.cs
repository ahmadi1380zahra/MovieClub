using MovieClub.Services.Rents.RentManangers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Rents.RentManangers.Contracts
{
    public interface RentManangerService
    {
        Task Add(AddRentManangerDto dto);
        Task Update(int id, UpdateRentManangerDto dto);
    }
}

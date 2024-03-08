using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Users.UserMananger.Contracts
{
    public interface UserMananengerService
    {
        Task Add(AddUserManangerDto dto);
        Task Delete(int id);
        Task<List<GetUserManangerDto>?>  GetAll(GetUserManangerFilterDto? dto);
        Task Update(int id, UpdateUserManangerDto dto);
    }
}

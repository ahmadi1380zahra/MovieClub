using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Users.UserMananger.Contracts.Dtos
{
    public class GetUserManangerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}

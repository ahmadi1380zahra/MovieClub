using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Users.UserMananger.Contracts.Dtos
{
    public class AddUserManangerDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime Age { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
    }
}

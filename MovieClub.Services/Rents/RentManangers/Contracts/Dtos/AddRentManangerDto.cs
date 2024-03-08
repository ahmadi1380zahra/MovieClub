using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Rents.RentManangers.Contracts.Dtos
{
    public class AddRentManangerDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int FilmId { get; set; }
 

    }
}

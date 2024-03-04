using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Genres.GenreManagers.Contracts.Dtos
{
    public class UpdateGenreDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        //[Required]
        //public int Rate { get; set; }
    }
}

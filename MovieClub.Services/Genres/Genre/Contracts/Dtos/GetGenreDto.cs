using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Services.Genres.Genre.Contracts.Dtos
{
    public class GetGenreDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}

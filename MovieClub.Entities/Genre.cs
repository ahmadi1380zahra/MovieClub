using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Decimal Rate { get; set; }
        public HashSet<Film> Films { get; set; }
    }
}

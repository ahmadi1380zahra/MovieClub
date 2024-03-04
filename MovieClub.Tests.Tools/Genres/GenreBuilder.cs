using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Genres
{
    public class GenreBuilder
    {
        private readonly Genre _genre;
        public GenreBuilder()
        {
            _genre = new Genre
            {
                Title = "dummy-title",
                Rate = 0
            };
        }
        public GenreBuilder WithTitle(string title)
        {
            _genre.Title = title;
            return this;
        }
        public Genre Build()
        {
            return _genre;
        }
    }
}

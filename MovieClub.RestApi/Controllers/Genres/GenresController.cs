using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClub.Services.Genres.Genre.Contracts;
using MovieClub.Services.Genres.Genre.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;

namespace MovieClub.RestApi.Controllers.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GenreService _service;
        public GenresController(GenreService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<List<GetGenreDto>?> GetAll([FromQuery] GetGenreFilterDto? dto)
        {
            return await _service.GetAll(dto);
        }
    }
}

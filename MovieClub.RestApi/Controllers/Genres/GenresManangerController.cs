using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;

namespace MovieClub.RestApi.Controllers.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresManangerController : ControllerBase
    {
        private readonly GenreManangerService _service;
        public GenresManangerController(GenreManangerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody] AddGenreManangerDto dto)
        {
            await _service.Add(dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.Delete(id);
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] UpdateGenreManangerDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpGet]
        public async Task <List<GetGenreManangerDto>?> GetAll([FromQuery]GetGenreManangerFilterDto? dto)
        {
            return await _service.GetAll(dto);
        }
    }
}

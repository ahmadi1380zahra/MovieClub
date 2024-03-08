using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClub.Services.Genres.GenreManagers.Contracts.Dtos;
using MovieClub.Services.Genres.GenreManagers.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts;
using MovieClub.Services.Users.UserMananger.Contracts.Dtos;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;

namespace MovieClub.RestApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersManangerController : ControllerBase
    {

        private readonly UserMananengerService _service;
        public UsersManangerController(UserMananengerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody] AddUserManangerDto dto)
        {
            await _service.Add(dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.Delete(id);
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] UpdateUserManangerDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpGet]
        public async Task<List<GetUserManangerDto>?> GetAll([FromQuery] GetUserManangerFilterDto? dto)
        {
            return await _service.GetAll(dto);
        }
    }
}

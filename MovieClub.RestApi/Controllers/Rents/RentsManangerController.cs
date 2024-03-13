using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClub.Services.Rents.RentManangers.Contracts;
using MovieClub.Services.Rents.RentManangers.Contracts.Dtos;

namespace MovieClub.RestApi.Controllers.Rents
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsManangerController : ControllerBase
    {
        private readonly RentManangerService _service;
        public RentsManangerController(RentManangerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddRentManangerDto dto)
        {
            await _service.Add(dto); 
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute]int id,[FromBody] UpdateRentManangerDto dto)
        {
            await _service.Update(id,dto);
        }
    }
}

using EgyWonders.DTO;
using EgyWonders.IRepository;
using EgyWonders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenitiesRepository _Repo;
        public AmenityController(IAmenitiesRepository repo)
        {
            _Repo = repo;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var respone= await _Repo.GetAllAsync();
            
            if (respone!=null)
            {
                var result = respone.Select(b => new AmenityDTO
                {
                    AmenitiesId=b.AmenitiesId,
                   AmenityName=b.AmenityName
                });
                return Ok(result);
            }
            else
            {
                return NoContent();  
            }


        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var respone = await _Repo.GetByIdAsync( id);
            if (respone != null)
            {
                var result = respone.AmenityName;
               
                return Ok(result);
            }
            else
            {
                return NotFound($" ID {id} not found.");
            }


        }
        [HttpPost]
        public async Task<IActionResult>Create(AmenityDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var amen = new Amenity
            {
                AmenitiesId = dto.AmenitiesId,
                AmenityName = dto.AmenityName
            };
           await _Repo.AddAsync(amen);
            var respone = new
            {
                amen.AmenitiesId,
                amen.AmenityName
            };
            return Ok(respone);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ame=  await _Repo.GetByIdAsync(id);
            if (ame==null)
            {
                return NotFound($" ID {id} not found.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _Repo.DeleteAsync(id);
            return NoContent();
        }

    }
}

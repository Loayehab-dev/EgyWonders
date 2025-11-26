using EgyWonders.DTO;
using EgyWonders.IRepository;
using Microsoft.AspNetCore.Mvc;
using EgyWonders.Models;


namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserProfileController(IUserRepository repo)
        {
            _repo = repo;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repo.GetAllAsync();

            var response = users.Select(user => new UserCreateResponseDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                Gender = user.Gender,
                Phone = user.Phone,
                Nationality = user.Nationality,
                DateOfBirth = (DateOnly)user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt ?? DateTime.UtcNow
            });

            return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var response = new UserCreateResponseDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                Gender = user.Gender,
                Phone = user.Phone,
                Nationality = user.Nationality,
                DateOfBirth = (DateOnly)user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt ?? DateTime.UtcNow
            };

            return Ok(response);
        }

        // POST: api/Users
        //[HttpPost]
        //public async Task<IActionResult> Post(UserCreateDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    //var user = await _repo.CreateAsync(dto); // <-- use your method

        //    var response = new UserCreateResponseDTO
        //    {
        //        UserId = user.UserId,
        //        Email = user.Email,
        //        Gender = user.Gender,
        //        Phone = user.Phone,
        //        Nationality = user.Nationality,
        //        DateOfBirth = (DateOnly)user.DateOfBirth,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        CreatedAt = user.CreatedAt,
        //        UpdatedAt = user.UpdatedAt ?? DateTime.UtcNow
        //    };

        //    return Ok(response);
        //}


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // Update fields
            user.Email = dto.Email;
            user.Gender = dto.Gender;
            user.Phone = dto.Phone;
            user.Nationality = dto.Nationality;
            user.DateOfBirth = dto.DateOfBirth;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;




            // Update the timestamp
            user.UpdatedAt = DateTime.UtcNow;





            await _repo.UpdateAsync(user);

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });
            try
            {
                await _repo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the user.", error = ex.Message });
            }

        }
    }
}

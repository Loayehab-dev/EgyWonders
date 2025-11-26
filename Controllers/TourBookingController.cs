using Microsoft.AspNetCore.Mvc;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.DTO;

namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourBookingController : ControllerBase
    {
        private readonly ITourBookingRepository _repo;

        public TourBookingController(ITourBookingRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _repo.GetAllAsync();

            var result = bookings.Select(b => new TourBookingResponseDTO
            {
                BookingId = b.BookingId,
                TotalPrice = b.TotalPrice,
                Status = b.Status,
                NumParticipants = b.NumParticipants,
                UserId = b.UserId,
                ScheduleId = b.ScheduleId
              
            });

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var b = await _repo.GetByIdAsync(id);
            if (b == null) return NotFound();

            var result = new TourBookingResponseDTO
            {
                BookingId = b.BookingId,
                TotalPrice = b.TotalPrice,
                Status = b.Status,
                NumParticipants = b.NumParticipants,
                UserId = b.UserId,
                ScheduleId = b.ScheduleId
               
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourBookingDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = new TourBooking
            {
                TotalPrice = dto.TotalPrice,
                NumParticipants = dto.NumParticipants,
                UserId = dto.UserId,
                ScheduleId = dto.ScheduleId,
                Status = "Pending"
            };

            await _repo.AddAsync(booking);

            return Ok(booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTourBookingDTO dto)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) return NotFound();

            booking.TotalPrice = dto.TotalPrice;
            booking.NumParticipants = dto.NumParticipants;
            booking.UserId = dto.UserId;
            booking.ScheduleId = dto.ScheduleId;

            await _repo.UpdateAsync(booking);

            return Ok(booking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }
    }
}

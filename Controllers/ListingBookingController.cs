using EgyWonders.DTO;
using EgyWonders.IRepository;
using EgyWonders.Models;
using Microsoft.AspNetCore.Mvc;



namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingBookingController : ControllerBase
    {
        private readonly IListingBookingRepository _repository;
        public ListingBookingController(IListingBookingRepository repo) {
            _repository = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _repository.GetAllAsync();

            var result = bookings.Select(b => new ListingBookingDTO
            {
                BookingId = b.BookId,
                ListingId = b.ListingId,
                UserId = b.UserId,
                FromDate = b.CheckIn,
                ToDate = b.CheckOut,
                TotalPrice = b.TotalPrice
            });

            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");
            var dto = new ListingBookingDTO
            {
                BookingId = booking.BookId,
                ListingId = booking.ListingId,
                UserId = booking.UserId,
                FromDate = booking.CheckIn,
                ToDate = booking.CheckOut,
                TotalPrice = booking.TotalPrice
            };

            return Ok(dto);


        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateListingBookingDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (dto.FromDate >= dto.ToDate)
                return BadRequest("FromDate must be before ToDate.");

            var model = new ListingBooking
            {
                ListingId = dto.ListingId,
                UserId = dto.UserId,
                CheckIn = dto.FromDate,
                CheckOut = dto.ToDate,
                TotalPrice = 0 // calculate later
            };
            await _repository.AddAsync(model);
            var response = new
            {
                model
             .BookId,
                model.ListingId,
                model.UserId,
                FromDate = model.CheckIn,
                ToDate = model.CheckOut,
                model.TotalPrice
            };

            
            return CreatedAtAction(nameof(GetByID), new { id = model.BookId }, response);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateListingBookingDTO dto)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");

            if (dto.FromDate >= dto.ToDate)
                return BadRequest("FromDate must be before ToDate.");

            booking.CheckIn = dto.FromDate;
            booking.CheckOut = dto.ToDate;

            await _repository.UpdateAsync(booking);


            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _repository.GetByIdAsync(id);


            if (booking == null)
                return NotFound($" ID {id} not found.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.DeleteAsync(id);


            return NoContent();
        }

        //  SPECIAL: GET BOOKINGS FOR LISTING 
        [HttpGet("listing/{listingId:int}")]
        public async Task<IActionResult> GetByListing(int listingId)
        {
            var bookings = await _repository.GetByListingIdAsync(listingId);

            var result = bookings.Select(b => new ListingBookingDTO
            {
                BookingId = b.BookId,
                ListingId = b.ListingId,
                UserId = b.UserId,
                FromDate = b.CheckIn,
                ToDate = b.CheckOut,
                TotalPrice = b.TotalPrice
            });

            return Ok(result);
        }



    }}

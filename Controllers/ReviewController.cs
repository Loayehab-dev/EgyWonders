using EgyWonders.DTO;
using EgyWonders.IRepository;
using EgyWonders.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _repository;

        public ReviewController(IReviewRepository repository)
        {
            _repository = repository;
        }

        // GET: api/review
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _repository.GetAllAsync();

            var result = reviews.Select(r => new ReviewDTO
            {
                ReviewId = r.ReviewId,
                Comment = r.Comment,
                Rating = r.Rating,
                UserId = r.UserId,
                ListingId = r.ListingId,
                TourId = r.TourId
            });

            return Ok(result);
        }

        // GET: api/review/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _repository.GetByIdAsync(id);

            if (r == null)
                return NotFound($"Review with ID {id} not found.");

            var dto = new ReviewDTO
            {
                ReviewId = r.ReviewId,
                Comment = r.Comment,
                Rating = r.Rating,
                UserId = r.UserId,
                ListingId = r.ListingId,
                TourId = r.TourId
            };

            return Ok(dto);
        }

        // GET: api/review/listing/10
        [HttpGet("listing/{listingId:int}")]
        public async Task<IActionResult> GetByListing(int listingId)
        {
            var reviews = await _repository.GetByListingIdAsync(listingId);

            var result = reviews.Select(r => new ReviewDTO
            {
                ReviewId = r.ReviewId,
                Comment = r.Comment,
                Rating = r.Rating,
                UserId = r.UserId,
                ListingId = r.ListingId,
                TourId = r.TourId
            });

            return Ok(result);
        }

        // GET: api/review/tour/3
        [HttpGet("tour/{tourId:int}")]
        public async Task<IActionResult> GetByTour(int tourId)
        {
            var reviews = await _repository.GetByTourIdAsync(tourId);

            var result = reviews.Select(r => new ReviewDTO
            {
                ReviewId = r.ReviewId,
                Comment = r.Comment,
                Rating = r.Rating,
                UserId = r.UserId,
                ListingId = r.ListingId,
                TourId = r.TourId
            });

            return Ok(result);
        }

        // POST: api/review
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = new Review
            {
                Comment = dto.Comment,
                Rating = dto.Rating,
                UserId = dto.UserId,
                ListingId = dto.ListingId,
                TourId = dto.TourId
            };

            await _repository.AddAsync(review);

            var resultDto = new ReviewDTO
            {
                ReviewId = review.ReviewId,
                Comment = review.Comment,
                Rating = review.Rating,
                UserId = review.UserId,
                ListingId = review.ListingId,
                TourId = review.TourId
            };

            return CreatedAtAction(nameof(GetById), new { id = review.ReviewId }, resultDto);
        }

        // PUT: api/review/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateReviewDTO dto)
        {
            var review = await _repository.GetByIdAsync(id);

            if (review == null)
                return NotFound($"Review with ID {id} not found.");


            
            review.Comment = dto.Comment;
            review.Rating = dto.Rating;
            review.UserId = dto.UserId;
            review.ListingId = dto.ListingId;
            review.TourId = dto.TourId;

            await _repository.UpdateAsync(review);

            return NoContent();
        }

        // DELETE: api/review/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _repository.GetByIdAsync(id);

            if (exists == null)
                return NotFound($"Review with ID {id} not found.");

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}

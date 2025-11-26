using EgyWonders.DTO;
using EgyWonders.IRepository;
using EgyWonders.Models;
using Microsoft.AspNetCore.Mvc;


namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IListingRepository _Repo;
      
        public ListingController(IListingRepository repo) {
        _Repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var listings = await _Repo.GetAllAsync();
            if (listings.Count() == 0)
            {
                return NotFound("NO Lisitngs");
            }
            var result = listings.Select(l => new ListingDTO
                {
                      Title = l.Title,
                      Status =l.Status,
                      Category = l.Category,
                    Description = l.Description,
                    PricePerNight = l.PricePerNight,
                    Capacity = l.Capacity,
                    CityName = l.CityName,
                  
                });
            return Ok(result);

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var listing = await _Repo.GetByIdAsync(id);

            if (listing == null) return NotFound();

            var result = new ListingDTO
            {
              
                Title = listing.Title,
                Category = listing.Category,
                Description = listing.Description,
                Status=listing.Status,
                PricePerNight = listing.PricePerNight,
                Capacity = listing.Capacity,
                CityName = listing.CityName,
              
            };

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ListingDTO dto)
        {
            int userId = 4; //int.Parse(User.FindFirst("userId").Value);
            var listing = new Listing
            {
                Title = dto.Title,
                Description = dto.Description,
                PricePerNight = dto.PricePerNight,
                Category=dto.Category,
                Capacity = dto.Capacity,
                CityName = dto.CityName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };
            
            await _Repo.AddAsync(listing);



            return CreatedAtAction(nameof(GetById), new { id = listing.ListingId }, new
            {
                listing.ListingId,
                listing.Title,
                OwnerUserId = userId
            }); 
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ListingDTO dto)
        {
            int userId = 4;//int.Parse(User.FindFirst("userId").Value);

            // Load listing from DB
            var listing = await _Repo.GetByIdAsync(id);

            if (listing == null)
                return NotFound("Listing not found.");

            //// Ensure the logged-in user is the owner
            //if (listing.UserId != userId)
            //    return Unauthorized("You cannot update someone else's listing.");

            // Update allowed fields
            listing.Title = dto.Title ?? listing.Title;
            listing.Description = dto.Description ?? listing.Description;
            listing.Category = dto.Category ?? listing.Category;
            listing.Capacity = dto.Capacity != 0 ? dto.Capacity : listing.Capacity;
            listing.PricePerNight = dto.PricePerNight != 0 ? dto.PricePerNight : listing.PricePerNight;
            listing.CityName = dto.CityName ?? listing.CityName;

            await _Repo.UpdateAsync(listing);

            return Ok(new { message = "Listing updated successfully." });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
         //   int userId = 4; //int.Parse(User.FindFirst("userId")!.Value);

            var listing = await _Repo.GetByIdAsync(id);

            if (listing == null)
                return NotFound();

            //if (listing.UserId != userId)
            //    return Unauthorized("You cannot delete another user's listing.");

            await _Repo.DeleteAsync(id);

            return Ok(new { message = "Listing deleted successfully" });
        }





    }
}

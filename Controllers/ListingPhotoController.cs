using EgyWonders.DTO;
using EgyWonders.IRepository;
using EgyWonders.Models;
using Microsoft.AspNetCore.Mvc;


namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingPhotoController : ControllerBase
    {
        private readonly IListingPhotosRepository _repo;

        public ListingPhotoController(IListingPhotosRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("listing/{listingId}")]
        public async Task<IActionResult> GetPhotos(int listingId)
        {
            var photos = await _repo.GetByListingIdAsync(listingId);

            var response = photos.Select(p => new ListingPhotoDTO
            {
                PhotoId = p.PhotoId,
                Url = p.Url,
                Caption = p.Caption,
                ListingId = p.ListingId
            });

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photo = await _repo.GetByIdAsync(id);
            if (photo == null)
                return NotFound("Photo not found.");

            var dto = new ListingPhotoDTO
            {
                PhotoId = photo.PhotoId,
                Url = photo.Url,
                Caption = photo.Caption,
                ListingId = photo.ListingId
            };

            return Ok(dto);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ListingPhotoUploadDTO
            dto)
        {
            if (dto.Photo == null || dto.Photo.Length == 0)
                return BadRequest("Upload a valid photo.");

            string folderPath = Path.Combine("wwwroot", "listing-photos");
            Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid() + Path.GetExtension(dto.Photo.FileName);
            string fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await dto.Photo.CopyToAsync(stream);
            }

            var photo = new ListingPhoto
            {
                Url = "/listing-photos/" + fileName,
                Caption = dto.Caption,
                ListingId = dto.ListingId
            };

            await _repo.AddAsync(photo);

            return Ok(photo);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromBody] ListingPhotoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var photo = await _repo.GetByIdAsync(id);
            if (photo == null)
                return NotFound("Photo not found.");

            photo.Url = dto.Url;
            photo.Caption = dto.Caption;
            photo.ListingId = dto.ListingId;

            await _repo.UpdateAsync(photo);

            return Ok("Photo updated successfully.");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _repo.GetByIdAsync(id);
            if (photo == null)
                return NotFound("Photo not found.");

            await _repo.DeleteAsync(id);

            return Ok("Photo deleted successfully.");
        }


    }
}

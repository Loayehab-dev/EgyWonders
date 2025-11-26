using EgyWonders.DTO;
using EgyWonders.IRepository;
using EgyWonders.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostDocumentController : ControllerBase
    {
        private readonly IHostDocumentRepository _repo;

        public HostDocumentController(IHostDocumentRepository repo)
        {
            _repo = repo;
        }

        private int GetUserId()
        {
            // Replace this with actual authentication logic
            return 4;
        }

        // GET /api/hostdocument
        [HttpGet]
        public async Task<IActionResult> GetMyDocuments()
        {
            var userId = GetUserId();

            var docs = await _repo.GetByUserIdAsync(userId);

            var response = docs.Select(d => new HostDocumentDTO
            {
                DocumentId = d.DocumentId,
                DocumentPath = d.DocumentPath,
                TextRecord = d.TextRecord,
                NationalId = d.NationalId,
                CreatedAt = d.CreatedAt,
                Verified = d.Verified
            });

            return Ok(response);
        }

        // GET /api/hostdocument/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var userId = GetUserId();

            var doc = await _repo.GetByIdAsync(id);

            if (doc == null)
                return NotFound("Document not found");

            if (doc.UserId != userId)
                return Unauthorized("You do not own this document.");

            var dto = new HostDocumentDTO
            {
                DocumentId = doc.DocumentId,
                DocumentPath = doc.DocumentPath,
                TextRecord = doc.TextRecord,
                NationalId = doc.NationalId,
                CreatedAt = doc.CreatedAt,
                Verified = doc.Verified
            };

            return Ok(dto);
        }

        // POST /api/hostdocument/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadHostDocument([FromForm] UploadHostDocumentDTO dto)
        {
            var userId = GetUserId();

            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded.");

            // Save file in local folder
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "host-documents");
            Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
            string fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var doc = new HostDocument
            {
                DocumentPath = $"/host-documents/{fileName}",
                TextRecord = dto.TextRecord,
                NationalId = dto.NationalId,
                CreatedAt = DateTime.UtcNow,
                Verified = false,
                UserId = userId
            };

            await _repo.AddAsync(doc);

            var response = new HostDocumentDTO
            {
                DocumentId = doc.DocumentId,
                DocumentPath = doc.DocumentPath,
                TextRecord = doc.TextRecord,
                NationalId = doc.NationalId,
                Verified = doc.Verified,
                CreatedAt = doc.CreatedAt
            };

            return Ok(response);
        }

        // DELETE /api/hostdocument/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var userId = GetUserId();

            var doc = await _repo.GetByIdAsync(id);

            if (doc == null)
                return NotFound("Document not found");

            if (doc.UserId != userId)
                return Unauthorized("You cannot delete someone else's document.");

            await _repo.DeleteAsync(id);

            return Ok("Document deleted successfully.");
        }
    }
}

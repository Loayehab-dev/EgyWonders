using Azure;
using EgyWonders.DTO;
using EgyWonders.IRepository;
using EgyWonders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideCertificationController : ControllerBase
    {
        private readonly IGuideCertificationRepository _guideCertificationRepository;
        public GuideCertificationController(IGuideCertificationRepository guideCertificationRepository)
        {
            _guideCertificationRepository = guideCertificationRepository;
        }
        private int GetUserId()
        {
            // Replace this with actual authentication logic
            return 4;
        }
        [HttpGet]
        public async Task<IActionResult> GetGuideCertificates()
        {
            var userId = GetUserId();

            var docs = await _guideCertificationRepository.GetByUserIdAsync(userId);

            var response = docs.Select(d => new GuideCertificateDTO
            {
                GuideId = userId,
                IssueDate = d.IssueDate,
                ExpiryDate = d.ExpiryDate,
                CertificationName = d.CertificationName


            });
            return Ok(response);
        }


            [HttpPost("upload")]
            public async Task<IActionResult> UploadGuideCertifcate([FromForm] UploadGuideCertificationDTO dto)
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

                var doc = new GuideCertification
                {
                    GuideId = userId,
                    CertificationName =dto.CertificationName,
                    IssueDate=dto.IssueDate,
                    ExpiryDate=dto.ExpiryDate

                    
                };

                await _guideCertificationRepository.AddAsync(doc);

                var response = new GuideCertificateDTO
                {
                    GuideId = userId,
                    CertificationName = dto.CertificationName,
                    IssueDate = dto.IssueDate,
                    ExpiryDate = dto.ExpiryDate
                };

                return Ok(response);
            }
        // GET /api/hostdocument/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var userId = GetUserId();

            var doc =  await _guideCertificationRepository.GetByIdAsync(id);

            if (doc == null)
                return NotFound("Document not found");

            if (doc.GuideId != userId)
                return Unauthorized("You do not own this document.");

            var dto = new GuideCertificateDTO
            {
                CertificationName = doc.CertificationName,
                IssueDate = doc.IssueDate,
                ExpiryDate = doc.ExpiryDate

            };

            return Ok(dto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var userId = GetUserId();

            var doc = await _guideCertificationRepository.GetByIdAsync(id);

            if (doc == null)
                return NotFound("Document not found");

            if (doc.GuideId != userId)
                return Unauthorized("You cannot delete someone else's document.");

            await _guideCertificationRepository.DeleteAsync(id);

            return Ok("Document deleted successfully.");
        }

    }
}

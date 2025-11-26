using EgyWonders.DTO;
using EgyWonders.IRepository;
using EgyWonders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgyWonders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourScheduleController : ControllerBase
    {
        private readonly ITourSchedulesRepository _scheduleRepo;

        public TourScheduleController(ITourSchedulesRepository scheduleRepo)
        {
            _scheduleRepo = scheduleRepo;
        }

        // GET: api/TourSchedule
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schedules = await _scheduleRepo.GetAllAsync();
            
            var scheduleDtos = schedules.Select(s => new TourScheduleSummaryDTO
            {
                ScheduleId = s.ScheduleId,
                TourId = s.TourId,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                MaxParticipants = s.MaxParticipants,
                CurrentBooked = s.CurrentBooked ?? 0,
                AvailableSpots = s.MaxParticipants - (s.CurrentBooked ?? 0),
              
                IsAvailable = (s.CurrentBooked ?? 0) < s.MaxParticipants
            }).OrderBy(s => s.Date).ThenBy(s => s.StartTime);

            return Ok(scheduleDtos);
        }

        // GET: api/TourSchedule/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var schedule = await _scheduleRepo.GetByIdAsync(id);

            if (schedule == null)
                return NotFound(new { message = $"Schedule with ID {id} not found" });

            var scheduleDto = new TourScheduleDTO
            {
                ScheduleId = schedule.ScheduleId,
                TourId = schedule.TourId,
                Date = schedule.Date,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                MaxParticipants = schedule.MaxParticipants,
                CurrentBooked = schedule.CurrentBooked ?? 0,
                AvailableSpots = schedule.MaxParticipants - (schedule.CurrentBooked ?? 0)
               
                
                
            };

            return Ok(scheduleDto);
        }

        // GET: api/TourSchedule/tour/5
        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetByTourId(int tourId)
        {
            var schedules = await _scheduleRepo.GetByTourIdAsync(tourId);

            var scheduleDtos = schedules.Select(s => new TourScheduleSummaryDTO
            {
                ScheduleId = s.ScheduleId,
                TourId = s.TourId,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                MaxParticipants = s.MaxParticipants,
                CurrentBooked = s.CurrentBooked ?? 0,
                AvailableSpots = s.MaxParticipants - (s.CurrentBooked ?? 0),
                
                IsAvailable = (s.CurrentBooked ?? 0) < s.MaxParticipants
            }).OrderBy(s => s.Date).ThenBy(s => s.StartTime);

            return Ok(scheduleDtos);
        }

        // GET: api/TourSchedule/available
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var schedules = await _scheduleRepo.GetAllAsync();

            var availableSchedules = schedules
                .Where(s => s.Date >= today && (s.CurrentBooked ?? 0) < s.MaxParticipants)
                .Select(s => new TourScheduleSummaryDTO
                {
                    ScheduleId = s.ScheduleId,
                    TourId = s.TourId,
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    MaxParticipants = s.MaxParticipants,
                    CurrentBooked = s.CurrentBooked ?? 0,
                    AvailableSpots = s.MaxParticipants - (s.CurrentBooked ?? 0),
                  
                    IsAvailable = true
                }).OrderBy(s => s.Date).ThenBy(s => s.StartTime);

            return Ok(availableSchedules);
        }

        // GET: api/TourSchedule/upcoming
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming([FromQuery] DateOnly? fromDate)
        {
            var startDate = fromDate ?? DateOnly.FromDateTime(DateTime.Now);
            var schedules = await _scheduleRepo.GetAllAsync();

            var upcomingSchedules = schedules
                .Where(s => s.Date >= startDate)
                .Select(s => new TourScheduleSummaryDTO
                {
                    ScheduleId = s.ScheduleId,
                    TourId = s.TourId,
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    MaxParticipants = s.MaxParticipants,
                    CurrentBooked = s.CurrentBooked ?? 0,
                    AvailableSpots = s.MaxParticipants - (s.CurrentBooked ?? 0),
                 
                    IsAvailable = (s.CurrentBooked ?? 0) < s.MaxParticipants
                }).OrderBy(s => s.Date).ThenBy(s => s.StartTime);

            return Ok(upcomingSchedules);
        }

        // POST: api/TourSchedule
        [HttpPost]
        public async Task<IActionResult> Create(TourScheduleCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate end time is after start time
            if (dto.EndTime <= dto.StartTime)
                return BadRequest(new { message = "End time must be after start time" });

            // Validate date is not in the past
            if (dto.Date < DateOnly.FromDateTime(DateTime.Now))
                return BadRequest(new { message = "Cannot create schedule for past dates" });

            var schedule = new TourSchedule
            {
                TourId = dto.TourId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                MaxParticipants = dto.MaxParticipants,
                CurrentBooked = 0
            };

            await _scheduleRepo.AddAsync(schedule);

            // Return DTO instead of entity
            var createdSchedule = await _scheduleRepo.GetByIdAsync(schedule.ScheduleId);

            var scheduleDto = new TourScheduleDTO
            {
                ScheduleId = createdSchedule.ScheduleId,
                TourId = createdSchedule.TourId,
                Date = createdSchedule.Date,
                StartTime = createdSchedule.StartTime,
                EndTime = createdSchedule.EndTime,
                MaxParticipants = createdSchedule.MaxParticipants,
                CurrentBooked = createdSchedule.CurrentBooked ?? 0,
                AvailableSpots = createdSchedule.MaxParticipants - (createdSchedule.CurrentBooked ?? 0),
                
                
              
            };

            return CreatedAtAction(nameof(Get), new { id = schedule.ScheduleId }, scheduleDto);
        }

        // PUT: api/TourSchedule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TourScheduleUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSchedule = await _scheduleRepo.GetByIdAsync(id);
            if (existingSchedule == null)
                return NotFound(new { message = $"Schedule with ID {id} not found" });

            // Validate end time is after start time
            if (dto.EndTime <= dto.StartTime)
                return BadRequest(new { message = "End time must be after start time" });

            // Check if reducing max participants below current bookings
            if (dto.MaxParticipants < (existingSchedule.CurrentBooked ?? 0))
                return BadRequest(new { message = $"Cannot reduce max participants below current bookings ({existingSchedule.CurrentBooked})" });

            // Create entity for update
            var schedule = new TourSchedule
            {
                ScheduleId = id,
                TourId = existingSchedule.TourId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                MaxParticipants = dto.MaxParticipants,
                CurrentBooked = existingSchedule.CurrentBooked
            };

            await _scheduleRepo.UpdateAsync(schedule);

            // Return DTO
            var updatedSchedule = await _scheduleRepo.GetByIdAsync(id);
            var scheduleDto = new TourScheduleDTO
            {
                ScheduleId = updatedSchedule.ScheduleId,
                TourId = updatedSchedule.TourId,
                Date = updatedSchedule.Date,
                StartTime = updatedSchedule.StartTime,
                EndTime = updatedSchedule.EndTime,
                MaxParticipants = updatedSchedule.MaxParticipants,
                CurrentBooked = updatedSchedule.CurrentBooked ?? 0,
                AvailableSpots = updatedSchedule.MaxParticipants - (updatedSchedule.CurrentBooked ?? 0)
                
                
            };

            return Ok(scheduleDto);
        }

        // DELETE: api/TourSchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await _scheduleRepo.GetByIdAsync(id);
            if (schedule == null)
                return NotFound(new { message = $"Schedule with ID {id} not found" });

            // Check if schedule has bookings
            if (schedule.CurrentBooked > 0)
                return BadRequest(new { message = "Cannot delete schedule with existing bookings" });

            await _scheduleRepo.DeleteAsync(id);

            return NoContent();
        }

        // GET: api/TourSchedule/5/capacity
        [HttpGet("{id}/capacity")]
        public async Task<IActionResult> GetCapacity(int id)
        {
            var schedule = await _scheduleRepo.GetByIdAsync(id);
            if (schedule == null)
                return NotFound(new { message = $"Schedule with ID {id} not found" });

            var capacity = new
            {
                scheduleId = schedule.ScheduleId,
                maxParticipants = schedule.MaxParticipants,
                currentBooked = schedule.CurrentBooked ?? 0,
                availableSpots = schedule.MaxParticipants - (schedule.CurrentBooked ?? 0),
                isAvailable = (schedule.CurrentBooked ?? 0) < schedule.MaxParticipants,
                utilizationPercentage = schedule.MaxParticipants > 0
                    ? Math.Round(((schedule.CurrentBooked ?? 0) * 100.0 / schedule.MaxParticipants), 2)
                    : 0
            };

            return Ok(capacity);
        }
    }
}
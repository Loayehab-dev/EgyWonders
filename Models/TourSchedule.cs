using System;
using System.Collections.Generic;

namespace EgyWonders.Models;

public partial class TourSchedule
{
    public int ScheduleId { get; set; }

    public int? CurrentBooked { get; set; }

    public TimeOnly EndTime { get; set; }

    public TimeOnly StartTime { get; set; }

    public DateOnly Date { get; set; }

    public int MaxParticipants { get; set; }

    public int TourId { get; set; }

    public virtual Tour Tour { get; set; } = null!;

    public virtual ICollection<TourBooking> TourBookings { get; set; } = new List<TourBooking>();
}

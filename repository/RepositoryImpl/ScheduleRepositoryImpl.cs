using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository;

public class ScheduleRepositoryImpl : IScheduleRepository
{
    private ServiceContext _context;

    public ScheduleRepositoryImpl(ServiceContext context)
    {
      _context = context;
    }
    async public Task<Schedule?> GetSchedule(int doctorID, DateOnly date)
    {
        var schedule = await _context.Schedules.FirstOrDefaultAsync(
            sch => sch.DoctorID == doctorID && sch.Date == date
        );

        if (schedule is null) return null;

        return new Schedule(
            schedule.DoctorID,
            schedule.Date,
            schedule.DayStart,
            schedule.DayEnd
        );
    }
    async public Task<Schedule?> AddSchedule(ScheduleForm form)
    {
        await _context.Schedules.AddAsync(
            new ScheduleModel
            {
              DoctorID = form.DoctorID,
              Date = form.Date,
              DayStart = form.DayStart,
              DayEnd = form.DayEnd
            }
        );
        await _context.SaveChangesAsync();

        var schedule = await _context.Schedules.FirstOrDefaultAsync(
            sch => sch.DoctorID == form.DoctorID &&
            sch.Date == form.Date && sch.DayStart == form.DayStart &&
            sch.DayEnd == sch.DayEnd
        );

        if (schedule is null) return null;

        return new Schedule(
            schedule.DoctorID,
            schedule.Date,
            schedule.DayStart,
            schedule.DayEnd
        );
    }
    async public Task<Schedule?> ChangeSchedule(ScheduleForm actual, ScheduleForm recent)
    {
        var schedule = await _context.Schedules.FirstOrDefaultAsync(
            sch => sch.DoctorID == actual.DoctorID &&
            sch.Date == actual.Date && sch.DayStart == actual.DayStart &&
            sch.DayEnd == actual.DayEnd
        );

        if (schedule is not null)
        {
            schedule.DoctorID = recent.DoctorID;
            schedule.Date = recent.Date;
            schedule.DayStart = recent.DayStart;
            schedule.DayEnd = recent.DayEnd;
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
        }
        else
        {
          return null;
        }

        return new Schedule(
            schedule.DoctorID,
            schedule.Date,
            schedule.DayStart,
            schedule.DayEnd
        );
    }
}
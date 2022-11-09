using Domain;

namespace Repository;

public class ScheduleRepositoryImpl : IScheduleRepository
{
    private ServiceContext _context;

    public ScheduleRepositoryImpl(ServiceContext context)
    {
      _context = context;
    }
    public Schedule? GetSchedule(int doctorID, DateOnly date)
    {
      var schedule = _context.Schedules.FirstOrDefault(sch => sch.DoctorID == doctorID && sch.Date == date);

      if (schedule is null) return null;

      return new Schedule(
          schedule.DoctorID,
          schedule.Date,
          schedule.DayStart,
          schedule.DayEnd
      );
    }
    public Schedule? AddSchedule(ScheduleForm form)
    {
      int newID = _context.Schedules.Count() == 0 ? 1 : _context.Schedules.Last().ID;
      _context.Schedules.Append(
          new ScheduleModel
          {
            ID = newID,
            DoctorID = form.DoctorID,
            Date = form.Date,
            DayStart = form.DayStart,
            DayEnd = form.DayEnd
          }
      );

      var schedule = _context.Schedules.FirstOrDefault(sch => sch.ID == newID);

      if (schedule is null) return null;

      return new Schedule(
          schedule.DoctorID,
          schedule.Date,
          schedule.DayStart,
          schedule.DayEnd
      );
    }
    public Schedule? ChangeSchedule(ScheduleForm form)
    {
      var schedule = _context.Schedules.FirstOrDefault(sch => sch.DoctorID == form.DoctorID &&
          sch.Date == form.Date && sch.DayStart == form.DayStart && sch.DayEnd == form.DayEnd);

      if (schedule is not null)
      {
          schedule.DoctorID = form.DoctorID;
          schedule.Date = form.Date;
          schedule.DayStart = form.DayStart;
          schedule.DayEnd = form.DayEnd;
          _context.Schedules.Update(schedule);
          _context.SaveChanges();
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
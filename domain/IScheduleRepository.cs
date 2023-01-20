namespace Domain;

public interface IScheduleRepository
{
    Task<Schedule?> GetSchedule(int doctorID, DateOnly date);
    Task<Schedule?> AddSchedule(ScheduleForm form);
    Task<Schedule?> ChangeSchedule(ScheduleForm actual, ScheduleForm recent);
}
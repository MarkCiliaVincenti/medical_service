namespace Domain;

public interface IScheduleRepository
{
    Schedule? GetSchedule(int doctorID, DateOnly date);
    Schedule? AddSchedule(ScheduleForm form);
    Schedule? ChangeSchedule(ScheduleForm form);
}
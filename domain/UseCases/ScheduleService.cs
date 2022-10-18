namespace Domain;

public class ScheduleService
{
    public readonly IScheduleRepository _repository;

    public ScheduleService(IScheduleRepository repository)
    {
        _repository = repository;
    }

    Result<Schedule> GetSchedule(int doctorID, DateOnly date)
    {
        var schedule = _repository.GetSchedule(doctorID, date);

        if (schedule is not null)
            return Result.Ok<Schedule>(schedule);

        return Result.Err<Schedule>("Schedule not found");
    }

    Result<Schedule> AddSchedule(ScheduleForm form)
    {
        var schedule = _repository.AddSchedule(form);

        if (schedule is not null)
            return Result.Ok<Schedule>(schedule);

        return Result.Err<Schedule>("Failed to add schedule");
    }
    Result<Schedule> ChangeSchedule(ScheduleForm form)
    {
        var schedule = _repository.ChangeSchedule(form);

        if (schedule is not null)
            return Result.Ok<Schedule>(schedule);

        return Result.Err<Schedule>("Failed to change schedule");
    }
}
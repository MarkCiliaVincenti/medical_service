namespace Domain;

public class ScheduleService
{
    public readonly IScheduleRepository _repository;
    private SemaphoreSlim scheduleSemaphore = new SemaphoreSlim(1, 1);

    public ScheduleService(IScheduleRepository repository)
    {
        _repository = repository;
    }

    async public Task<Result<Schedule>> GetSchedule(int doctorID, DateOnly date)
    {
        Schedule? schedule = null;
        try
        {
            await scheduleSemaphore.WaitAsync();

            schedule = await _repository.GetSchedule(doctorID, date);
        }
        finally
        {
            scheduleSemaphore.Release();
        }

        if (schedule is not null)
            return Result.Ok<Schedule>(schedule);

        return Result.Err<Schedule>("Schedule not found");
    }

    async public Task<Result<Schedule>> AddSchedule(ScheduleForm form)
    {
        Schedule? schedule = null;
        try
        {
            await scheduleSemaphore.WaitAsync();

            schedule = await _repository.AddSchedule(form);
        }
        finally
        {
            scheduleSemaphore.Release();
        }

        if (schedule is not null)
            return Result.Ok<Schedule>(schedule);

        return Result.Err<Schedule>("Failed to add schedule");
    }
    async public Task<Result<Schedule>> ChangeSchedule(ScheduleForm actual, ScheduleForm recent)
    {
        Schedule? schedule = null;
        try
        {
            await scheduleSemaphore.WaitAsync();

            schedule = await _repository.ChangeSchedule(actual, recent);
        }
        finally
        {
            scheduleSemaphore.Release();
        }

        if (schedule is not null)
            return Result.Ok<Schedule>(schedule);

        return Result.Err<Schedule>("Failed to change schedule");
    }
}
namespace Domain;

public class AppointmentService
{
    public readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Appointment> CreateAppointment(DateTime date, int doctorID)
    {
        var appointment = _repository.CreateAppointment(date, doctorID);

        if (appointment is not null)
            return Result.Ok<Appointment>(appointment);

        return Result.Err<Appointment>("Failed to create appointment");
    }

    public Result<Appointment> CreateAppointment(DateTime date)
    {
        var appointment = _repository.CreateAppointment(date);

        if (appointment is not null)
            return Result.Ok<Appointment>(appointment);

        return Result.Err<Appointment>("Failed to create appointment");
    }

    public Result<List<DateTime>> GetFreeDates(string specialization)
    {
        if (string.IsNullOrEmpty(specialization))
            return Result.Err<List<DateTime>>("Specialization not specified");

        var freeDates = _repository.GetFreeDates(specialization);

        if (freeDates is not null)
            return Result.Ok<List<DateTime>>(freeDates);

        return Result.Err<List<DateTime>>("Free dates not found");
    }
}
namespace Domain;

public class AppointmentService
{
    public readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Appointment> CreateAppointment(AppointmentForm form)
    {
        var appointment = _repository.CreateAppointment(form);

        if (appointment is not null)
            return Result.Ok<Appointment>(appointment);

        return Result.Err<Appointment>("Failed to create appointment");
    }

    public Result<List<Appointment>> GetFreeDates(string specialization)
    {
        if (string.IsNullOrEmpty(specialization))
            return Result.Err<List<Appointment>>("Specialization not specified");

        var freeDates = _repository.GetFreeDates(specialization);

        if (freeDates is not null)
            return Result.Ok<List<Appointment>>(freeDates);

        return Result.Err<List<Appointment>>("Free dates not found");
    }
}
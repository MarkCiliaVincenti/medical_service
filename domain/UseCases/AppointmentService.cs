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
        if (form.DoctorID == AppointmentForm.EmptyDoctorID)
        {
            //  обработать случай EmptyDoctorID
        }

        if (form.Specialization == string.Empty)
            return Result.Err<Appointment>("Specialization not specified");

        if (_repository.AppointmentExists(form))
            return Result.Err<Appointment>("Appointment with this doctor for this date already exists");
        
        var appointment = _repository.CreateAppointment(form);

        if (appointment is not null)
            return Result.Ok<Appointment>(appointment);

        return Result.Err<Appointment>("Failed to create appointment");
    }

    public Result<List<(DateTime, DateTime)>> GetFreeDates(string specialization, DateOnly date)
    {
        if (string.IsNullOrEmpty(specialization))
            return Result.Err<List<(DateTime, DateTime)>>("Specialization not specified");

        var busyDates = _repository.GetAllDates(specialization, date);

        var start = date.ToDateTime(new TimeOnly(0, 0, 0));
        var end = date.ToDateTime(new TimeOnly(23, 59, 59));

        if (busyDates.Count == 0)
            return Result.Ok(new List<(DateTime, DateTime)>{(start, end)});

        var freeDates = new List<(DateTime, DateTime)>();
        var lastDate = (start, start);

        foreach(var currentDate in busyDates)
        {
            freeDates.Add((lastDate.Item2, currentDate.Item1));
            lastDate = currentDate;
        }

        if (busyDates.Last().Item2 != end)
            freeDates.Add((busyDates.Last().Item2, end));

        return Result.Ok<List<(DateTime, DateTime)>>(freeDates);
    }
}
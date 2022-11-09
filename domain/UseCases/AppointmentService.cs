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
        if (form.DoctorID == -0xdeadb0b)
        {
            //  обработать случай 0xdeadb0b -- отдельно в форме прописать специализацию?
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

    public Result<List<DateTime>> GetFreeDates(string specialization)
    {
        if (string.IsNullOrEmpty(specialization))
            return Result.Err<List<DateTime>>("Specialization not specified");

        var busyDates = _repository.GetAllDates(specialization);

        var allDates = new List<DateTime>();
        for(int i = 0; i < 30; ++i) // диапазон дат, хардкод
        {
            allDates.Append(DateTime.Now.AddDays(i));
        }

        if (busyDates is null)
            return Result.Ok<List<DateTime>>(allDates);

        var freeDates = new List<DateTime>();
        foreach(var date in allDates)
        {
            if (!busyDates.Contains(date))
            {
                freeDates.Append(date);
            }
        }

        if (freeDates.Count != 0)
            return Result.Ok<List<DateTime>>(freeDates);

        return Result.Err<List<DateTime>>("Free dates not found"); 
    }
}
namespace Domain;

public interface IAppointmentRepository
{
    Task<Appointment?> CreateAppointment(AppointmentForm form);
    Task<List<(DateTime, DateTime)>> GetAllDates(string specialization, DateOnly date);
    Task<bool> AppointmentExists(AppointmentForm form);
}
namespace Domain;

public interface IAppointmentRepository
{
    Appointment? CreateAppointment(AppointmentForm form);
    List<(DateTime, DateTime)> GetAllDates(string specialization, DateOnly date);
    bool AppointmentExists(AppointmentForm form);
}
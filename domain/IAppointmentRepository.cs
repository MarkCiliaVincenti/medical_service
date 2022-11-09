namespace Domain;

public interface IAppointmentRepository
{
    Appointment? CreateAppointment(AppointmentForm form);
    List<DateTime>? GetAllDates(string specialization);
    bool AppointmentExists(AppointmentForm form);
}
namespace Domain;

public interface IAppointmentRepository
{
    Appointment? CreateAppointment(DateTime date, int doctorID);
    Appointment? CreateAppointment(DateTime date);
    List<DateTime>? GetFreeDates(string specialization);
}
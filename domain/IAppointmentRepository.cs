namespace Domain;

public interface IAppointmentRepository
{
    Appointment? CreateAppointment(AppointmentForm form);

    List<Appointment>? GetFreeDates(string specialization);
}
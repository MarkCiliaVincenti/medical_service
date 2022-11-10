using Domain;

namespace Repository;

public class AppointmentRepositoryImpl : IAppointmentRepository
{
    private ServiceContext _context;

    public AppointmentRepositoryImpl(ServiceContext context)
    {
      _context = context;
    }
    public Appointment? CreateAppointment(AppointmentForm form)
    {
      var appointment = new AppointmentModel
      {
          Start = form.Start,
          End = form.End,
          PatientID = form.PatientID,
          DoctorID = form.DoctorID
      };
      _context.Appointments.Add(appointment);
      _context.SaveChanges();

      var check = _context.Appointments.FirstOrDefault(ap => ap.DoctorID == form.DoctorID &&
          ap.PatientID == form.PatientID && ap.Start == form.Start && ap.End == form.End);

      if (check is null) return null;

      return new Appointment(
          check.Start,
          check.End,
          check.PatientID,
          check.DoctorID
      );
    }
    public bool AppointmentExists(AppointmentForm form)
    {
      var appointment = _context.Appointments.FirstOrDefault(ap => ap.DoctorID == form.DoctorID &&
          ap.PatientID == form.PatientID && ap.Start == form.Start && ap.End == form.End);
      
      if (appointment is null) return false;

      return true;
    }

    public List<(DateTime, DateTime)> GetAllDates(string specialization, DateOnly date)
    {
      var dateTime = date.ToDateTime(new TimeOnly(0, 0, 0));
      return _context.Appointments
          .Where(ap => ap.Specialization == specialization && ap.Start.Date == dateTime)
          .Select(ap => new Tuple<DateTime, DateTime>(ap.Start, ap.End).ToValueTuple()).OrderBy(ap => ap.Item2).ToList();
    }
}
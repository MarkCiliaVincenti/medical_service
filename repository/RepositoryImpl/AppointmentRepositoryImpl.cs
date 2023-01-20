using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository;

public class AppointmentRepositoryImpl : IAppointmentRepository
{
    private ServiceContext _context;

    public AppointmentRepositoryImpl(ServiceContext context)
    {
      _context = context;
    }
    async public Task<Appointment?> CreateAppointment(AppointmentForm form)
    {
        var appointment = new AppointmentModel
        {
            Start = form.Start,
            End = form.End,
            PatientID = form.PatientID,
            DoctorID = form.DoctorID
        };
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();

        var check = await _context.Appointments.FirstOrDefaultAsync(
            ap => ap.DoctorID == form.DoctorID &&
            ap.PatientID == form.PatientID && ap.Start == form.Start &&
            ap.End == form.End
        );

        if (check is null) return null;

        return new Appointment(
            check.Start,
            check.End,
            check.PatientID,
            check.DoctorID
        );
    }
    async public Task<bool> AppointmentExists(AppointmentForm form)
    {
        var appointment = await _context.Appointments.FirstOrDefaultAsync(ap => ap.DoctorID == form.DoctorID &&
            ap.PatientID == form.PatientID && ap.Start == form.Start && ap.End == form.End);
        
        if (appointment is null) return false;

        return true;
    }

    async public Task<List<(DateTime, DateTime)>> GetAllDates(string specialization, DateOnly date)
    {
        var dateTime = date.ToDateTime(new TimeOnly(0, 0, 0));
        return await _context.Appointments
            .Where(ap => ap.Specialization == specialization && ap.Start.Date == dateTime)
            .Select(ap => new Tuple<DateTime, DateTime>(ap.Start, ap.End).ToValueTuple())
            .OrderBy(ap => ap.Item2)
            .ToListAsync();
    }
}
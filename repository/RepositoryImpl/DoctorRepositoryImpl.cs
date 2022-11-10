using Domain;

namespace Repository;

public class DoctorRepositoryImpl : IDoctorRepository
{
    private ServiceContext _context;

    public DoctorRepositoryImpl(ServiceContext context)
    {
      _context = context;
    }
    public Doctor? CreateDoctor(DoctorForm form)
    {
        _context.Doctors.Append(
            new DoctorModel
            {
                FullName = form.FullName,
                Specialization = form.Specialization
            }
        );

        var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.FullName == form.FullName &&
            doctor.Specialization == form.Specialization);

        if (doctor is null) return null;

        return new Doctor(
            doctor.ID,
            doctor.FullName,
            doctor.Specialization
        );
    }
    public bool DeleteDoctor(int id)
    {
      var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.ID == id);

      if (doctor is null) return false;
      
      var result = _context.Doctors.Remove(doctor);

      return result is null ? false : true;
      
    }
    public Doctor? GetDoctorByID(int id)
    {
        var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.ID == id);

        if (doctor is null) return null;

        return new Doctor(doctor.ID, doctor.FullName, doctor.Specialization);
    }
    public List<Doctor> GetDoctorsBySpecialization(string specialization)
        => _context.Doctors
                .Where(doctor => doctor.Specialization == specialization)
                .Select(doctor => new Doctor(doctor.ID, doctor.FullName, doctor.Specialization))
                .ToList();

    public List<Doctor> GetAllDoctors()
        => _context.Doctors
                .Select(doctor => new Doctor(doctor.ID, doctor.FullName, doctor.Specialization))
                .ToList();
}
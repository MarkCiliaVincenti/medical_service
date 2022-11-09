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
        int newID = _context.Doctors.Count() == 0 ? 1 : _context.Doctors.Last().ID;
        _context.Doctors.Append(
            new DoctorModel
            {
                ID = newID,
                FullName = form.FullName,
                Specialization = form.Specialization
            }
        );

        var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.ID == newID);

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
    public List<Doctor>? GetDoctorsBySpecialization(string specialization)
    {
        var doctors = _context.Doctors.Where(doctor => doctor.Specialization == specialization).ToList();

        if (doctors.Count == 0) return null;

        List<Doctor> result = new List<Doctor>();
        foreach(var doctor in doctors)
        {
            result.Append(new Doctor(doctor.ID, doctor.FullName, doctor.Specialization));
        }

        return result;
    }
    public List<Doctor>? GetAllDoctors()
    {
        var doctors = _context.Doctors.ToList();

        if (doctors.Count == 0) return null;

        List<Doctor> result = new List<Doctor>();
        foreach(DoctorModel doctor in doctors)
        {
            result.Append(new Doctor(doctor.ID, doctor.FullName, doctor.Specialization));
        }

        return result;
    }
}
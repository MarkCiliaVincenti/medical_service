using Domain;

namespace Repository;

public class DoctorRepositoryImpl : IDoctorRepository
{
    private ServiceContext _context;

    public DoctorRepositoryImpl(ServiceContext context)
    {
      _context = context;
    }
    async public Task<Doctor?> CreateDoctor(DoctorForm form)
    {
        await Task.Delay(0);
        _context.Doctors.Add(
            new DoctorModel
            {
                FullName = form.FullName,
                Specialization = form.Specialization
            }
        );
        _context.SaveChanges();

        var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.FullName == form.FullName &&
            doctor.Specialization == form.Specialization);

        if (doctor is null) return null;

        return new Doctor(
            doctor.ID,
            doctor.FullName,
            doctor.Specialization
        );
    }
    async public Task<bool> DeleteDoctor(int id)
    {
        await Task.Delay(0);
        var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.ID == id);

        if (doctor is null) return false;
        
        var result = _context.Doctors.Remove(doctor);

        return result is null ? false : true;
        
    }
    async public Task<Doctor?> GetDoctorByID(int id)
    {
        await Task.Delay(0);
        var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.ID == id);

        if (doctor is null) return null;

        return new Doctor(doctor.ID, doctor.FullName, doctor.Specialization);
    }
    async public Task<List<Doctor>> GetDoctorsBySpecialization(string specialization)
        => _context.Doctors
                .Where(doctor => doctor.Specialization == specialization)
                .Select(doctor => new Doctor(doctor.ID, doctor.FullName, doctor.Specialization))
                .ToList();

    async public Task<List<Doctor>> GetAllDoctors()
        => _context.Doctors
                .Select(doctor => new Doctor(doctor.ID, doctor.FullName, doctor.Specialization))
                .ToList();
}
using Microsoft.EntityFrameworkCore;
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
        await _context.Doctors.AddAsync(
            new DoctorModel
            {
                FullName = form.FullName,
                Specialization = form.Specialization
            }
        );
        await _context.SaveChangesAsync();

        var doctor = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.FullName == form.FullName &&
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
        var doctor = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.ID == id);

        if (doctor is null) return false;
        
        var result = _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();

        return result is null ? false : true;
        
    }
    async public Task<Doctor?> GetDoctorByID(int id)
    {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.ID == id);

        if (doctor is null) return null;

        return new Doctor(doctor.ID, doctor.FullName, doctor.Specialization);
    }
    async public Task<List<Doctor>> GetDoctorsBySpecialization(string specialization)
        => await _context.Doctors
                .Where(doctor => doctor.Specialization == specialization)
                .Select(doctor => new Doctor(doctor.ID, doctor.FullName, doctor.Specialization))
                .ToListAsync();

    async public Task<List<Doctor>> GetAllDoctors()
        => await _context.Doctors
                .Select(doctor => new Doctor(doctor.ID, doctor.FullName, doctor.Specialization))
                .ToListAsync();
}
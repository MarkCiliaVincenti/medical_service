namespace Domain;

public interface IDoctorRepository
{
    Task<Doctor?> CreateDoctor(DoctorForm form);
    Task<bool> DeleteDoctor(int id);
    Task<Doctor?> GetDoctorByID(int id);
    Task<List<Doctor>> GetDoctorsBySpecialization(string specialization);
    Task<List<Doctor>> GetAllDoctors();
}
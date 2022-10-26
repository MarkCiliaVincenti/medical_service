namespace Domain;

public interface IDoctorRepository
{
    Doctor? CreateDoctor(DoctorForm form);
    bool DeleteDoctor(int id);
    Doctor? GetDoctorByID(int id);
    List<Doctor>? GetDoctorsBySpecialization(string specialization);
    List<Doctor>? GetAllDoctors();
}
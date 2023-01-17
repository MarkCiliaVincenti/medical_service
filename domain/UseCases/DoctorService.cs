namespace Domain;

public class DoctorService
{
    private readonly IDoctorRepository _repository;

    public DoctorService(IDoctorRepository repository)
    {
        _repository = repository;
    }

    async public Task<Result<Doctor>> CreateDoctor(DoctorForm form)
    {
        if (string.IsNullOrEmpty(form.FullName))
            return Result.Err<Doctor>("Name not specified");

        if (string.IsNullOrEmpty(form.Specialization))
            return Result.Err<Doctor>("Specialization not specified");

        var doctor = await _repository.CreateDoctor(form);

        if (doctor is not null)
            return Result.Ok<Doctor>(doctor);

        return Result.Err<Doctor>("Failed to create doctor");
    }

    async public Task<Result> DeleteDoctor(int id)
    {
        bool result = await _repository.DeleteDoctor(id);

        if (result == true)
            return Result.Ok();

        return Result.Err("Failed to delete doctor");
    }

    async public Task<Result<Doctor>> GetDoctorByID(int id)
    {
        var doctor = await _repository.GetDoctorByID(id);

        if (doctor is not null)
            return Result.Ok<Doctor>(doctor);

        return Result.Err<Doctor>("Doctor not found");
    }
    async public Task<Result<List<Doctor>>> GetDoctorsBySpecialization(string specialization)
    {
        if (string.IsNullOrEmpty(specialization))
            return Result.Err<List<Doctor>>("Specialization not specified");

        var doctors = await _repository.GetDoctorsBySpecialization(specialization);

        if (doctors is not null)
            return Result.Ok<List<Doctor>>(doctors);

        return Result.Err<List<Doctor>>("Doctors not found");
    }

    async public Task<Result<List<Doctor>>> GetAllDoctors()
    {
        var doctors = await _repository.GetAllDoctors();

        if (doctors is not null)
            return Result.Ok<List<Doctor>>(doctors);

        return Result.Err<List<Doctor>>("Doctors not found");
    }
}
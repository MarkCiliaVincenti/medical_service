namespace Domain;

public class DoctorService
{
    private readonly IDoctorRepository _repository;
    private static SemaphoreSlim doctorSemaphore = new SemaphoreSlim(1, 1);

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

        Doctor? doctor = null;

        try
        {
            await doctorSemaphore.WaitAsync();

            doctor = await _repository.CreateDoctor(form);
        }
        finally
        {
            doctorSemaphore.Release();
        }

        if (doctor is not null)
            return Result.Ok<Doctor>(doctor);

        return Result.Err<Doctor>("Failed to create doctor");
    }

    async public Task<Result> DeleteDoctor(int id)
    {
        bool result = false;
        try
        {
            await doctorSemaphore.WaitAsync();

            result = await _repository.DeleteDoctor(id);
        }
        finally
        {
            doctorSemaphore.Release();
        }

        if (result == true)
            return Result.Ok();

        return Result.Err("Failed to delete doctor");
    }

    async public Task<Result<Doctor>> GetDoctorByID(int id)
    {
        Doctor? doctor = null;
        try
        {
            await doctorSemaphore.WaitAsync();

            doctor = await _repository.GetDoctorByID(id);
        }
        finally
        {
            doctorSemaphore.Release();
        }

        if (doctor is not null)
            return Result.Ok<Doctor>(doctor);

        return Result.Err<Doctor>("Doctor not found");
    }
    async public Task<Result<List<Doctor>>> GetDoctorsBySpecialization(string specialization)
    {
        if (string.IsNullOrEmpty(specialization))
            return Result.Err<List<Doctor>>("Specialization not specified");

        List<Doctor> doctors = new List<Doctor>();
        try
        {
            await doctorSemaphore.WaitAsync();

            doctors = await _repository.GetDoctorsBySpecialization(specialization);
        }
        finally
        {
            doctorSemaphore.Release();
        }

        if (doctors is not null)
            return Result.Ok<List<Doctor>>(doctors);

        return Result.Err<List<Doctor>>("Doctors not found");
    }

    async public Task<Result<List<Doctor>>> GetAllDoctors()
    {
        List<Doctor> doctors = new List<Doctor>();
        try
        {
            await doctorSemaphore.WaitAsync();

            doctors = await _repository.GetAllDoctors();
        }
        finally
        {
            doctorSemaphore.Release();
        }

        if (doctors is not null)
            return Result.Ok<List<Doctor>>(doctors);

        return Result.Err<List<Doctor>>("Doctors not found");
    }
}
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Api;

[ApiController]
[Route("[controller]")]
public class DoctorController : ControllerBase
{
    private readonly ILogger<DoctorController> _logger;
    private readonly DoctorService _service;

    public DoctorController(ILogger<DoctorController> logger, DoctorService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("createDoctor")]
    async public Task<ActionResult<DoctorView>> CreateDoctor(DoctorForm form)
    {
        var result = await _service.CreateDoctor(form);

        if (result.Success)
        {
            return Ok(new DoctorView(result.Value));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpPost("deleteDoctor")]
    async public Task<ActionResult> DeleteDoctor(int id)
    {
        var result = await _service.DeleteDoctor(id);

        if (result.Success)
        {
            return Ok();
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctorById")]
    async public Task<ActionResult<DoctorView>> GetDoctorByID(int id)
    {
        var result = await _service.GetDoctorByID(id);

        if (result.Success)
        {
            return Ok(new DoctorView(result.Value));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctorsSpecialization")]
    async public Task<ActionResult<List<DoctorView>>> GetDoctorsBySpecialization(string specialization)
    {
        var result = await _service.GetDoctorsBySpecialization(specialization);

        if (result.Success)
        {
            return Ok(result.Value.ConvertAll(doctor => new DoctorView(doctor)));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctorsAll")]
    async public Task<ActionResult<List<DoctorView>>> GetAllDoctors()
    {
        var result = await _service.GetAllDoctors();

        if (result.Success)
        {
            return Ok(result.Value.ConvertAll(doctor => new DoctorView(doctor)));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }
}
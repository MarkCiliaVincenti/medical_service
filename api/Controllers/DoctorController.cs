using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Api;

[ApiController]
[Route("[doctorController]")]
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
    public ActionResult<DoctorView> CreateDoctor(DoctorForm form)
    {
        var result = _service.CreateDoctor(form);

        if (result.Success)
        {
            return Ok(new DoctorView(result.Value));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpPost("deleteDoctor")]
    public ActionResult DeleteDoctor(int id)
    {
        var result = _service.DeleteDoctor(id);

        if (result.Success)
        {
            return Ok();
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctorById")]
    public ActionResult<DoctorView> GetDoctorByID(int id)
    {
        var result = _service.GetDoctorByID(id);

        if (result.Success)
        {
            return Ok(new DoctorView(result.Value));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctorsSpecialization")]
    public ActionResult< List<DoctorView> > GetDoctorsBySpecialization(string specialization)
    {
        var result = _service.GetDoctorsBySpecialization(specialization);

        if (result.Success)
        {
            return Ok(result.Value.ConvertAll(doctor => new DoctorView(doctor)));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctorsAll")]
    public ActionResult< List<DoctorView> > GetAllDoctors()
    {
        var result = _service.GetAllDoctors();

        if (result.Success)
        {
            return Ok(result.Value.ConvertAll(doctor => new DoctorView(doctor)));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }
}
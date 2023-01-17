using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Api;

[ApiController]
[Route("[appointmentController]")]
public class AppointmentController : ControllerBase
{
    private readonly ILogger<AppointmentController> _logger;
    private readonly AppointmentService _service;

    public AppointmentController(ILogger<AppointmentController> logger, AppointmentService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("createAppointment")]
    public ActionResult<AppointmentView> CreateAppointment(AppointmentForm form)
    {
      var result = _service.CreateAppointment(form);

      if (result.Success)
      {
          return Ok(new AppointmentView(result.Value));
      }

      return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getFreeDates")]
    public ActionResult<List<(DateTime, DateTime)>> GetFreeDates(string specialization, DateOnly date)
    {
      var result = _service.GetFreeDates(specialization, date);

      if (result.Success)
      {
          return Ok(result.Value);
      }

      return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }
}
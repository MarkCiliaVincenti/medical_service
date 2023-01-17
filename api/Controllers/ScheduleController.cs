using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Api;

[ApiController]
[Route("[scheduleController]")]
public class ScheduleController : ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly ScheduleService _service;

    public ScheduleController(ILogger<ScheduleController> logger, ScheduleService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("getSchedule")]
    public ActionResult<ScheduleView> GetSchedule(int doctorID, DateOnly date)
    {
      var result = _service.GetSchedule(doctorID, date);

      if (result.Success)
      {
          return Ok(new ScheduleView(result.Value));
      }

      return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpPost("addSchedule")]
    public ActionResult<ScheduleView> AddSchedule(ScheduleForm form)
    {
      var result = _service.AddSchedule(form);

      if (result.Success)
      {
          return Ok(new ScheduleView(result.Value));
      }

      return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpPost("changeSchedule")]
    public ActionResult<ScheduleView> ChangeSchedule(ScheduleForm actual, ScheduleForm recent)
    {
      var result = _service.ChangeSchedule(actual, recent);

      if (result.Success)
      {
          return Ok(new ScheduleView(result.Value));
      }

      return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }
}
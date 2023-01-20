using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Api;

[ApiController]
[Route("[controller]")]
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
    async public Task<ActionResult<ScheduleView>> GetSchedule(int doctorID, DateOnly date)
    {
      var result = await _service.GetSchedule(doctorID, date);

      if (result.Success)
      {
          return Ok(new ScheduleView(result.Value));
      }

      return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpPost("addSchedule")]
    async public Task<ActionResult<ScheduleView>> AddSchedule(ScheduleForm form)
    {
      var result = await _service.AddSchedule(form);

      if (result.Success)
      {
          return Ok(new ScheduleView(result.Value));
      }

      return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpPost("changeSchedule")]
    async public Task<ActionResult<ScheduleView>> ChangeSchedule([FromQuery] ScheduleForm actual,
                                                     [FromQuery] ScheduleForm recent)
    {
      var result = await _service.ChangeSchedule(actual, recent);

      if (result.Success)
      {
          return Ok(new ScheduleView(result.Value));
      }

      return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }
}
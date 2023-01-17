using System.Text.Json.Serialization;
using Domain;


namespace Api;

public class ScheduleView
{
    [JsonPropertyName("doctorId")]
    public int DoctorID { get; set; }
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }
    [JsonPropertyName("dayStart")]
    public TimeOnly DayStart { get; set; }
    [JsonPropertyName("dayEnd")]
    public TimeOnly DayEnd { get; set; }

    public ScheduleView(Schedule schedule)
    {
        DoctorID = schedule.DoctorID;
        Date = schedule.Date;
        DayStart = schedule.DayStart;
        DayEnd = schedule.DayEnd;
    }
}
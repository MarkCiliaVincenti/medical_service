namespace Domain;

public class ScheduleForm
{
    public int DoctorID { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly DayStart { get; set; }
    public TimeOnly DayEnd { get; set; }
    public ScheduleForm(int doctorID, DateOnly date, TimeOnly dayStart, TimeOnly dayEnd)
    {
        DoctorID = doctorID;
        Date = date;
        DayStart = dayStart;
        DayEnd = dayEnd;
    }
}
namespace Domain;
public class Schedule
{
    public int DoctorID { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly DayStart { get; set; }
    public TimeOnly DayEnd { get; set; }
}
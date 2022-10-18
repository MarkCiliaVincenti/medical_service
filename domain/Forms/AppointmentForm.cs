namespace Domain;

public class AppointmentForm
{
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }

    public AppointmentForm(int patientID, int doctorID, DateOnly date, TimeOnly start, TimeOnly end)
    {
        PatientID = patientID;
        DoctorID = doctorID;
        Date = date;
        Start = start;
        End = end;
    }
}
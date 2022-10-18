namespace Domain;
public class Appointment
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public bool Free { get; set; }

    public Appointment(DateTime start, DateTime end, int patientID, int doctorID,
        bool free = true)
    {
        Start = start;
        End = end;
        PatientID = patientID;
        DoctorID = doctorID;
        Free = free;
    }
}
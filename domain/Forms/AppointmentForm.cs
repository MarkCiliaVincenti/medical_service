namespace Domain;
public class AppointmentForm
{
    public static readonly int EmptyDoctorID = -0xdeadb0b; // magic number == doctor not selected
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; } = EmptyDoctorID;
    public string Specialization { get; set; }

    public AppointmentForm(DateTime start, DateTime end, int patientID, int doctorID, string specialization)
    {
        Start = start;
        End = end;
        PatientID = patientID;
        DoctorID = doctorID;
        Specialization = specialization;
    }
}
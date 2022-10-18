namespace Domain;

public class AppointmentForm
{
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public DateOnly Date { get; set; }

    public AppointmentForm(int patientID, int doctorID, DateOnly date)
    {
        PatientID = patientID;
        DoctorID = doctorID;
        Date = date;
    }
}
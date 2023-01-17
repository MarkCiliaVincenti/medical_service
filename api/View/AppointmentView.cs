using System.Text.Json.Serialization;
using Domain;


namespace Api;

public class AppointmentView
{
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }
    [JsonPropertyName("end")]
    public DateTime End { get; set; }
    [JsonPropertyName("patientId")]
    public int PatientID { get; set; }
    [JsonPropertyName("doctorId")]
    public int DoctorID { get; set; }

    public AppointmentView(Appointment appointment)
    {
        Start = appointment.Start;
        End = appointment.End;
        PatientID = appointment.PatientID;
        DoctorID = appointment.DoctorID;
    }
}
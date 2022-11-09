using Domain;

namespace Repository;
public class AppointmentModel
{
    public int ID { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public Specialization Specialization { get; set; }
}
namespace Domain;
public class Reception {
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }

    public void GetSheduleDoctor() {}
    public void SaveSchedule() {}
    public void GetFreeDates() {}
    public void ChangeSchedule() {}
}
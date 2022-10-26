namespace Domain;
public class Doctor {
    public int ID { get; set; }
    public string FullName { get; set; } = "";
    public string Specialization { get; set; } = "";

    public Doctor(int id, string fullName, string specialization)
    {
        ID = id;
        FullName = fullName;
        Specialization = specialization;
    }
}
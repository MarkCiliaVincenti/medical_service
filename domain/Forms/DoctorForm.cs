namespace Domain;

public class DoctorForm
{
    public string FullName { get; set; } = "";
    public string Specialization { get; set; } = "";

    DoctorForm(string fullName, string specialization)
    {
        FullName = fullName;
        Specialization = specialization;
    }
}
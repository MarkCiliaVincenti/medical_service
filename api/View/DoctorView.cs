using System.Text.Json.Serialization;
using Domain;


namespace Api;

public class DoctorView
{
    [JsonPropertyName("id")]
    public int ID { get; set; }
    [JsonPropertyName("name")]
    public string FullName { get; set; } = "";
    [JsonPropertyName("specialization")]
    public string Specialization { get; set; } = "";

    public DoctorView(Doctor doctor)
    {
        ID = doctor.ID;
        FullName = doctor.FullName;
        Specialization = doctor.Specialization;
    }
}
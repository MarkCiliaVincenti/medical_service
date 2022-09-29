namespace Domain;
public class User {
    public int ID { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public AccountRole Role { get; }
}
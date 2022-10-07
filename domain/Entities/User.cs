namespace Domain;
public class User
{
    public int ID { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public AccountRole Role { get; }

    public string Login { get; set; }
    public string Password { get; set; }

    public User(int id, string phone, string name, string login, string password,
        AccountRole role = AccountRole.Patient)
    {
        ID = id;
        PhoneNumber = phone;
        FullName = name;
        Role = role;
        Login = login;
        Password = password;
    }
}
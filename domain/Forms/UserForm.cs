namespace Domain;

public class UserForm
{
    public string PhoneNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Login { get; set; }
    public string Password { get; set; }

    public UserForm(string phoneNumber, string fullName, string login, string password)
    {
        PhoneNumber = phoneNumber;
        FullName = fullName;
        Login = login;
        Password = password;
    }
}
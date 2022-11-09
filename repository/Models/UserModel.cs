using Domain;

namespace Repository;
public class UserModel
{
    public int ID { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public AccountRole Role { get; } = AccountRole.Patient;

    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
}
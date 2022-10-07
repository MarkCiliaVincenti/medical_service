namespace Domain;

public interface IUserRepository
{
    bool UserExists(string login);
    User? GetUserByLogin(string login);
    User? CreateUser(UserForm form);
}
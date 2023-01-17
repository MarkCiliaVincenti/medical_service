namespace Domain;

public interface IUserRepository
{
    Task<bool> UserExists(string login);
    Task<User?> GetUserByLogin(string login);
    Task<User?> CreateUser(UserForm form);
}
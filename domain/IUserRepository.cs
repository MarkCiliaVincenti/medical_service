namespace Domain;

interface IUserRepository
{
    bool UserExists(string login, string password);
    User? GetUserByLogin(string login);
    bool CreateUser(string login, string password);
}
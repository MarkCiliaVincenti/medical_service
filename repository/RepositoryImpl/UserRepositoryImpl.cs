using Domain;

namespace Repository;

public class UserRepositoryImpl : IUserRepository
{
    private ServiceContext _context;

    public UserRepositoryImpl(ServiceContext context)
    {
      _context = context;
    }
    async public Task<bool> UserExists(string login)
    {
        await Task.Delay(0);
        var users = _context.Users.Where(user => user.Login == login).ToList();

        return users.Count == 0 ? false : true;
    }
    async public Task<User?> GetUserByLogin(string login)
    {
        await Task.Delay(0);
        var user = _context.Users.FirstOrDefault(user => user.Login == login);

        if (user is null) return null;

        return new User(
            user.ID,
            user.PhoneNumber,
            user.FullName,
            user.Login,
            user.Password,
            user.Role
        );
    }
    async public Task<User?> CreateUser(UserForm form)
    {
        await Task.Delay(0);
        _context.Users.Add(
            new UserModel
            {
                PhoneNumber = form.PhoneNumber,
                FullName = form.FullName,
                Login = form.Login,
                Password = form.Password
            }
        );
        _context.SaveChanges();

        var user = _context.Users.FirstOrDefault(user => user.Login == form.Login);

        if (user is null) return null;

        return new User(
            user.ID,
            user.PhoneNumber,
            user.FullName,
            user.Login,
            user.Password,
            user.Role
        );
    }
}
using Domain;

namespace Repository;

public class UserRepositoryImpl : IUserRepository
{
    private ServiceContext _context;

    public UserRepositoryImpl(ServiceContext context)
    {
      _context = context;
    }
    public bool UserExists(string login)
    {
        var users = _context.Users.Where(user => user.Login == login).ToList();

        return users.Count == 0 ? false : true;
    }
    public User? GetUserByLogin(string login)
    {
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
    public User? CreateUser(UserForm form)
    {     
        int newID = _context.Users.Count() == 0 ? 1 : _context.Users.Last().ID;
        _context.Users.Append(
            new UserModel
            {
                ID = newID,
                PhoneNumber = form.PhoneNumber,
                FullName = form.FullName,
                Login = form.Login,
                Password = form.Password
            }
        );

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
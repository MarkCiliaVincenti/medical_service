using Microsoft.EntityFrameworkCore;
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
        var users = await _context.Users.Where(user => user.Login == login).ToListAsync();

        return users.Count == 0 ? false : true;
    }
    async public Task<User?> GetUserByLogin(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Login == login);

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
        await _context.Users.AddAsync(
            new UserModel
            {
                PhoneNumber = form.PhoneNumber,
                FullName = form.FullName,
                Login = form.Login,
                Password = form.Password
            }
        );
        await _context.SaveChangesAsync();

        var user = await _context.Users.FirstOrDefaultAsync(user => user.Login == form.Login);

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
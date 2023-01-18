namespace Domain;

public class UserService
{
    private readonly IUserRepository _repository;
    private static SemaphoreSlim userSemaphore = new SemaphoreSlim(1, 1);

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    async public Task<Result> UserExists(string login)
    {
        if (string.IsNullOrEmpty(login))
            return Result.Err("Login not specified");
        
        Boolean exists = false;

        try
        {
            await userSemaphore.WaitAsync();

            exists = await _repository.UserExists(login);
        }
        finally
        {
            userSemaphore.Release();
        }

        if (exists)
            return Result.Ok();

        return Result.Err("User not found");
    }

    async public Task<Result<User>> GetUserByLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
            return Result.Err<User>("Login not specified");

        User? user = null;

        try
        {
            await userSemaphore.WaitAsync();

            user = await _repository.GetUserByLogin(login);
        }
        finally
        {
            userSemaphore.Release();
        }
        
        if (user is not null)
            return Result.Ok<User>(user);

        return Result.Err<User>("User not found");
    }

    async public Task<Result<User>> CreateUser(UserForm form)
    {
        if (string.IsNullOrEmpty(form.Login))
            return Result.Err<User>("Login not specified");

        if (string.IsNullOrEmpty(form.Password))
            return Result.Err<User>("Password not specified");

        if (_repository.GetUserByLogin(form.Login) is not null)
            return Result.Err<User>("User with this login already exists");

        User? user = null;

        try
        {
            await userSemaphore.WaitAsync();

            user = await _repository.CreateUser(form);
        }
        finally
        {
            userSemaphore.Release();
        }

        if (user is not null)
            return Result.Ok<User>(user);

        return Result.Err<User>("Failed to create user");
    }
}
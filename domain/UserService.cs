namespace Domain;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public Result UserExists(string login, string password)
    {     
        if (string.IsNullOrEmpty(login))
            return Result.Err("Login not specified");

        if (string.IsNullOrEmpty(password))
            return Result.Err("Password not specified");

        bool exists = _repository.UserExists(login, password);

        if (exists)
            return Result.Ok();

        return Result.Err("User not found");
    }

    public Result<User> GetUserByLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
            return Result.Err<User>("Login not specified");

        User? result = _repository.GetUserByLogin(login);

        if (result is null)
            return Result.Err<User>("User not found");

        return Result.Ok<User>(result);
    }

    public Result<User> CreateUser(string login, string password)
    {
        if (string.IsNullOrEmpty(login))
            return Result.Err<User>("Login not specified");

        if (string.IsNullOrEmpty(password))
            return Result.Err<User>("Password not specified");

        if (_repository.GetUserByLogin(login) is not null)
            return Result.Err<User>("User with this login already exists");


        var user = new User(
            id: default,
            phone: string.Empty,
            name: string.Empty,
            login: login,
            password: password
        );
        bool isCreate = _repository.CreateUser(login, password);

        if (isCreate)
            return Result.Ok<User>(user);

        return Result.Err<User>("Failed to create user");
    }
}
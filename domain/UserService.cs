namespace Domain;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public bool UserExists(string login, string password)
    {        
        return _repository.UserExists(login, password);
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

    public Result CreateUser(string login, string password)
    {
        if (string.IsNullOrEmpty(login))
            return Result.Err("Login not specified");

        if (string.IsNullOrEmpty(password))
            return Result.Err("Password not specified");

        if (_repository.GetUserByLogin(login) is not null)
            return Result.Err("User with this login already exists");


        bool isCreate = _repository.CreateUser(login, password);

        if (isCreate)
            return Result.Ok();

        return Result.Err("Failed to create user");
    }
}
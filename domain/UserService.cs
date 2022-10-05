namespace Domain;

class UserService
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
        User? result = _repository.GetUserByLogin(login);

        if (result is null)
            return Result.Err<User>("User is not exists");

        return Result.Ok<User>(result);
    }

    public Result CreateUser(string login, string password)
    {
        bool isCreate = _repository.CreateUser(login, password);

        if (isCreate)
            return Result.Ok();

        return Result.Err("Failed to create user");
    }
}
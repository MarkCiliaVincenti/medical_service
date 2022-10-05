using Domain;

namespace UnitTests;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void GetUserLoginIsEmpty_ShouldFail()
    {
        //Arrange
        string expected_error = "Login not specified";
        
        //Act
        var result = _userService.GetUserByLogin(string.Empty);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
        Assert.Equal(null, result.Value);
    }

    [Fact]
    public void GetUserNotFoundUser_ShouldFail()
    {
        //Arrange
        string login = "lol";
        string expected_error = "User not found";

        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => null); 
        
        //Act
        var result = _userService.GetUserByLogin(login);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
        Assert.Equal(null, result.Value);
    }

    [Fact]
    public void GetUserFoundUser_ShouldOk()
    {
        //Arrange
        string login = "lol";
        string expected_error = string.Empty;

        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => new User(default, string.Empty, string.Empty, login, string.Empty)); 
        
        //Act
        var result = _userService.GetUserByLogin(login);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
        Assert.Equal(login, result.Value.Login);
    }

    [Fact]
    public void CreateUserWithEmptyLogin_ShouldFail()
    {
        //Arrange
        string login = string.Empty;
        string password = string.Empty;
        string expected = "Login not specified";

        //Act
        var result = _userService.CreateUser(login, password);
        var actual = result.Error;

        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected, actual);
        Assert.Equal(null, result.Value);
    }

    [Fact]
    public void CreateUserWithEmptyPassword_ShouldFail()
    {
        //Arrange
        string login = "fs-anvr";
        string password = string.Empty;
        string expected = "Password not specified";

        //Act
        var result = _userService.CreateUser(login, password);
        var actual = result.Error;

        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected, actual);
        Assert.Equal(null, result.Value);
    }

    [Fact]
    public void CreateUserWithOccupiedLogin_ShouldFail()
    {
        //Arrange
        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => new User(5, "", "", "", ""));

        string login = "fs-anvr";
        string password = "difficult password";
        string expected = "User with this login already exists";

        //Act
        var result = _userService.CreateUser(login, password);
        var actual = result.Error;

        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected, actual);
        Assert.Equal(null, result.Value);
    }

    [Fact]
    public void CreateUserWhenRepositoryWork_ShouldOk()
    {
        //Arrange

        string login = "fs-anvr";
        string password = "difficult password";
        string expected_error = string.Empty;

        _userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(() => true);

        //Act
        var result = _userService.CreateUser(login, password);

        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
        Assert.Equal(login, result.Value.Login);
        Assert.Equal(password, result.Value.Password);
    }

    [Fact]
    public void CreateUserWhenRepositoryBroken_ShouldFail()
    {
        //Arrange

        string login = "fs-anvr";
        string password = "difficult password";
        string expected_error = "Failed to create user";

        _userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(() => false);

        //Act
        var result = _userService.CreateUser(login, password);

        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
        Assert.Equal(null, result.Value);
    }

    [Fact]
    public void UserExistsWithEmptyLogin_ShouldFail()
    {
        //Arrange

        string login = string.Empty;
        string password = "difficult password";
        string expected_error = "Login not specified";

        //Act
        var result = _userService.UserExists(login, password);

        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void UserExistsWithEmptyPassword_ShouldFail()
    {
        //Arrange

        string login = "fsanvr";
        string password = string.Empty;
        string expected_error = "Password not specified";

        //Act
        var result = _userService.UserExists(login, password);

        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void UserExists_ShouldOk()
    {
        //Arrange

        string login = "fsanvr";
        string password = "difficult password";
        string expected_error = string.Empty;

        _userRepositoryMock.Setup(repository => repository.UserExists(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(() => true);

        //Act
        var result = _userService.UserExists(login, password);

        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void UserNotExists_ShouldFail()
    {
        //Arrange

        string login = "fsanvr";
        string password = "difficult password";
        string expected_error = "User not found";

        _userRepositoryMock.Setup(repository => repository.UserExists(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(() => false);

        //Act
        var result = _userService.UserExists(login, password);

        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }
}
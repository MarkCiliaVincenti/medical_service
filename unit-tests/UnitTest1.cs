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
    public void LoginIsEmpty_ShouldFail()
    {
        //Arrange
        string expected = "Login not specified";
        
        //Act
        var res = _userService.GetUserByLogin(string.Empty);
        var actual = res.Error;
        
        //Assert
        Assert.True(res.IsFail);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void UserNotFound_ShouldFail()
    {
        //Arrange
        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => null); 
        
        //Act
        var result = _userService.GetUserByLogin("lol");
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal("User not found", result.Error);
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
    }
}
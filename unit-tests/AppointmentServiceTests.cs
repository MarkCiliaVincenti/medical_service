using Domain;

namespace UnitTests;

public class AppointmentServiceTests
{
    private readonly AppointmentService _appointmentService;
    private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;

    public AppointmentServiceTests()
    {
        _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        _appointmentService = new AppointmentService(_appointmentRepositoryMock.Object);
    }

    [Fact]
    public void CreateAppointmentOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        var date = new DateOnly(1, 1, 1);
        var form = new AppointmentForm(12345, 12354, date);
        _appointmentRepositoryMock.Setup(repository => repository.CreateAppointment(form))
            .Returns(() => new Appointment(new DateTime(1, 1, 1), new DateTime(1, 1, 1), 1, 1));
        
        //Act
        var result = _appointmentService.CreateAppointment(form);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateAppointmentOtherError_ShouldFail()
    {
        //Arrange
        string expected_error = "Failed to create appointment";
        var date = new DateOnly(1, 1, 1);
        var form = new AppointmentForm(12345, 12354, date);
        _appointmentRepositoryMock.Setup(repository => repository.CreateAppointment(form))
            .Returns(() => null);
        
        //Act
        var result = _appointmentService.CreateAppointment(form);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetFreeDatesOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        string specialization = "gigachad";
        _appointmentRepositoryMock.Setup(repository => repository.GetFreeDates(specialization))
            .Returns(() => new List<Appointment>());
        
        //Act
        var result = _appointmentService.GetFreeDates(specialization);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetFreeDatesEmptySpecialization_ShouldFail()
    {
        //Arrange
        string expected_error = "Specialization not specified";
        string specialization = string.Empty;
        
        //Act
        var result = _appointmentService.GetFreeDates(specialization);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetFreeDatesNotFound_ShouldFail()
    {
        //Arrange
        string expected_error = "Free dates not found";
        string specialization = "gigachad";
        _appointmentRepositoryMock.Setup(repository => repository.GetFreeDates(specialization))
            .Returns(() => null);
        
        //Act
        var result = _appointmentService.GetFreeDates(specialization);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }
}
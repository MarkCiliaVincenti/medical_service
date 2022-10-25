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
    public void CreateAppointmentWithDoctorOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        var date = new DateTime(1, 1, 1);
        int doctorID = 5;
        _appointmentRepositoryMock.Setup(repository => repository.AppointmentExists(date, doctorID))
            .Returns(() => false);
        _appointmentRepositoryMock.Setup(repository => repository.CreateAppointment(date, doctorID))
            .Returns(() => new Appointment(new DateTime(1, 1, 1), new DateTime(1, 1, 1), 1, 1));
        
        //Act
        var result = _appointmentService.CreateAppointment(date, doctorID);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateAppointmentAlreadyExists_ShouldFail()
    {
        //Arrange
        string expected_error = "Appointment with this doctor for this date already exists";
        var date = new DateTime(1, 1, 1);
        int doctorID = 5;
        _appointmentRepositoryMock.Setup(repository => repository.AppointmentExists(date, doctorID))
            .Returns(() => true);
        
        //Act
        var result = _appointmentService.CreateAppointment(date, doctorID);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateAppointmentWithDoctorOtherError_ShouldFail()
    {
        //Arrange
        string expected_error = "Failed to create appointment";
        var date = new DateTime(1, 1, 1);
        int doctorID = 5;
        _appointmentRepositoryMock.Setup(repository => repository.AppointmentExists(date, doctorID))
            .Returns(() => false);
        _appointmentRepositoryMock.Setup(repository => repository.CreateAppointment(date, doctorID))
            .Returns(() => null);
        
        //Act
        var result = _appointmentService.CreateAppointment(date, doctorID);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    public void CreateAppointmentOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        var date = new DateTime(1, 1, 1);
        _appointmentRepositoryMock.Setup(repository => repository.CreateAppointment(date))
            .Returns(() => new Appointment(new DateTime(1, 1, 1), new DateTime(1, 1, 1), 1, 1));
        
        //Act
        var result = _appointmentService.CreateAppointment(date);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateAppointmentOtherError_ShouldFail()
    {
        //Arrange
        string expected_error = "Failed to create appointment";
        var date = new DateTime(1, 1, 1);
        _appointmentRepositoryMock.Setup(repository => repository.CreateAppointment(date))
            .Returns(() => null);
        
        //Act
        var result = _appointmentService.CreateAppointment(date);
        
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
            .Returns(() => new List<DateTime>());
        
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
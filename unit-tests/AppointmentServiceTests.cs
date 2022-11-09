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
        var date = new DateTime(1, 1, 1);
        int patientID = 10;
        int doctorID = 5;
        string specialization = "some spec";
        var form = new AppointmentForm(date, date, patientID, doctorID, specialization);
        _appointmentRepositoryMock.Setup(repository => repository.AppointmentExists(form))
            .Returns(() => false);
        _appointmentRepositoryMock.Setup(repository => repository.CreateAppointment(form))
            .Returns(() => new Appointment(new DateTime(1, 1, 1), new DateTime(1, 1, 1), 1, 1));
        
        //Act
        var result = _appointmentService.CreateAppointment(form);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateAppointmentEmptySpecialization_ShouldFail()
    {
        //Arrange
        string expected_error = "Specialization not specified";
        var date = new DateTime(1, 1, 1);
        int patientID = 10;
        int doctorID = 5;
        string specialization = string.Empty;
        var form = new AppointmentForm(date, date, patientID, doctorID, specialization);
        
        //Act
        var result = _appointmentService.CreateAppointment(form);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateAppointmentAlreadyExists_ShouldFail()
    {
        //Arrange
        string expected_error = "Appointment with this doctor for this date already exists";
        var date = new DateTime(1, 1, 1);
        int patientID = 10;
        int doctorID = 5;
        string specialization = "some spec";
        var form = new AppointmentForm(date, date, patientID, doctorID, specialization);
        _appointmentRepositoryMock.Setup(repository => repository.AppointmentExists(form))
            .Returns(() => true);
        
        //Act
        var result = _appointmentService.CreateAppointment(form);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateAppointmentWithoutDoctor_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        var date = new DateTime(1, 1, 1);
        int patientID = 10;
        int doctorID = -0xdeadb0b;
        string specialization = "some spec";
        var form = new AppointmentForm(date, date, patientID, doctorID, specialization);
        _appointmentRepositoryMock.Setup(repository => repository.AppointmentExists(form))
            .Returns(() => false);
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
        var date = new DateTime(1, 1, 1);
        int patientID = 10;
        int doctorID = 5;
        string specialization = "some spec";
        var form = new AppointmentForm(date, date, patientID, doctorID, specialization);
        _appointmentRepositoryMock.Setup(repository => repository.AppointmentExists(form))
            .Returns(() => false);
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
        _appointmentRepositoryMock.Setup(repository => repository.GetAllDates(specialization))
            .Returns(() => null);
        
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
        var busyDates = new List<DateTime>();
        for(int i = 0; i < 30; ++i)
        {
            busyDates.Append(DateTime.Now.AddDays(i));
        }
        _appointmentRepositoryMock.Setup(repository => repository.GetAllDates(specialization))
            .Returns(() => busyDates);
        
        //Act
        var result = _appointmentService.GetFreeDates(specialization);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }
}
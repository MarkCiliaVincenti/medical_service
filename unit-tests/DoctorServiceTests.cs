using Domain;

namespace UnitTests;

public class DoctorServiceTests
{
    private readonly DoctorService _doctorService;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

        public DoctorServiceTests()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _doctorService = new DoctorService(_doctorRepositoryMock.Object);
    }

    [Fact]
    public void CreateDoctorWithEmptyName_ShouldFail()
    {
        //Arrange
        string expected_error = "Name not specified";
        var form = new DoctorForm(string.Empty, "fkfdbkmfgbl");
        
        //Act
        var result = _doctorService.CreateDoctor(form);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateDoctorWithEmptySpecialization_ShouldFail()
    {
        //Arrange
        string expected_error = "Specialization not specified";
        var form = new DoctorForm("Klim Sanich Zhikov", string.Empty);
        
        //Act
        var result = _doctorService.CreateDoctor(form);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateDoctorOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        var form = new DoctorForm("Klim Sanich Zhikov", "34567");
        _doctorRepositoryMock.Setup(repository => repository.CreateDoctor(form))
            .Returns(() => new Doctor(234, form.FullName, form.Specialization));
        
        //Act
        var result = _doctorService.CreateDoctor(form);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void CreateDoctorOtherError_ShouldFail()
    {
        //Arrange
        string expected_error = "Failed to create doctor";
        var form = new DoctorForm("Klim Sanich Zhukov", "34567");
        _doctorRepositoryMock.Setup(repository => repository.CreateDoctor(form))
            .Returns(() => null);
        
        //Act
        var result = _doctorService.CreateDoctor(form);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void DeleteDoctorOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        int id = 3467;
        _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(id))
            .Returns(() => true);
        
        //Act
        var result = _doctorService.DeleteDoctor(id);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void DeleteDoctorOtherError_ShouldFail()
    {
        //Arrange
        string expected_error = "Failed to delete doctor";
        int id = 3467;
        _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(id))
            .Returns(() => false);
        
        //Act
        var result = _doctorService.DeleteDoctor(id);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetDoctorByID_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        int id = 3467;
        _doctorRepositoryMock.Setup(repository => repository.GetDoctorByID(id))
            .Returns(() => new Doctor(34, "Klim Sanich Zhukov", "random"));
        
        //Act
        var result = _doctorService.GetDoctorByID(id);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetDoctorByIDNotFound_ShouldFail()
    {
        //Arrange
        string expected_error = "Doctor not found";
        int id = 3467;
        _doctorRepositoryMock.Setup(repository => repository.GetDoctorByID(id))
            .Returns(() => null);
        
        //Act
        var result = _doctorService.GetDoctorByID(id);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }
    
    [Fact]
    public void GetDoctorBySpecialization_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        string specialization = "random ";
        _doctorRepositoryMock.Setup(
            repository => repository.GetDoctorsBySpecialization(specialization)
            ).Returns(() => new List<Doctor>());
        
        //Act
        var result = _doctorService.GetDoctorsBySpecialization(specialization);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetDoctorsBySpecializationEmptySpec_ShouldFail()
    {
        //Arrange
        string expected_error = "Specialization not specified";
        string specialization = string.Empty;
        
        //Act
        var result = _doctorService.GetDoctorsBySpecialization(specialization);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetDoctorsBySpecializationNotFound_ShouldFail()
    {
        //Arrange
        string expected_error = "Doctors not found";
        string specialization = "random ";
        _doctorRepositoryMock.Setup(
            repository => repository.GetDoctorsBySpecialization(specialization)
            ).Returns(() => null);
        
        //Act
        var result = _doctorService.GetDoctorsBySpecialization(specialization);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetAllDoctors_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        _doctorRepositoryMock.Setup(repository => repository.GetAllDoctors())
            .Returns(() => new List<Doctor>());
        
        //Act
        var result = _doctorService.GetAllDoctors();
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetAllDoctorsNotFound_ShouldFail()
    {
        //Arrange
        string expected_error = "Doctors not found";
        _doctorRepositoryMock.Setup(repository => repository.GetAllDoctors())
            .Returns(() => null);
        
        //Act
        var result = _doctorService.GetAllDoctors();
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }
}
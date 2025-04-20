namespace Importify.Tests.Services;

public class EmployeeImporterServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IEmployeeRepository> _employeeRepoMock = new();
    private readonly Mock<ILogger<EmployeeImporterService>> _loggerMock = new();

    public EmployeeImporterServiceTests()
    {
        _unitOfWorkMock.SetupGet(x => x.Employees).Returns(_employeeRepoMock.Object);
    }

    [Fact]
    public async Task ImportAsync_ShouldImport_ValidRows()
    {
        // Arrange
        var csv = new StringBuilder();
        csv.AppendLine("Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname," +
            "Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address," +
            "Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date");
        csv.AppendLine("001,John,Doe,1980-01-01,123,456,Main St,City,0000,john@example.com,2020-01-01");

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv.ToString()));

        _employeeRepoMock.Setup(r => r.GetExistingPayrollNumbersAsync(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(new HashSet<string>());

        var service = new EmployeeImporterService(_unitOfWorkMock.Object, _loggerMock.Object);

        // Act
        var result = await service.ImportAsync(stream, Guid.Empty);

        // Assert
        Assert.Equal(1, result.Success);
        Assert.Equal(0, result.Failed);
        _employeeRepoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
        _unitOfWorkMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ImportAsync_ShouldDetect_DuplicateInFile()
    {
        var csv = new StringBuilder();
        csv.AppendLine("Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname," +
            "Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address," +
            "Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date");
        csv.AppendLine("001,John,Doe,1980-01-01,123,456,Main St,City,0000,john@example.com,2020-01-01");
        csv.AppendLine("001,Jane,Doe,1981-01-01,123,456,Main St,City,0000,jane@example.com,2021-01-01");

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv.ToString()));

        _employeeRepoMock.Setup(r => r.GetExistingPayrollNumbersAsync(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(new HashSet<string>());

        var service = new EmployeeImporterService(_unitOfWorkMock.Object, _loggerMock.Object);

        var result = await service.ImportAsync(stream, Guid.Empty);

        Assert.Equal(1, result.Success);
        Assert.Equal(1, result.Failed);
        Assert.Single(result.Errors);
        Assert.Contains("Duplicate payroll number", result.Errors[0].Message);
    }

    [Fact]
    public async Task ImportAsync_ShouldDetect_DuplicateInDatabase()
    {
        var csv = new StringBuilder();
        csv.AppendLine("Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname," +
            "Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address," +
            "Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date");
        csv.AppendLine("001,John,Doe,1980-01-01,123,456,Main St,City,0000,john@example.com,2020-01-01");

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv.ToString()));

        _employeeRepoMock.Setup(r => r.GetExistingPayrollNumbersAsync(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(new HashSet<string> { "001" });

        var service = new EmployeeImporterService(_unitOfWorkMock.Object, _loggerMock.Object);

        var result = await service.ImportAsync(stream, Guid.Empty);

        Assert.Equal(0, result.Success);
        Assert.Equal(1, result.Failed);
        Assert.Single(result.Errors);
        Assert.Contains("Duplicate payroll number", result.Errors[0].Message);
    }
}
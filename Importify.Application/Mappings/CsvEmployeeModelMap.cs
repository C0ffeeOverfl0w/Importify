namespace Importify.Application.Mappings;

/// <summary>
/// Maps the properties of the <see cref="CsvEmployeeModel"/> class to the corresponding
/// column names in a CSV file for data import/export.
/// </summary>
public class CsvEmployeeModelMap : ClassMap<CsvEmployeeModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CsvEmployeeModelMap"/> class
    /// and defines the mapping between the <see cref="CsvEmployeeModel"/> properties
    /// and the CSV column headers.
    /// </summary>
    public CsvEmployeeModelMap()
    {
        Map(m => m.PayrollNumber).Name("Personnel_Records.Payroll_Number");
        Map(m => m.Forenames).Name("Personnel_Records.Forenames");
        Map(m => m.Surname).Name("Personnel_Records.Surname");
        Map(m => m.DateOfBirth).Name("Personnel_Records.Date_of_Birth");
        Map(m => m.Telephone).Name("Personnel_Records.Telephone");
        Map(m => m.Mobile).Name("Personnel_Records.Mobile");
        Map(m => m.Address).Name("Personnel_Records.Address");
        Map(m => m.Address2).Name("Personnel_Records.Address_2");
        Map(m => m.Postcode).Name("Personnel_Records.Postcode");
        Map(m => m.Email).Name("Personnel_Records.EMail_Home");
        Map(m => m.StartDate).Name("Personnel_Records.Start_Date");
    }
}
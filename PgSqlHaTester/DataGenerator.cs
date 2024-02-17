using Bogus;

namespace PgSqlHaTester;
public static class BogusDataGenerator
{
    private static readonly Random _random = new();
    const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public static List<TableUser> GetSampleTableData(int numberOfRecords = 100000)
    {
        var customerId = 1;
        var userFaker = new Faker<TableUser>("en_IND")
            .CustomInstantiator(f => new TableUser(customerId++.ToString()))
            .RuleFor(o => o.CustomerCode, f => RandomString(10))
            .RuleFor(o => o.ModifiedDate, f => f.Date.Recent(100))
            .RuleFor(o => o.NameStyle, f => f.Random.Bool())
            .RuleFor(o => o.Phone, f => f.Person.Phone)
            .RuleFor(o => o.FirstName, f => f.Name.FirstName())
            .RuleFor(o => o.LastName, f => f.Name.LastName())
            .RuleFor(o => o.Title, f => f.Name.Prefix(f.Person.Gender))
            .RuleFor(o => o.Suffix, f => f.Name.Suffix())
            .RuleFor(o => o.MiddleName, f => f.Name.FirstName())
            .RuleFor(o => o.EmailAddress, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(o => o.SalesPerson, f => f.Name.FullName())
            .RuleFor(o => o.CompanyName, f => f.Company.CompanyName());

        return userFaker.Generate(numberOfRecords);
    }

    public static string RandomString(int length) => new(Enumerable.Repeat(_chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());

}

public class TableUser
{
    private string _customerId;

    public TableUser() { }

    public TableUser(string customerId)
    {
        _customerId = customerId;

    }
    public string CustomerId
    {
        get => _customerId;
        set => _customerId = value;
    }
    public DateTime ModifiedDate { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public bool NameStyle { get; set; }
    public string Suffix { get; set; }
    public string CompanyName { get; set; }
    public string SalesPerson { get; set; }
    public string EmailAddress { get; set; }
    public string Phone { get; set; }
    public string CustomerCode { get; set; }
}

public class TableUserExtended : TableUser
{
    public string Server { get; set; }

}

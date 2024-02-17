using Dapper;
using Npgsql;
using System.Text.Json;

namespace PgSqlHaTester;
public class PgSqlHelper
{
    static readonly string _writeConnectionString = @"WRITE CONNECTION STRING";
    static readonly string _readConnectionString = @"READ CONNECTION STRING";

    static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static async Task InsertData()
    {
        var data = BogusDataGenerator.GetSampleTableData(10000);


        foreach (var item in data)
        {
            using (var dbConnection = new NpgsqlConnection(_writeConnectionString))
            {
                try
                {
                    var insertedUser = await dbConnection.QueryFirstAsync<TableUserExtended>($"INSERT INTO public.users (customerid, modifieddate, title, firstname, lastname, middlename, namestyle, suffix, companyname, salesperson, emailaddress, phone) " +
                           $" VALUES (@customerid, @modifieddate, @title, @firstname, @lastname, @middlename, @namestyle, @suffix, @companyname, @salesperson, @emailaddress, @phone) RETURNING *,cast ( inet_server_addr() as text) server;",
                           new
                           {
                               customerid = item.CustomerId,
                               modifieddate = DateTime.Now,
                               title = item.Title,
                               firstname = item.FirstName,
                               lastname = item.LastName,
                               middlename = item.MiddleName,
                               namestyle = item.NameStyle,
                               suffix = item.Suffix,
                               companyname = item.CompanyName,
                               salesperson = item.SalesPerson,
                               emailaddress = item.EmailAddress,
                               phone = item.Phone
                           }, commandType: System.Data.CommandType.Text);

                    await Task.Delay(300);
                    Console.ForegroundColor = ConsoleColor.Green;
                    await Console.Out.WriteLineAsync(JsonSerializer.Serialize(insertedUser, _options));
                }
                catch (Exception ex)
                {

                }
                finally
                {

                }
            }
        }


    }


    public static async Task ReadData()
    {

        var sqlQuery = $"SELECT *,cast (inet_server_addr() as text) server FROM public.users u order by u.modifieddate desc limit 1;";
        var sqlQuery1 = $"SELECT *,cast ( inet_server_addr() as text) server FROM public.users where customerid=@customerid;";
        while (true)
        {

            using (var dbConnection = new NpgsqlConnection(_readConnectionString))
            {
                try
                {
                    var record = await dbConnection.QueryFirstAsync<TableUserExtended>(sqlQuery,
                           new
                           {
                               // customerid = Random.Shared.Next(1, 2500),
                           }, commandType: System.Data.CommandType.Text);

                    await Task.Delay(200);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    await Console.Out.WriteLineAsync(JsonSerializer.Serialize(record, _options));
                }
                catch (Exception ex)
                {

                    // throw;
                }
                finally
                {

                }
            }
        }

    }
}

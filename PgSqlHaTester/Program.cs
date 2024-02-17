
using PgSqlHaTester;

Task.Run(async () =>
{
    await PgSqlHelper.InsertData();
});

Task.Run(async () =>
{
    await PgSqlHelper.ReadData();
});

Console.ReadLine();

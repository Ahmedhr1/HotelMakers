using Npgsql;
using System;

public class Bookings
{
    NpgsqlDataSource _db;

    public Bookings(NpgsqlDataSource db)
    {
        _db = db;
    }
    public async Task Book()
    {

        Console.WriteLine("Choose one of the following Hotels");
        string qhotels = @"
          SELECT * FROM hotels
    
         ";

        string result = string.Empty;
        NpgsqlDataReader reader = await _db.CreateCommand(qhotels).ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result += reader.GetString(1);
            result += " - ";
            result += reader.GetInt32(0);
            result += "\n";
        }


    }
}

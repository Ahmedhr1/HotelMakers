using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMakers_8;

public class Customer
{
    NpgsqlDataSource _db;

    public Customer(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task InsertCustomer()
    {
        string insert = "INSERT INTO customers (customer_ID, first_name, last_name, email, birthdate) VALUES ($1, $2, $3, $4, $5)";
        string select = "SELECT * FROM customers";

        await using (var cmd = _db.CreateCommand(insert))
        {
            cmd.Parameters.AddWithValue(102);
            cmd.Parameters.AddWithValue("bAhmed");
            cmd.Parameters.AddWithValue("Raif");
            cmd.Parameters.AddWithValue("dooubleeee.a@gmail.com");
            cmd.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, new DateTime(2001, 5, 5));

            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = _db.CreateCommand(select))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Console.WriteLine(reader.GetString(1));
                Console.WriteLine(reader.GetString(2));
                Console.WriteLine(reader.GetString(3));
            }
        }
    }
}







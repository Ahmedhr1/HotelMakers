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
            //fixa så att felinmatning får sin konsekvens
            Console.Clear();
            Console.Write("Enter ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Birthdate (YYYY-MM-DD): ");
            DateTime birthdate = DateTime.Parse(Console.ReadLine());

            cmd.Parameters.AddWithValue(id);
            cmd.Parameters.AddWithValue(firstName);
            cmd.Parameters.AddWithValue(lastName);
            cmd.Parameters.AddWithValue(email);
            cmd.Parameters.AddWithValue(birthdate);


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







using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMakers_8;

public class ViewOfBooking
{
    NpgsqlDataSource _db;

    public ViewOfBooking(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task ShowAllBookings()
    {
        Console.Clear();
        Console.WriteLine("All Bookings");
        Console.WriteLine("---------------------------------");

        string query = @"SELECT
                           customer_id,
                           first_name,
                           last_name,
                           check_in,
                           check_out
                           FROM
                           bookedrooms
                           JOIN
                           customers using (customer_id)";

        await using (var cmd = _db.CreateCommand(query))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"Customer ID: {reader.GetInt32(0)}");
                Console.WriteLine($"First Name: {reader.GetString(1)}");
                Console.WriteLine($"Last Name: {reader.GetString(2)}");
                DateOnly checkinDateOnly = await reader.GetFieldValueAsync<DateOnly>(3);
                Console.WriteLine($"Check-in Date: {checkinDateOnly}");
                DateOnly checkoutDateOnly = await reader.GetFieldValueAsync<DateOnly>(4);
                Console.WriteLine($"Check-in Date: {checkoutDateOnly}");

               
                
                //glöm inte skapa dateonlys



            }
            Console.WriteLine();
            Console.Write("Type CustomerID: ");

            if (int.TryParse(Console.ReadLine(), out int customerID))
            {

                await ShowPersonBookings(customerID);


            }
        }
    }

    public async Task ShowPersonBookings(int customerID)
    {
        Console.Clear();
        Console.WriteLine($"Bookings for CustomerID {customerID}:");
        Console.WriteLine("-----------------------------");

        string query = $"SELECT * FROM bookedrooms WHERE customer_id = {customerID}";
        await using (var cmd = _db.CreateCommand(query))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"BookingID: {reader.GetInt32(0)}");
                Console.WriteLine($"Hotel ID: {reader.GetInt32(5)}");
                Console.WriteLine($"Room ID: {reader.GetInt32(1)}");
                DateOnly checkinDateOnly = await reader.GetFieldValueAsync<DateOnly>(3);
                Console.WriteLine($"Check-in Date: {checkinDateOnly}");
                DateOnly checkoutDateOnly = await reader.GetFieldValueAsync<DateOnly>(4);
                Console.WriteLine($"Check-out Date: {checkoutDateOnly}");
                Console.WriteLine($"Addons: {reader.GetInt32(6)}");

            }
        }


        AlterBookings showin = new AlterBookings(_db);
        await showin.AlterBooking(customerID);
    }

    

}











// Fixa så att alla bokningar visas i en lista
// välj sedan en persons bokningar
//personens bokning ska visas som den visas i table
//därefter ska det finnas två val Alter eller Cancel Booking




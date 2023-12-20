using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMakers_8;

public class AlterBooking
{
    NpgsqlDataSource _db;

    public AlterBooking(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task ShowAllBookings()
    {
        Console.Clear();
        Console.WriteLine("All Bookings:");

        string query = "SELECT * FROM bookings";
        await using (var cmd = _db.CreateCommand(query))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"Booking ID: {reader.GetInt32(0)}");
                Console.WriteLine($"Customer ID: {reader.GetInt32(1)}");
                Console.WriteLine();
            }
        }
    }

    public async Task ShowPersonBookings(int customerId)
    {
        Console.Clear();
        Console.WriteLine($"Bookings for Customer ID: {customerId}");

        string query = $"SELECT * FROM bookings WHERE customer_id = {customerId}";
        await using (var cmd = _db.CreateCommand(query))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"Booking ID: {reader.GetInt32(0)}");
                Console.WriteLine();
            }
        }
    }

    public async Task AlterOrCancelBooking(int bookingId)
    {
        Console.Clear();
        Console.WriteLine($"Booking ID: {bookingId}");
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Alter Booking");
        Console.WriteLine("2. Cancel Booking");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid choice, Try again");
            return;
        }

        switch (choice)
        {
            case 1:
                // Implement logic to alter the booking
                Console.WriteLine("Implement logic to alter the booking.");
                break;
            case 2:
                // Implement logic to cancel the booking
                Console.WriteLine("Implement logic to cancel the booking.");
                break;
            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }
}
    // Fixa så att alla bokningar visas i en lista
    // välj sedan en persons bokningar
    //personens bokning ska visas som den visas i table
    //därefter ska det finnas två val Alter eller Cancel Booking




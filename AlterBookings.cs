using HotelMakers_8;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelMakers_8;
public class AlterBookings
{

    NpgsqlDataSource _db;

    public AlterBookings(NpgsqlDataSource db)
    {
        _db = db;
    }


    public async Task AlterBooking(int customerID)
    {

        Console.WriteLine("----------------------------");
        Console.Clear();

        Console.WriteLine("1. Change Hotel");
        Console.WriteLine("2. Change Room");
        Console.Write("Choose an option: ");

        if (!int.TryParse(Console.ReadLine(), out int alterChoice))
        {
            Console.WriteLine("Invalid. Try again");
            return;
        }

        switch (alterChoice)
        {
            case 1:
                await ChangeHotel(customerID);
                break;
            case 2:
                await ChangeRoom(customerID);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
                
        }
    }

    public async Task ChangeHotel(int customerID)
    {
        string hotelsQuery = "SELECT * FROM hotels";

        await using (var cmd = _db.CreateCommand(hotelsQuery))
        await using (var Reader = await cmd.ExecuteReaderAsync())
        {
            while (await Reader.ReadAsync())
            {
                Console.WriteLine();

                Console.WriteLine($"Hotel ID: {Reader.GetInt32(0)}");
                Console.WriteLine($"Hotel Name: {Reader.GetString(1)}");
            }
        }
        Console.WriteLine();
        Console.Write("Select a new hotel by entering its ID: ");

        if (int.TryParse(Console.ReadLine(), out int newHotelID))
        {
            string updateQuery = $"UPDATE bookedrooms SET hotel_id = {newHotelID} WHERE customer_id = {customerID}";

            await using (var cmd = _db.CreateCommand(updateQuery))
            {
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Booking successfully updated with new hotel.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a relevant hotel ID.");
        }
    }

    public async Task ChangeRoom(int customerID)
    {
        Console.Clear();

        string hotelIdQuery = $"SELECT hotel_id FROM bookedrooms WHERE customer_id = {customerID}";
        int hotelId;

        await using (var cmd = _db.CreateCommand(hotelIdQuery))
        {
            var result = await cmd.ExecuteScalarAsync();
            hotelId = Convert.ToInt32(result);
        }

       
        string roomsQuery = $"SELECT * FROM rooms WHERE hotel_id = {hotelId}";

        await using (var cmd = _db.CreateCommand(roomsQuery))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"Room ID: {reader.GetInt32(0)}");
                Console.WriteLine($"Room Size: {reader.GetString(1)}");
                Console.WriteLine($"Price: {reader.GetDecimal(2)}");
                Console.WriteLine();
            }
        }

        Console.Write("Select a new room by entering its ID:");

        if (int.TryParse(Console.ReadLine(), out int newRoomID))
        {
            string updateQuery = $"UPDATE bookedrooms SET room_id = {newRoomID} WHERE customer_id = {customerID}";

            await using (var cmd = _db.CreateCommand(updateQuery))
            {
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Booking successfully updated with the new room.");
            }
        }
        else
        {
            Console.WriteLine("Invalid. enter a valid room ID.");
        }
    }
}












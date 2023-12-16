using Npgsql;
using System;
using System.Security.Cryptography.X509Certificates;

public class Bookings
{
    NpgsqlDataSource _db;

    public Bookings(NpgsqlDataSource db)
    {
        _db = db;
    }
    public async Task Book()
    {
        Console.Clear();
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
        Console.WriteLine(result);


        Console.Write("Enter the hotel ID to proceed: ");
        if (int.TryParse(Console.ReadLine(), out int selectedHotelId))
        {
            Console.WriteLine($"You selected Hotel ID {selectedHotelId}");
            showRoom show = new showRoom(_db);
            await show.ShowRoomsInSelectedHotel(selectedHotelId);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid hotel ID.");
        }
    }

    public class showRoom
    {
        NpgsqlDataSource _db;

        public showRoom(NpgsqlDataSource db)
        {
            _db = db;
        }
        public async Task ShowRoomsInSelectedHotel(int selectedHotelId)
        {
            Console.WriteLine($"Rooms in the selected hotel (Hotel ID: {selectedHotelId})");

            string qroomsInSelectedHotel = $@"
                 SELECT room_id, size, price
                 FROM rooms
                 WHERE hotel_id = {selectedHotelId}
                 ";

            NpgsqlDataReader reader = await _db.CreateCommand(qroomsInSelectedHotel).ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int roomId = reader.GetInt32(0);
                string roomSize = reader.GetString(1);
                decimal price = reader.GetDecimal(2);

                Console.WriteLine($"Room ID: {roomId}, Size: {roomSize}, Price: {price}");
            }

            reader.Close();
        }

    }
}

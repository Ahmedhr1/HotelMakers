using HotelMakers_8;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class Bookings
{
    NpgsqlDataSource _db;

    public Bookings(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task ChooseCustomer()
    {
        Console.Clear();

        string qchoose = @"
          SELECT * FROM customers
    
         ";

        string iterate = string.Empty;
        NpgsqlDataReader reader = await _db.CreateCommand(qchoose).ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            iterate += reader.GetInt32(0);
            iterate += " - ";
            iterate += reader.GetString(1);
            iterate += " ";
            iterate += reader.GetString(2);
            iterate += "\n";


        }
        Console.WriteLine(iterate);

        Console.Write("Enter Customer ID to choose Customer: ");
        if (int.TryParse(Console.ReadLine(), out int CustomerID))
        {
            Console.Clear();

            // så att namnet på det id du val selectas ut i terminalen
            string qGetName = $"SELECT first_name FROM customers WHERE customer_id = {CustomerID}";
            string Qgetlastname = $"SELECT last_name FROM customers WHERE customer_id = {CustomerID}";

            string firstname = await _db.CreateCommand(qGetName).ExecuteScalarAsync() as string;
            string lastname = await _db.CreateCommand(Qgetlastname).ExecuteScalarAsync() as string;

            Bookings booking = new Bookings(_db);
            await booking.CreateBooking();

            Console.WriteLine($"Selected Customer: {firstname} {lastname}");
           



        }
        else
        {
            Console.WriteLine("Please enter a existing Customer ID.");
        }

      

    }

    public async Task attractions()
    {
        Console.WriteLine("What attractions would you like");
        Console.WriteLine("Type 1 for: Pool");
        Console.WriteLine("Type 2 for: Restaurant");
        Console.WriteLine("Type 3 for: Kids Club");
        Console.WriteLine("Type 4 for: Night Fun");




        int choosing;
        if (int.TryParse(Console.ReadLine(), out choosing))
        {
            switch (choosing)
            {
               
                case 1:
                    Console.Clear();
                    Console.WriteLine("Hotels with Pool:");

                    string poolQuery = "SELECT hotels.hotel_id, hotels.hotel_name,hotels.rating, locations.city, locations.address, hotel_entertainment.entertainment_name FROM hotels LEFT JOIN locations using (location_id) LEFT JOIN hotel_entertainment using (hotel_id); ";

                    await using (var cmd = _db.CreateCommand(poolQuery))
                    {

                      await using (var reader = cmd.ExecuteReader())
                       {
                            while (await reader.ReadAsync())
                            {
                                Console.WriteLine($"Hotel ID: {reader.GetInt32(0)}"); 
                                Console.WriteLine($"Hotel Name: {reader.GetString(1)}");
                                Console.WriteLine($"Hotel Rating: {reader.GetDecimal(2)}");
                                Console.WriteLine($"City: {reader.GetString(3)}");
                                Console.WriteLine($"Address: {reader.GetString(4)}");
                                Console.WriteLine($"Entertainment Name: {reader.GetString(5)}");
                                Console.WriteLine(); 

                            }
                        }

                    }

                break;

                case 2:
                    Console.WriteLine("Hotels with Restaurants:");


                    break;
                case 3:
                    //Cancel/alter booking
                    // Showallbookings();
                    break;
                case 4:

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break; //fixa så att man loopar tillbaks till att välja igen.
            }
        }
        else
        {
            Console.WriteLine("Invalid choice, Try again");
            //continue;


        }



    }



    public async Task CreateBooking()
    {
        Console.Clear();


        Bookings Attraction = new Bookings(_db);
        await Attraction.attractions();


        Console.WriteLine("Choose one of the following Hotels:");
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
            Console.Clear();
            Console.WriteLine($"You selected Hotel ID {selectedHotelId}");
            Room show = new Room(_db);
            await show.ShowRoomsInSelectedHotel(selectedHotelId);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid hotel ID.");
        }
    }



    public class Room
    {
        NpgsqlDataSource _db;

        public Room(NpgsqlDataSource db)
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

            while (await reader.ReadAsync()) // denna läser ut rows tills det inte finns rows att läsa ut mer
            {
                int roomId = reader.GetInt32(0);
                string roomSize = reader.GetString(1);
                decimal price = reader.GetDecimal(2);

                Console.WriteLine($"Room ID: {roomId}, Size: {roomSize}, Price: {price}");
            }

            Console.Write("Enter the room ID to proceed: ");
            if (int.TryParse(Console.ReadLine(), out int Roomid))
            {
                Console.Clear();
                Console.WriteLine($"You selected Hotel ID {Roomid}");
               
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid hotel ID.");
            }
            reader.Close();
        }

    }
}

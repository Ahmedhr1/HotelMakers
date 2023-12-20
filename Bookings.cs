using HotelMakers_8;
using Npgsql;
using Npgsql.Internal.Postgres;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Bookings
{
    NpgsqlDataSource _db;

    public Bookings(NpgsqlDataSource db)
    {
        _db = db;
    }
    
    private int _customerId;
    private int _hotelId;
    private int _roomId;
    private DateOnly _checkInDate;
    private DateOnly _checkOutDate;

    public async Task<int?> ChooseCustomer()
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

            Console.WriteLine($"Selected Customer: {firstname} {lastname}");


            Bookings booking = new Bookings(_db);
            await booking.CreateBooking();


            _customerId = CustomerID;
            return CustomerID;
        }
        else
        {
            Console.WriteLine("Please enter a existing Customer ID.");
            return null;
        }
    }

    public async Task attractions()
    {
        
            Console.WriteLine("What attractions would you like");
            Console.WriteLine("Type 1 for: Pool");
            Console.WriteLine("Type 2 for: Restaurant");
            Console.WriteLine("Type 3 for: Kids Club");
            Console.WriteLine("Type 4 for: Night Fun");

            if (int.TryParse(Console.ReadLine(), out int choosing) && choosing >= 1 && choosing <= 4)
            {
                switch (choosing)
                {

                    case 1:
                        Console.Clear();
                        Console.WriteLine("Hotels with Pool:");
                        Console.WriteLine();

                        string poolQuery = "SELECT hotels.hotel_id, hotels.hotel_name,hotels.rating, locations.city, locations.address, hotel_entertainment.entertainment_name FROM hotels LEFT JOIN locations using (location_id) LEFT JOIN hotel_entertainment using (hotel_id) WHERE entertainment_name = 'Pool'; ";

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
                        Console.Clear();
                        Console.WriteLine("Hotels with Restaurants:");
                        Console.WriteLine();

                        string RestaurantQuery = 
                          @"SELECT hotels.hotel_id, hotels.hotel_name, hotels.rating, locations.city, locations.address, hotel_entertainment.entertainment_name 
                          FROM hotels LEFT JOIN locations using (location_id) 
                          LEFT JOIN hotel_entertainment using (hotel_id) 
                          WHERE entertainment_name = 'Restaurant'; ";

                        await using (var cmd = _db.CreateCommand(RestaurantQuery))
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
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Hotels with Kids Clubs:");
                        Console.WriteLine();

                          string KidsClubsQuery = @"SELECT hotels.hotel_id, hotels.hotel_name, hotels.rating, locations.city, locations.address, hotel_entertainment.entertainment_name 
                          FROM hotels LEFT JOIN locations using (location_id) 
                          LEFT JOIN hotel_entertainment using (hotel_id) 
                          WHERE entertainment_name = 'Kids_Club'; "; 
                    
                        await using (var cmd = _db.CreateCommand(KidsClubsQuery))
                        await using (var reader = cmd.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                Console.WriteLine($"Hotel ID: {reader.GetInt32(0)}");
                                Console.WriteLine($"Hotel Name: {reader.GetString(1)}");
                                Console.WriteLine($"Rating: {reader.GetDecimal(2)}");
                                Console.WriteLine($"City: {reader.GetString(3)}");
                                Console.WriteLine($"Address: {reader.GetString(4)}");
                                Console.WriteLine($"Entertainments: {reader.GetString(5)}");
                                Console.WriteLine();
                            }




                        }
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Hotels with Night Fun:");
                        Console.WriteLine();

                          string NightFunQ = @"SELECT hotels.hotel_id, hotels.hotel_name, hotels.rating, locations.city, locations.address, hotel_entertainment.entertainment_name 
                          FROM hotels LEFT JOIN locations using (location_id) 
                          LEFT JOIN hotel_entertainment using (hotel_id) 
                          WHERE entertainment_name = 'Night_Fun'; "; 

                        await using (var cmd = _db.CreateCommand(NightFunQ))
                        await using (var reader = cmd.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                Console.WriteLine($"Hotel ID: {reader.GetInt32(0)}");
                                Console.WriteLine($"Hotel Name: {reader.GetString(1)}");
                                Console.WriteLine($"Rating: {reader.GetDecimal(2)}");
                                Console.WriteLine($"City: {reader.GetString(3)}");
                                Console.WriteLine($"Address: {reader.GetString(4)}");
                                Console.WriteLine($"Entertainments: {reader.GetString(5)}");
                                Console.WriteLine();
                               
                            }
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break; //fixa så att man loopar tillbaks till att välja igen.
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again");
                //continue;


            }

        

    }

    //fixa så att man i slutet av bokningen lägger in vilka dates

    public async Task<int> CreateBooking()
    {


        Bookings Attraction = new Bookings(_db);
        await Attraction.attractions();

        Console.ReadKey();

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
        if (int.TryParse(Console.ReadLine(), out int HotelId))
        {
            Console.Clear();
            Console.WriteLine($"You selected Hotel ID {HotelId}");

            Bookings sshow = new Bookings(_db);
            await sshow.ShowRooms(HotelId);
        }
        else
        {
            
            Console.WriteLine("Invalid input. Please enter a valid hotel ID.");
        }

        _hotelId = HotelId;
        //Bookings show = new Bookings(_db);
        //await show.ShowRooms(selectedHotelId);
        return _hotelId;
    }

        public async Task<int> ShowRooms(int HotelId)
        {
            Console.WriteLine($"Rooms in the selected hotel (Hotel ID: {HotelId})");
            Console.WriteLine();

            string qroomsInSelectedHotel = $@"
                 SELECT room_id, size, price
                 FROM rooms
                 WHERE hotel_id = {HotelId}
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
                Console.WriteLine($"You selected Room ID {Roomid}");
                await BookingDates();
               
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid hotel ID.");
            }

            _roomId = Roomid;
            return _roomId;
        }


    public async Task<DateOnly> BookingDates()
    {
        Console.WriteLine("Enter Check-in date: ");
        DateOnly Checkin = DateOnly.Parse(Console.ReadLine());

        Console.WriteLine("Enter Check-out date: ");
        DateOnly Checkout = DateOnly.Parse(Console.ReadLine());

        Console.WriteLine($"Youre stay is from {Checkin} to {Checkout}");

        Checkout = _checkOutDate; 

        return _checkOutDate;


    }

    public async Task Addons() { }

    public async Task InsertAll(int CustomerID, int hotelID, int room_id, DateOnly CheckIn, DateOnly CheckOut)
    {

        string insert = $@"INSERT INTO bookedrooms (room_id, customer_id, check_in, check_out) 
                        VALUES({CustomerID}, {room_id}, {hotelID}, {CheckIn}, {CheckOut})";

        await using (var cmd = _db.CreateCommand(insert))
        {

            cmd.Parameters.AddWithValue("room_id", room_id);
            cmd.Parameters.AddWithValue("customer_id", CustomerID);
            cmd.Parameters.AddWithValue("hotel_id", hotelID);
            cmd.Parameters.AddWithValue("check_in", CheckIn);
            cmd.Parameters.AddWithValue("check_out", CheckOut);

            await using (var reader = await cmd.ExecuteReaderAsync()) ;

        }

    }
    public async Task BuildBooking()
    {
        int customerId = (int)await ChooseCustomer();
        int hotelId = await CreateBooking();
        int roomId = await ShowRooms(hotelId);

        DateOnly bookingDate = (DateOnly)await BookingDates();
        DateOnly checkIn = bookingDate;
        DateOnly checkOut = bookingDate;

        await InsertAll(_customerId, _hotelId, _roomId, _checkInDate, _checkOutDate);

     
    }


}

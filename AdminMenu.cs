using HotelMakers_8;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AdminMenu
{
    NpgsqlDataSource _db; 

    public AdminMenu(NpgsqlDataSource db) 
    {
        _db = db; 
    }

    public async Task Main(NpgsqlDataSource db)
    {
        Console.Clear();
        Console.WriteLine("Choose Option:");
        Console.WriteLine("1. Register Customer");
        Console.WriteLine("2. Add Booking");
        Console.WriteLine("3. Show Bookings");


        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid choice, Try again");
            //continue;
        }


        switch (choice)
        {
            case 1:
                Customer customero = new Customer(_db);
                await customero.InsertCustomer();
                break;
            case 2:
                Bookings choosing = new Bookings(_db);
                await choosing.BuildBooking();
                break;
            case 3:
                ViewOfBooking show = new ViewOfBooking(_db);
                await show.ShowAllBookings();

                break;
            case 4:
                
            //search with price/distance med order by

            default:
                Console.WriteLine("Ogiltigt val. Försök igen.");
                break; 
        }

    }
}
    
    

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
        Console.WriteLine("1. Add Booking");
        Console.WriteLine("2. Show Bookings");


        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Ogiltigt val. Försök igen.");
            //  continue;
        }


        switch (choice)
        {

            case 1:
                Bookings bookings = new Bookings(_db);
                await bookings.Book();
                break;
            case 2:
                // AddBooking();
                break;
            case 3:
                Console.WriteLine("Avslutar programmet...");
                return;
            default:
                Console.WriteLine("Ogiltigt val. Försök igen.");
                break;
        }

    }
}
    
    

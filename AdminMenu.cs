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
            // måste fixa så att man först kommer in som customer sedan fortsätter till bokning 
            //När information ska in till någon table använd query insert into
            // hur ska vi söka 
            case 1:
                //Register customer();
                Customer customero = new Customer(_db);
                await customero.InsertCustomer();

                break;
            case 2:
                Bookings customer = new Bookings(_db);
                await customer.ChooseCustomer();
                //Bookings bookings = new Bookings(_db);
                //await bookings.Book();
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
}
    
    

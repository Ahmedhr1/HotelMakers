using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Addon
{

    NpgsqlDataSource _db;

    public Addon(NpgsqlDataSource db)
    {
        _db = db;
    }


    public async Task Addons()
    {

    
            Console.WriteLine("What addons would like:");
            Console.WriteLine("type 1 for: Extrabed");
            Console.WriteLine("type 2 for: Halfboard");
            Console.WriteLine("type 3 for: Allinclusive");

            if (int.TryParse(Console.ReadLine(), out int choosing) && choosing >= 1 && choosing <= 3)
            {

                Console.WriteLine("Invalid choice, try again");

                switch (choosing)
                {
                    case 1:
                        int extrabed = 1;
                        Console.Clear();
                        Console.WriteLine("Extrabed Added");
                        Console.WriteLine();
                        break;

                    case 2:
                        int halfboard = 2;
                        Console.Clear();
                        Console.WriteLine("Halfboard Added");
                        Console.WriteLine();
                        break;

                    case 3:
                        int allinclusive = 3;
                        Console.Clear();
                        Console.WriteLine("Allinclusive Added");
                        break;

                }
            }

            else
            {
                Console.WriteLine("please enter a valid number!");
                Console.Clear();
                Addons();
            }





    }


}













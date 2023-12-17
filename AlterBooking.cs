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

    // Fixa så att alla bokningar visas i en lista
    // välj sedan en persons bokningar
    //personens bokning ska visas som den visas i table
    //därefter ska det finnas två val Alter eller Cancel Booking


}

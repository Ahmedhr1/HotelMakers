using Npgsql;

const string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=Hotel8";
await using var db = NpgsqlDataSource.Create(dbUri);

AdminMenu adminMenu = new AdminMenu(db);


    Console.WriteLine("Welcome to HolidayMakers8!");
    Console.ReadKey();


// Call the Book method
await adminMenu.Main(db);




﻿using Npgsql;

const string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=Hotel8";
await using var db = NpgsqlDataSource.Create(dbUri);

AdminMenu adminMenu = new AdminMenu(db); 


    Console.WriteLine("Welcome to HoLiDaYMaKeRs!!!");
    Console.ReadKey();


// Calla Book methoden
await adminMenu.Main(db);




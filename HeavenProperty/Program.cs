using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeavenProperty.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HeavenProperty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //createDB();
        }

        static void createDB()
        {
            Console.WriteLine("** C# CRUD sample with Entity Framework Core and SQL Server **\n");
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";
                builder.UserID = "sa";
                builder.Password = "yourStrong(!)Password";
                builder.InitialCatalog = "HeavenProperty";

                using (HeavenPropertyContext context = new HeavenPropertyContext(builder.ConnectionString))
                {
                    
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    Console.WriteLine("Created database schema from C# classes.");

                    // Create demo: Create a User instance and save it to the database
                    Seller newUser = new Seller
                    {
                        FirstName = "Anna",
                        LastName = "Shrestinian",
                        Address = "Jonhsonville, Wellington",
                        Email = "Anna@gmail.com",
                        Password = "anna@123",
                        Phone = "0232453789"
                    };
                    context.Sellers.Add(newUser);

                    context.SaveChanges();
                    Console.WriteLine("\nCreated Seller: " + newUser.ToString());

                    // Create demo: Create a Task instance and save it to the database
                    Property newTask = new Property()
                    {
                        Title = "Ship Helsinki",
                        Seller_Id = 1,
                        BathRooms = 1,
                        Rooms = 2,
                        CarParkings = 3,
                        Email = "test@gmail.com",
                        FloorArea = "80",
                        LandArea = "100",
                        Location = "18 Bell Street, Tawa, Wellington",
                        RV = "$1000,000",
                        Type = "House"
                    };
                    context.Properties.Add(newTask);
                    context.SaveChanges();
                    Console.WriteLine("\nCreated Property: " + newTask.ToString());

                    // Association demo: Assign task to user
                    newTask.AssignedTo = newUser;
                    context.SaveChanges();
                    Console.WriteLine("\nAssigned Property: '" + newTask.Title + "' to seller '" + newUser.GetFullName() + "'");

                    // Read demo: find Property assigned to user 'Anna'
                    Console.WriteLine("\ntasks assigned to 'Anna':");
                    var query = from t in context.Properties
                                where t.AssignedTo.FirstName.Equals("Anna")
                                select t;
                    foreach (var t in query)
                    {
                        Console.WriteLine(t.ToString());
                    }

                    // Update demo:
                    Property taskToUpdate = context.Properties.First(); // get the first Property
                    Console.WriteLine("\nUpdating Property: " + taskToUpdate.ToString());
                    taskToUpdate.LandArea = "90";
                    context.SaveChanges();

                    // Delete demo: delete all Properties
                    Console.WriteLine("\nDeleting all Properties");

                    query = from t in context.Properties
                            select t;
                    foreach (Property t in query)
                    {
                        Console.WriteLine("Deleting Property: " + t.ToString());
                        context.Properties.Remove(t);
                    }
                    context.SaveChanges();

                    // Show Properties after the 'Delete' operation - there should be 0 Properties
                    Console.WriteLine("\nProperties after delete:");
                    List<Property> tasksAfterDelete = (from t in context.Properties select t).ToList<Property>();
                    if (tasksAfterDelete.Count == 0)
                    {
                        Console.WriteLine("[None]");
                    }
                    else
                    {
                        foreach (Property t in query)
                        {
                            Console.WriteLine(t.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

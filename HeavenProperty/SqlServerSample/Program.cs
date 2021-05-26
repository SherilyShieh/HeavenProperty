using System;
using System.Text;
using Microsoft.Data.SqlClient;

namespace SqlServerSample
{
    class Program
    {
        void Main(string[] args)
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";
                builder.UserID = "SA";
                builder.Password = "yourStrong(!)Password";
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    // Create database - HeavenProperty Database
                    Console.Write("Dropping and creating database 'HeavenProperty' ... ");
                    String sql = "DROP DATABASE IF EXISTS [HeavenProperty]; CREATE DATABASE [HeavenProperty]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }

                    // Create Property Table and insert some sample data
                    Console.Write("Creating Property table with data, press any key to continue...");
                    Console.ReadKey(true);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE HeavenProperty; ");
                    sb.Append("CREATE TABLE Property ( ");
                    sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" Seller_Id INT NOT NULL, ");
                    sb.Append(" Title NVARCHAR(200) NOT NULL, ");
                    sb.Append(" Location NVARCHAR(200) NOT NULL, ");
                    sb.Append(" Rooms INT NOT NULL, ");
                    sb.Append(" BathRomms INT NOT NULL, ");
                    sb.Append(" CarParkings INT NOT NULL, ");
                    sb.Append(" Type NVARCHAR(200) NOT NULL, ");
                    sb.Append(" FloorArea NVARCHAR(200) NOT NULL, ");
                    sb.Append(" LandArea NVARCHAR(200) NOT NULL, ");
                    sb.Append(" RV NVARCHAR(200) NOT NULL, ");
                    sb.Append(" Email NVARCHAR(200) NOT NULL, ");
                    sb.Append(" Icon NVARCHAR(200), ");
                    sb.Append("); ");
                    sb.Append("INSERT INTO Property (Seller_Id, Title, Location, Rooms, BathRomms, CarParkings, Type, FloorArea, LandArea, RV, Email, Icon) VALUES ");
                    sb.Append("(N'1', N'Tranquil Tree House', N'11a Totara Road, Miramar, Wellington, Wellington', N'2', N'1', N'1', N'House', N'80', N'100', N'$1000,000',  N'test@gmail.com', N''); ");

                    // Create Seller Table and insert some sample data

                    sb.Append("USE HeavenProperty; ");
                    sb.Append("CREATE TABLE Seller ( ");
                    sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" FirstName NVARCHAR(200) NOT NULL, ");
                    sb.Append(" LastName NVARCHAR(200) NOT NULL, ");
                    sb.Append(" Address NVARCHAR(200) NOT NULL, ");
                    sb.Append(" Phone NVARCHAR(200) NOT NULL, ");
                    sb.Append(" Email NVARCHAR(200) NOT NULL, ");
                    sb.Append(" Password NVARCHAR(200) NOT NULL, ");
                    sb.Append("); ");
                    sb.Append("INSERT INTO Seller (FirstName, LastName, Address, Phone, Email, Password) VALUES ");
                    sb.Append("(N'Sherily', N'Sheieh', N'11a Totara Road, Miramar, Wellington, Wellington', N'0273212675', N'test@gmail.com', N'test@123'); ");


                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }

                    // INSERT demo
                    Console.Write("Inserting a new row into table, press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("INSERT Seller (FirstName, LastName, Address, Phone, Email, Password) ");
                    sb.Append("VALUES (@firstname, @lastname, @address, @phone, @email, @password);");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", "Ryan");
                        command.Parameters.AddWithValue("@lastname", "Smith");
                        command.Parameters.AddWithValue("@address", "Lower Hutt, Wellington");
                        command.Parameters.AddWithValue("@phone", "0212653456");
                        command.Parameters.AddWithValue("@email", "ryan@gmail.com");
                        command.Parameters.AddWithValue("@password", "ryan@123");
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) inserted");
                    }

                    // UPDATE demo
                    String userToUpdate = "Sherily";
                    Console.Write("Updating 'Location' for user '" + userToUpdate + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("UPDATE Seller SET Address = N'Tawa, Wellington' WHERE FirstName = @firstname");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", userToUpdate);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) updated");
                    }

                    // DELETE demo
                    String userToDelete = "Ryan";
                    Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("DELETE FROM Seller WHERE FirstName = @firstname;");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", userToDelete);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) deleted");
                    }

                    // READ demo
                    Console.WriteLine("Reading data from table, press any key to continue...");
                    Console.ReadKey(true);
                    sql = "SELECT Id, FirstName, Address FROM Seller;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }
    }
    }
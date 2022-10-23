﻿using Npgsql;

namespace ChatServer
{
    public class Queries
    {
        private static string connString = "Host=localhost;Username=postgres;password=password;Database=postgres"; //Set to your own

        public static async void WriteMessageQuery(string id, string conversation_id, string author_id, string message)
        {
            await using var dbConn = new NpgsqlConnection(connString);
            await dbConn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("INSERT INTO messages(id, conversation_id, author_id, message) VALUES (@id, @c_id, @a_id, @msg)", dbConn))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("c_id", conversation_id);
                cmd.Parameters.AddWithValue("a_id", author_id);
                cmd.Parameters.AddWithValue("msg", message);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void WriteRoomUsers(string user_id, string room_id)
        {
            await using var dbConn = new NpgsqlConnection(connString);
            await dbConn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("INSERT INTO room_users(user_id, room_id) VALUES (@u_id, @r_id)", dbConn))
            {
                cmd.Parameters.AddWithValue("u_id", user_id);
                cmd.Parameters.AddWithValue("r_id", room_id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void WriteRooms(string room_id)
        {
            await using var dbConn = new NpgsqlConnection(connString);
            await dbConn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("INSERT INTO rooms(room_id) VALUES (@r_id)", dbConn))
            {
                cmd.Parameters.AddWithValue("r_id", room_id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void WriteUsers(string user_id, string username, string password, string email, DateTime date_joined)
        {
            await using var dbConn = new NpgsqlConnection(connString);
            await dbConn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("INSERT INTO users(id, username, password, email, date_joined) VALUES (@u_id, @u_name, @pwd, @email, @date_joined)", dbConn))
            {
                cmd.Parameters.AddWithValue("u_id", user_id);
                cmd.Parameters.AddWithValue("u_name", username);
                cmd.Parameters.AddWithValue("pwd", password);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("date_joined", date_joined);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void ReadUsers()
        {
            await using var dbConn = new NpgsqlConnection(connString);
            await dbConn.OpenAsync();
            await using(var cmd = new NpgsqlCommand("SELECT * FROM users", dbConn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while(await reader.ReadAsync())
                {
                    Console.WriteLine($"u_id: {reader.GetString(0)}");
                    Console.WriteLine($"Username: {reader.GetString(1)}");
                    Console.WriteLine($"Password: {reader.GetString(2)}");
                    Console.WriteLine($"Email: {reader.GetString(3)}");
                    
                    
                }
            }
            
        }
    }
}
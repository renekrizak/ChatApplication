using Npgsql;
using System.Diagnostics;

namespace ChatServer
{
    public class Queries
    {
        private static string connString = ""; //Set to your own

        public static string[] ReadLastMessages()
        {
            string[] lastMessages = new string[40];
            var dbConn = new NpgsqlConnection(connString);
            dbConn.Open();
            int i = 0;
            try
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM messages ORDER BY ID desc", dbConn))
                {
                    using NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (i < 40)
                        {
                            lastMessages[i] += rdr.GetString(1);
                            lastMessages[i + 1] += rdr.GetString(2);
                            i += 2;
                        }
                        else
                        {
                            return lastMessages;
                        }
                        //Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)}");
                    }
                }
                return lastMessages;
            } catch (Exception e)
            {
                Debug.Write(e);
            }
            return lastMessages;
           
        }

        public static async void LogMessage(string username, string message)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] Storing message: [{message}] from User: [{username}] to database.");
            await using var dbConn = new NpgsqlConnection(connString);
            await dbConn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("INSERT INTO messages(author_Username, message) VALUES (@username, @message)", dbConn))
            {
                //need to remove null characters since postgresql cant write them into text
                var temp1 = username.Replace("\0", String.Empty);
                var temp2 = message.Replace("\0", String.Empty);
                cmd.Parameters.AddWithValue("username", temp1);
                cmd.Parameters.AddWithValue("message", temp2);
                if (message != null && username != null)
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async void RegisterClientQuery(Guid UID, string username, string email, string password, DateTime date)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] User: [{username}] has registered.");
            await using var dbConn = new NpgsqlConnection(connString);
            await dbConn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("INSERT INTO users(id, username, password, email, date_joined) VALUES(@id, @username, @password, @email, @date_joined)", dbConn))
            {
                cmd.Parameters.AddWithValue("id", UID.ToString());
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("@date_joined", date);
                await cmd.ExecuteNonQueryAsync();

            }
        }

        public static string ReturnIDQuery(string username, string password)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] User: [{username}] has connected to server");
            using var dbConn = new NpgsqlConnection(connString);
            dbConn.Open();
            using (var cmd = new NpgsqlCommand($"SELECT ID FROM users WHERE username='{username}' and password='{password}'", dbConn))
            {
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return "No ID";
                }
            }

        }

        public static async void ReadUsers()
        {
            await using var dbConn = new NpgsqlConnection(connString);
            await dbConn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM users", dbConn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
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
using Npgsql;
using System;

namespace real_time_horror_group4
{
    public class Getleaderboard
    {
        private readonly NpgsqlConnection _db;

        public Getleaderboard(NpgsqlConnection db)
        {
            _db = db;
        }

        public string leaderboard()
        {
            string qleaderboard = @"SELECT * FROM leaderboard;";

            string result = string.Empty;

            try
            {
                _db.Open();
                using (var command = new NpgsqlCommand(qleaderboard, _db))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result += reader.GetInt32(0);
                        result += ", ";
                        result += reader.GetString(1);
                    }
                }
            }
            finally
            {
                _db.Close();
            }

            return result;
        }
    }
}

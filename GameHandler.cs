using System.Security.Cryptography;
using Npgsql; 
namespace real_time_horror_group4;

public class GameHandler
{
    private readonly NpgsqlDataSource _db;
    public GameHandler(NpgsqlDataSource db)
    {
        _db = db;
    }

    public string StartMatch(string body)
    {
        string[] userInput = body.Split(',');
        int player1 = int.Parse(userInput[0]);
        int player2 = int.Parse(userInput[1]);

        using (var cmd = _db.CreateCommand(@"INSERT INTO games(
player_1, player_2, 
player_1_rights,player_1_questions_answered,
player_2_rights,player_2_questions_answered,
active) VALUES ($1, $2, 0, 0, 0, 0, true);"))
        {
            cmd.Parameters.AddWithValue(player1);
            cmd.Parameters.AddWithValue(player2);
            cmd.ExecuteNonQuery();

        }

        return "successfully joined match";
    }
}
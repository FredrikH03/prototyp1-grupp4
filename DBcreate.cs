namespace real_time_horror_group4;
using Npgsql;
public class Tables(NpgsqlDataSource db)
{
    public async Task CreateTables()
    {
        await db.CreateCommand("DROP TABLE IF EXISTS correctanswers").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS questions").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS qusers").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS Leaderboard").ExecuteNonQueryAsync();

        // skapar userID automatiskt.
        string qUsers = @" 
         CREATE TABLE IF NOT EXISTS Users( 
         ID SERIAL PRIMARY KEY, 
         username TEXT, password TEXT
        );";

        string qQuestions = @"
         CREATE TABLE IF NOT EXISTS Questions(
         ID SERIAL PRIMARY KEY, questions TEXT
        );";


        string qAnswers = @"
         CREATE TABLE IF NOT EXISTS CorrectAnswers( 
         ID SERIAL PRIMARY KEY, answer TEXT, 
         questionID INT REFERENCES Questions(ID)
        );";


        string qLeaderboard = @"
         CREATE TABLE IF NOT EXISTS Leaderboard( ID SERIAL PRIMARY KEY, wins INT,
         losses INT, userID INT REFERENCES Users(ID)
         );";

        await db.CreateCommand(qUsers).ExecuteNonQueryAsync();
        await db.CreateCommand(qQuestions).ExecuteNonQueryAsync();
        await db.CreateCommand(qAnswers).ExecuteNonQueryAsync();
        await db.CreateCommand(qLeaderboard).ExecuteNonQueryAsync();

    }





}
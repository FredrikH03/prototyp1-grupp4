namespace real_time_horror_group4;
using Npgsql;
public class Tables(NpgsqlDataSource db)
{
    public async Task CreateTables()
    {
        await db.CreateCommand("DROP TABLE IF EXISTS correctanswers").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS questions").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS users").ExecuteNonQueryAsync();

        string qUsers = @"
         CREATE TABLE IF NOT EXISTS users(
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

        await db.CreateCommand(qUsers).ExecuteNonQueryAsync();
        await db.CreateCommand(qQuestions).ExecuteNonQueryAsync();
        await db.CreateCommand(qAnswers).ExecuteNonQueryAsync();
    }

    public async Task AddUsersToDatabase()
    {
        string command = @"INSERT INTO users(username, password)
                                           VALUES ('test', 'test');";

        await db.CreateCommand(command).ExecuteNonQueryAsync();
    }
}
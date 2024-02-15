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


        string qInsertQ = @"INSERT INTO questions (id,questions) values
         (1,'What is a namespace in C#?'),
         (2,'What is inheritance?'),
         (3,'What is encapsulation?'),
         (4,'What is the this keyword in C#?'),
         (5,'What is the difference between == and Equals() in C#?'),
         (6,'What is the purpose of the async and await keywords in C#'),
         (7,'What does HTTP stand for'),
         (8,'Whats Manuels main programming language?'),
         (9,'Which SQL statement is used to delete records from a database?'),
         (10,'What does the SQL keyword DISTINCT do?'

        );";

        string qInsertA = @"INSERT INTO correctanswers (answer, questionid) VALUES
         ('c', 1),
         ('b', 2),
         ('c', 3),
         ('a', 4),
         ('a', 5),
         ('b', 6),
         ('c', 7),
         ('b', 8),
         ('a', 9),
         ('b', 10);";


        await db.CreateCommand(qInsertQ).ExecuteReaderAsync();
        await db.CreateCommand(qInsertA).ExecuteReaderAsync();


    }





}

    






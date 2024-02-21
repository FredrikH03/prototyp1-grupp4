using Npgsql;
public class Tables(NpgsqlDataSource db)
{
    public async Task CreateTables()
    {
        await db.CreateCommand("DROP TABLE IF EXISTS correctanswers").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS questions").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS qusers").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS Leaderboard").ExecuteNonQueryAsync();
        await db.CreateCommand("DROP TABLE IF EXISTS options").ExecuteNonQueryAsync();


        string qUsers = @"
         CREATE TABLE IF NOT EXISTS Users(
         ID SERIAL PRIMARY KEY, 
         username TEXT UNIQUE, password TEXT
        );";

        string qOptions = @"
         CREATE TABLE IF NOT EXISTS Options(
         ID SERIAL PRIMARY KEY, A TEXT, B TEXT, C TEXT

        );";

        string qQuestions = @"
         CREATE TABLE IF NOT EXISTS Questions(
         ID SERIAL PRIMARY KEY, questions TEXT, optionsID INT REFERENCES Options(ID)
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
        await db.CreateCommand(qOptions).ExecuteNonQueryAsync();
        await db.CreateCommand(qQuestions).ExecuteNonQueryAsync();
        await db.CreateCommand(qAnswers).ExecuteNonQueryAsync();
        await db.CreateCommand(qLeaderboard).ExecuteNonQueryAsync();


        string qOptions1 = @"INSERT INTO Options (A, B, C) VALUES
         ('A region of memory reserved for storing variables','A way to organize and group related classes, interfaces','A keyword used for conditional statements'),
         ('olijo','A way to organize and group related classes, interfaces','A keyword used for conditional statements'),
         ('kiko','A way to organize and group related classes, interfaces','A keyword used for conditional statements'),
         ('erio','A way to organize and group related classes, interfaces','A keyword used for conditional statements'),
         ('pipo','A way to organize and group related classes, interfaces','A keyword used for conditional statements'

        );";


        string qInsertQ = @"INSERT INTO questions (id,questions,optionsid) values
                 (1,'What is a namespace in C#?',1), 
                 (2,'What is inheritance?',2),
                 (3,'What is encapsulation?',3),
                 (4,'What is the this keyword in C#?',4),
                 (5,'What is the difference between == and Equals() in C#?',5

       );";


        string qInsertA = @"INSERT INTO correctanswers (answer, questionid) VALUES
         ('c', 1);";


        await db.CreateCommand(qOptions1).ExecuteReaderAsync();
        await db.CreateCommand(qInsertQ).ExecuteReaderAsync();
        await db.CreateCommand(qInsertA).ExecuteReaderAsync();


    }





}

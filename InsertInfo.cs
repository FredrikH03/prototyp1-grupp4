//using Npgsql;
//using System.IO;
//namespace real_time_horror_group4;

//public class InsertInfo
//{

//    private readonly NpgsqlDataSource _db;
//    public InsertInfo(NpgsqlDataSource db)
//    {
//        _db = db;
//    }

//    public async Task PopulateQuestions()
//    {
//        const string query = @"INSERT INTO Questions(questions) VALUES ($1)";
//        string[] questions = File.ReadAllLines("data/questions.txt");
//        for (int i = 0; i < questions.Length; i++)
//        {
//            string question = questions[i];
//            await using (var cmd = _db.CreateCommand(query))
//            {
//                cmd.Parameters.AddWithValue(question);
//                await cmd.ExecuteNonQueryAsync();
//            }
//        }
//    }

//    public async Task PopulateAnswers()
//    {
//        const string query = @"INSERT INTO CorrectAnswers(answer, questionID) VALUES ($1, $2)";
//        string[] answers = File.ReadAllLines("data/answers.csv");

//        for (int i = 0; i < answers.Length; i++)
//        {
//            string answerLine = answers[i];
//            string[] answerArray = answerLine.Split(",");

//            await using (var cmd = _db.CreateCommand(query))
//            {
//                cmd.Parameters.AddWithValue(answerArray[0]);
//                cmd.Parameters.AddWithValue(int.Parse(answerArray[1]));
//                await cmd.ExecuteNonQueryAsync();
//            }
//        }
//    }
//}
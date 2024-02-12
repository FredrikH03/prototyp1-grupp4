namespace real_time_horror_group4;
using System.IO;

public class InsertQuestions
{

    NpgsqlConnection _db;

    public dbUri(npgsqlDataSource db)
    {
        _db = db;
    }

    public async Task PopulateQuestions()
    {
        const string query = @"INSERT INTO questions(question) VALUES ($1)";
        string[] questions = File.ReadAllLines("data/questions.txt");
        for (int i = 0; i < questions.Length; i++)
        {
            string question = questions[i];
            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Paramaters.AddWithValue(question);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task PopulateAnswers()
    {
        const string query = @"INSERT INTO answers(answer, question_id) VALUES ($1, $2)";
        string[] answers = File.ReadAllLines("data/answers.csv");
        
        for (int i = 0; i < answers.Length; i++)
        {
            string answerLine = answers[i];
            string[] answerArray = answerLine.Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Paramaters.AddwithValue(answerArray[0]);
                cmd.Paramaters.AddwithValue(answerArray[1]);
                await cmd.ExecuteNonQueryAsync();
            }
        }
}
}
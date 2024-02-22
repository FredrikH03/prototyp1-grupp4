using Npgsql;
using Npgsql.Replication.TestDecoding;

namespace real_time_horror_group4;

public class AnswerHandler
{
    private readonly NpgsqlDataSource _db;
    public bool IsCorrectAnswer = false;
    public AnswerHandler(NpgsqlDataSource db)
    {
        _db = db;
    }

    public string AnswerQuestion(string userId, string userAnswer, string questionId)
    {
        
        string answer = "test"; 
        int user = int.Parse(userId);
        int num = int.Parse(questionId);
        string userInput = userAnswer;
        
        using var getAnswer =
            _db.CreateCommand("SELECT answer FROM CorrectAnswers WHERE questionid = $1 AND answer = $2");
        getAnswer.Parameters.AddWithValue(num);
        getAnswer.Parameters.AddWithValue(userInput);
        using var reader2 = getAnswer.ExecuteReader();
        while (reader2.Read())
        {
            answer = reader2.GetString(0);
        }

        //Console.WriteLine(answer);
        if (answer == "test")
        {
            Console.WriteLine("wrong");
            Console.WriteLine(answer);
        }
        else
        {
            Console.WriteLine("Correct");
            Console.WriteLine(answer);
            IsCorrectAnswer = true;
        }

        Console.WriteLine();

        //insert database connection
        
        return answer;
    }

}
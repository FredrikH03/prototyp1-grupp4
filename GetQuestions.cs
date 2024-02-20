using Npgsql;
using Npgsql.Replication.TestDecoding;

namespace real_time_horror_group4;

public class GetQuestions
{
    private readonly NpgsqlDataSource _db;

    public GetQuestions(NpgsqlDataSource db)
    {
        _db = db;
    }

    public string GetRandomQuestion()
    {
        int maxNum = 0;
        string question = "failed";
        string answer = "test";

        Random random = new Random();
        int num = random.Next(1, 31);

        using var getQuestions = _db.CreateCommand("SELECT questions FROM Questions WHERE ID = $1");
        getQuestions.Parameters.AddWithValue(num);
        using var reader = getQuestions.ExecuteReader();
        while (reader.Read())
        {
            question = reader.GetString(0);
        }

        return $"question {num} is as follows: " + question + "\n";

        /*
        */
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
        }
        Console.WriteLine();

        
        
        return answer;
    }

}
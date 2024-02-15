using Npgsql;
namespace real_time_horror_group4;

public class GetQuestions
{
    private readonly NpgsqlDataSource _db;
    public GetQuestions(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task GetRandomQuestion()
    {
        int maxNum = 0;
        string question = "failed";
        string answer = "test";
        
        Random random = new Random();
        int num = random.Next(1, 31);

        await using var getQuestions = _db.CreateCommand($"SELECT questions FROM Questions WHERE ID = {num}");
        await using var reader = await getQuestions.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            question = reader.GetString(0);
        }
        Console.WriteLine("the riddle is as follows: " + question);

        string userInput = Console.ReadLine();

        await using var getAnswer =
            _db.CreateCommand($"SELECT answer FROM CorrectAnswers WHERE questionid = {num} AND answer = '$1'");
        await using var reader2 = await getAnswer.ExecuteReaderAsync();
        while (await reader2.ReadAsync())
        {
            getAnswer.Parameters.AddWithValue(userInput);
            answer = reader2.GetString(0);
        }
        //Console.WriteLine(answer);
        Console.WriteLine("your guess is...");
        Thread.Sleep(1000);
        if (answer == "test")
        {
            Console.WriteLine("wrong!!!");
        }
        else
        {
            Console.WriteLine("Correct!!!");
        }
    } 
    
}
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

        Console.WriteLine(num);

        await using var getQuestions = _db.CreateCommand($"SELECT questions FROM Questions WHERE ID = {num}");
        await using var reader = await getQuestions.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            question = reader.GetString(0);
        }
        Console.WriteLine(question);

        string userInput = Console.ReadLine();

        await using var getAnswer =
            _db.CreateCommand($"SELECT answer FROM CorrectAnswers WHERE ID = {num} AND answer = {userInput}");
        await using var reader2 = await getAnswer.ExecuteReaderAsync();
        while (await reader2.ReadAsync())
        {
            answer = reader2.GetString(0);
        }
        Console.WriteLine(answer);
    } 
    
}
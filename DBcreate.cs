using Npgsql;
using System.Threading.Tasks;

using System.Threading.Tasks;

public class DBcreate
{
    NpgsqlConnection _db;

    public DBcreate(NpgsqlConnection db)
    {
        _db = db;
    }

    public DBcreate(NpgsqlDataSource db)
    {
    }

    public async Task CreateTable()
    {
        await _db.OpenAsync();

        using (var cmd = _db.CreateCommand())
        {
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Users(ID SERIAL PRIMARY KEY, username TEXT, password TEXT)";
            await cmd.ExecuteNonQueryAsync();
        }

        using (var cmd = _db.CreateCommand())
        {
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Questions (ID SERIAL PRIMARY KEY, question TEXT)";
            await cmd.ExecuteNonQueryAsync();
        }

        using (var cmd = _db.CreateCommand())
        {
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS CorrectAnswers (ID SERIAL PRIMARY KEY, answer TEXT, questionID INT REFERENCES Questions(ID))";
            await cmd.ExecuteNonQueryAsync();
        }
    }
}

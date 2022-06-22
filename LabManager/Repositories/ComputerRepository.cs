using LabManager.Models;
using LabManager.Database;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;
    
    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;
    
    public IEnumerable<Computer> GetAll()
    {
        var computers = new List<Computer>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        //var computers = new List<Computer>();
        //var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        /*
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers";
@@ -39,30 +41,36 @@ public List<Computer> GetAll()
        }
        reader.Close();
        connection.Close();
        */

        var computers = connection.Query<Computer>("SELECT * FROM Computers"); 
        return computers;
    }

    public Computer Save(Computer computer)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        /*
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);
        */

        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)", computer);

        return computer;
    }

     public Computer GetById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        /*
        var command = connection.CreateCommand();
        command.CommandText = @"
        SELECT * FROM Computers WHERE (id = $id)
        ";
        command.Parameters.AddWithValue("$id", id);
        var reader = command.ExecuteReader();
        reader.Read();
        id = reader.GetInt32(0);
        var ram = reader.GetString(1);
        var processor = reader.GetString(2);
        //var computer = new Computer(id, ram, processor);
        var computer = ReaderToComputer(reader);
        connection.Close(); 
        */

        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id = @Id", new {Id = id}); 

        return computer;
    }
    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
        UPDATE Computers SET ram = $ram, processor = $processor WHERE (id = $id)
        ";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);
        command.ExecuteNonQuery();
        connection.Close();
        return computer;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id = @Id", new {Id = id});
    }

    public bool ExistById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        /*
        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(id) FROM Computers WHERE (id = $id)";
        command.Parameters.AddWithValue("$id", id); 
        var reader = command.ExecuteReader();
        // var reader = command.ExecuteReader();
        // reader.Read();
        // var result = reader.GetBoolean(0);
        
        //bool result = command.ExecuteScalar();
        var result = Convert.ToBoolean(command.ExecuteScalar());
        connection.Close();
        */

        var result = connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Computers WHERE id = @Id", new {Id = id});

        return result;
    }

    private Computer ReaderToComputer(SqliteDataReader reader)
    {
        // var id = reader.GetInt32(0);
        // var ram = reader.GetString(1);
        // var processor = reader.GetString(2);
        // var computer = new Computer(id, ram, processor);
        var computer = new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        return computer;
    }

}
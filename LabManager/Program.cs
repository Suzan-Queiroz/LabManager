using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

//Routing
var modelName = args[0];
var modelAction = args[1];

var databaseConfig = new DatabaseConfig(); 
new DatabaseSetup(databaseConfig);

if(modelName == "Computer")
{
    var computerRepository = new ComputerRepository(databaseConfig);
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }
    }

    if(modelAction == "New")
    {
        Console.WriteLine("Computer New");
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];
        
        var computer = new Computer(id, ram, processor);
        var result = computerRepository.Save(computer);
        Console.WriteLine("{0},{1},{2}", result.Id, result.Ram, result.Processor);
    }

    if(modelAction == "Show")
    {
        Console.WriteLine("Computer Show");
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistById(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine("{0},{1},{2}", computer.Id, computer.Ram, computer.Processor);
        }
        else 
        {
            Console.WriteLine($"O computador {id} não existe!");
        }
    }

    if(modelAction == "Update")
    {
        Console.WriteLine("Computer Update");
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Update(computer);
        Console.WriteLine("{0},{1},{2}", computer.Id, computer.Ram, computer.Processor);
    }

    if(modelAction == "Delete")
    {
        Console.WriteLine("Computer Delete");
        var id = Convert.ToInt32(args[2]);
        
        computerRepository.Delete(id);
        Console.WriteLine("Computer {0}", id);
    }
}

if(modelName == "Lab")
{
    var labRepository = new LabRepository(databaseConfig);
    if(modelAction == "List")
    {
        Console.WriteLine("Lab List");
        
        //var connection = new SqliteConnection(databaseConfig.ConnectionString);
        //connection.Open();
        


        foreach (var lab in labRepository.GetAll())
        {
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");
        }
        
    }

    //var command = connection.CreateCommand();
    //command.CommandText = "SELECT * FROM Labs;";
    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = Convert.ToChar(args[5]);
        
        var lab = new Lab(id, number, name, block);
        labRepository.Save(lab);
    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = Convert.ToChar(args[5]);

        var lab = new Lab(id, number, name, block);
        labRepository.Update(lab);
    }

    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        
        if (labRepository.ExistById(id))
        {
            labRepository.Delete(id);
        }
        else
        {
            Console.WriteLine($"O Laboratório {id} não existe.");
        }
    }

    if(modelAction == "Show") 
    {
        var id = Convert.ToInt32(args[2]);

        if (labRepository.ExistById(id))
        {
            var lab = labRepository.GetById(id);
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");  
        }
        else
        {
            Console.WriteLine($"O Laboratório {id} não existe.");
        }
    }
} 
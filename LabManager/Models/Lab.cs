namespace LabManager.Models;

class Lab
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public char Block { get; set; }

    public Lab(int id, int numbers, string name, char block)
    {
        Id = id;
        Number = numbers;
        Name = name;
        Block = block;
    }  
}
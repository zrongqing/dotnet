namespace App.Entities;

public class Student : EntityBase
{

    public string Name { get; set; }

    public virtual List<Teacher> Teachers { get; set; }
}
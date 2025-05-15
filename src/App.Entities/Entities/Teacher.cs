namespace App.Entities;

public class Teacher : EntityBase
{
    public string Name { get; set; }

    public virtual List<Student> Students { get; set; }
}
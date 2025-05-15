namespace App.Entities;

public class Department : EntityBase
{
    public Department()
    {
        Courses = new HashSet<Course>();
    }

    public string Name { get; set; }
    public decimal Budget { get; set; }
    public DateTime StartDate { get; set; }
    public int? Administrator { get; set; }
    public ICollection<Course> Courses { get; set; }
}
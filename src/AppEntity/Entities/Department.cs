using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEntity;

public class Department:BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DepartmentId { get; set; }
    
    public Department()
    {
        this.Courses = new HashSet<Course>();
    }  
    public string Name { get; set; }
    public decimal Budget { get; set; }
    public DateTime StartDate { get; set; }
    public int? Administrator {get ; set; }
    public ICollection<Course> Courses { get; set; }
}
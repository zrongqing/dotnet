using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEntity;

public class Teacher: BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TeacherId { get; set; }
    
    public string Name { get; set; }
    
    public virtual List<Student> Students { get; set; } 
}
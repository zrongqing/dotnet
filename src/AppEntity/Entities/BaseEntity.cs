using System.ComponentModel.DataAnnotations.Schema;

namespace AppEntity;

public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdateTime { get; set; }
}
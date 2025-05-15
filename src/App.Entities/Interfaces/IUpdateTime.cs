using System.ComponentModel.DataAnnotations.Schema;

namespace App.Entities.Interfaces;

public interface IUpdateTime
{
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdateTime { get; set; }
}
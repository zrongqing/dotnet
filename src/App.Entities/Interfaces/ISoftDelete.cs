namespace App.Entities.Interfaces;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}
namespace App.Entities.Interfaces;

public interface IAuditableEntity
{
    public DateTime CreatedDate { get; set; }
    
    public int CreatedBy { get; set; }
    
    public DateTime ModifiedDate { get; set; }
}
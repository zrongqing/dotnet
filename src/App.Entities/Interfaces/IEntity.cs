namespace App.Entities.Interfaces;

public interface IEntity
{
    public long Id { get; set; }
    
    public string PubId { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Entities.Interfaces;

namespace App.Entities;

public class EntityBase : IEntityBase
{
    #region IEntityBase Members

    [Key][Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [MaxLength(40)]
    public string PubId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    #endregion
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Entities;

public class PlanTask : EntityBase
{
    [StringLength(255)]
    public string Name { get; set; }
    
    public long? PId { get; set; }
    
    [ForeignKey(nameof(PId))]
    public virtual PlanTask Parent { get; set; }
    
    public virtual ICollection<PlanTask> Children { get; set; } = new List<PlanTask>();
}

public class TaskConfiguration : IEntityTypeConfiguration<App.Entities.PlanTask>
{
    public void Configure(EntityTypeBuilder<PlanTask> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
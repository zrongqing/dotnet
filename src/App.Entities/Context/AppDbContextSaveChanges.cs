using App.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Entities.Context;

public partial class AppDbContextSaveChanges :AppDbContext
{
    private void PreSaveChanges()
    {
        var addEntries = ChangeTracker.Entries()
                                      .Where(e => e.State == EntityState.Added);
        foreach (var entry in addEntries)
        {
            {
                if (entry is IEntityBase entity)
                {
                    if (string.IsNullOrEmpty(entity.PubId))
                    {
                        entity.PubId = Ulid.NewUlid().ToString();
                    }
                }
            }
            {
                if (entry is IAuditableEntity entity)
                {
                    var now = DateTime.UtcNow;
                    
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = now;
                        // entity.CreatedBy = _currentUserService.UserId;
                    }
                    
                    entity.ModifiedDate = now;
                }
            }
        }
    }
    
    public override int SaveChanges()
    {
        // 在保存前执行自定义逻辑
        PreSaveChanges();
        
        var result = base.SaveChanges();
        
        // 在保存后执行自定义逻辑
        PostSaveChanges();

        return result;
    }

    private void PostSaveChanges()
    {
        
    }
}
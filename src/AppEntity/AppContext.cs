using System.Data.Entity;

namespace AppEntity;

public class AppContext : DbContext
{
    public AppContext() : base("Server=(local);Database=gitlab_dotnet;Trusted_Connection=True;")
    {
        
    }
}
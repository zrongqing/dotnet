using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;

namespace AppEntity;

public class AppContextConfiguration : DbConfiguration
{
    public AppContextConfiguration()
    {
        SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());

        // 对由 Code First 中的约定创建的数据库使用本地 DB
        SetDefaultConnectionFactory(new LocalDbConnectionFactory("mssqllocaldb"));
    }
}
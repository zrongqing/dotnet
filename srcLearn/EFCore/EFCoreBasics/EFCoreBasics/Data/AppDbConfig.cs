using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBasics.Data
{
    internal static class AppDbConfig
    {
        public static readonly string SqlServerDefault ="Server = localhost;Database = BloggingDB; Trusted_Connection=True;TrustServerCertificate=True;";
    }
}

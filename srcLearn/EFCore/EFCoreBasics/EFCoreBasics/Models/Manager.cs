using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBasics.Models
{
    /// <summary>
    /// 管理者
    /// </summary>
    internal class Manager
    {
        public int ManagerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}

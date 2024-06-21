using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBasics.Models
{
    /// <summary>
    /// 员工
    /// </summary>
    internal class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        /// <remarks> null! :直接初始化；</remarks>
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public long Salary { get; set; }
    }
}

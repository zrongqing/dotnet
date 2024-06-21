using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBasics.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        /// <summary>
        /// 价格
        /// </summary>
        /// <remarks> 具有两位精度的小数 </remarks>
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <remarks> Required 使用null,但是null不储存在数据库中</remarks>
        [Required]
        public string? Description { get; set; }
    }
}

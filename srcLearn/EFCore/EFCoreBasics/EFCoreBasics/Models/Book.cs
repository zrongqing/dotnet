using EFCoreBasics.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreBasics.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <inheritdoc>
    /// Fluent API的优先级是大于TableAttribute
    /// </inheritdoc>
    [Table("T_Books_Attribute")]
    public class Book
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PubTime { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 作者名字
        /// </summary>
        public string AuthorName { get; set; } 
    }
}

/// <summary>
/// Fluent API 配置类型
/// </summary>
/// <remarks>
/// Fluent API 来配置模型
/// </remarks>
/// <see cref="https://learn.microsoft.com/zh-cn/ef/core/modeling/"/>
class BookEntityConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("T_Books");
        builder.Property(e => e.Title).HasMaxLength(50).IsRequired();
        builder.Property(e => e.AuthorName).HasMaxLength(20).IsRequired();
    }
}

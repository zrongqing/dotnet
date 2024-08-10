using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public record User
{
    /// <summary>
    /// 实体类包含只读属性或者属性只能被类内部的代码修改
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// 实体类包含只读属性或者属性只能被类内部的代码修改
    /// </summary>
    public DateTime CreatedDateTime { get; init; } //特征一

    /// <summary>
    /// 实体类包含只读属性或者属性只能被类内部的代码修改
    /// </summary>
    public string UserName { get; private set; }

    public int Credit { get; private set; }

    /// <summary>
    /// 把不属于属性的成员变量映射为数据列
    /// </summary>
    private string? passwordHash; //特征三

    private string? remark;
    /// <summary>
    /// 从数据列中读取值的只读属性
    /// </summary>
    public string? Remark
    {
        get { return remark; }
    }

    /// <summary>
    /// 不需要映射数据列的属性
    /// </summary>
    public string? Tag { get; set; } //特征五

    /// <summary>
    /// 实体类可能包含有参构造方法
    /// </summary>
    private User()
    {
    }

    /// <summary>
    /// 实体类可能包含有参构造方法
    /// </summary>
    public User(string yhm) //特征二
    {
        this.UserName = yhm;
        this.CreatedDateTime = DateTime.Now;
        this.Credit = 10;
    }

    public void ChangeUserName(string newValue)
    {
        this.UserName = newValue;
    }

    public void ChangePassword(string newValue)
    {
        if (newValue.Length < 6)
        {
            throw new ArgumentException("密码太短");
        }

        this.passwordHash = HashHelper.Hash(newValue);
    }
}

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //把不属于属性的成员变量映射为数据列
        builder.Property("passwordHash");

        //从数据列中读取值的只读属性
        builder.Property(u => u.Remark).HasField("remark");

        //不需要映射数据列的属性
        builder.Ignore(u => u.Tag);
    }
}
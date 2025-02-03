using System.Reflection;

namespace AttributeTest;

/// <summary>
/// 任务枚举
/// </summary>
public enum MyTaskEnum
{
    None,
    One,
    Two,
    Three
}

/// <summary>
/// 接口
/// </summary>
[MyTask(MyTaskEnum.None)]
public interface IMyTask
{
}

/// <summary>
/// MyTaskAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class |
                AttributeTargets.Interface)]
public class MyTaskAttribute : Attribute
{
    public MyTaskAttribute(MyTaskEnum taskEnum, params string[] values)
    {
        TaskEnum = taskEnum;
        Values = values;
    }

    public MyTaskAttribute(Person person)
    {
    }

    public MyTaskEnum TaskEnum { get; }

    public string Name { get; set; }

    public string[] Values { get; private set; }
}

/// <summary>
/// 使用不定参数来定义一些内容
/// </summary>
[MyTask(MyTaskEnum.One, "1", "2")]
public class MyTaskOne : IMyTask
{
}

/// <summary>
/// 单属性初始化
/// </summary>
[MyTask(MyTaskEnum.Two, null, Name = "Name")]
public class MyTaskTwo : IMyTask
{
}

[MyTask(MyTaskEnum.Three)]
public class MyTaskThree : IMyTask
{
}

internal static class TestAuthorAttribute
{
    /// <summary>
    /// 获取继承了执行接口的全部类型
    /// </summary>
    private static IEnumerable<Type> GetInterfaceTypes()
    {
        // 获取当前程序集中实现了IMyInterface接口的所有类型
        var interfaceType = typeof(IMyTask);
        var implementingTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => interfaceType.IsAssignableFrom(type) && !type.IsInterface);
        return implementingTypes;
    }

    public static void Test()
    {
        var pubTypes = GetInterfaceTypes();
        foreach (var pubType in pubTypes)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(pubType); // Reflection.
            foreach (var attr in attrs)
                if (attr is MyTaskAttribute myTaskAttribute)
                    Console.WriteLine($"   {myTaskAttribute.TaskEnum}");
        }

        //PrintAuthorInfo(typeof(MyTaskOne));
        //PrintAuthorInfo(typeof(MyTaskTwo));
        //PrintAuthorInfo(typeof(MyTaskThree));
    }

    private static void PrintAuthorInfo(Type t)
    {
        Console.WriteLine($"Author information for {t}");

        // Using reflection.
        Attribute[] attrs = Attribute.GetCustomAttributes(t); // Reflection.

        // Displaying output.
        foreach (var attr in attrs)
            if (attr is MyTaskAttribute a)
                Console.WriteLine($"   {a.TaskEnum}");
    }
}
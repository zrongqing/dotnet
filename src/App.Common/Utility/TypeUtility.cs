namespace App.Common.Utility;

public static class TypeUtility
{
    public static bool IsImplementingInterface(this Type type, Type interfaceType)
    {
        if (!interfaceType.IsInterface)
        {
            throw new ArgumentException("The provided type is not an interface.");
        }

        // 检查是否为泛型接口
        if (interfaceType.IsGenericType)
        {
            // 如果是泛型接口，则检查所有实现的接口中是否有匹配的泛型接口定义
            return type.GetInterfaces().Any(i => 
                                                i.IsGenericType && 
                                                i.GetGenericTypeDefinition() == interfaceType);
        }
        else
        {
            // 对于非泛型接口，直接检查该类型是否实现了此接口
            return interfaceType.IsAssignableFrom(type);
        }
        
        return type.GetInterfaces().Any(i => 
                                            i.IsGenericType && 
                                            i.GetGenericTypeDefinition() == interfaceType);
    }
}
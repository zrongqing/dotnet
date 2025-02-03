using System.Reflection;

namespace ZRQ.Utils;

public static class RelfectUtils
{
    public static List<Attribute> GetAttributes(object ob)
    {
        PropertyInfo[] properties =
            ob.GetType().GetProperties();

        List<Attribute> attributes = new();
        foreach (var property in properties)
        {
            Attribute[] propertyAttributes = (Attribute[])property.GetCustomAttributes(true);
            attributes.AddRange(propertyAttributes);
        }

        return attributes;
    }
}
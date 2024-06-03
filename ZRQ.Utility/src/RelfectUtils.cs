using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZRQ.Utils
{
    public static class RelfectUtils
    {
        public static List<Attribute> GetAttributes(object ob)
        {
            PropertyInfo[] properties =
                ob.GetType().GetProperties();

            List<Attribute> attributes = new();
            foreach (PropertyInfo property in properties)
            {
                Attribute[] propertyAttributes = (Attribute[])property.GetCustomAttributes(true);
                attributes.AddRange(propertyAttributes);
            }

            return attributes;
        }
    }
}

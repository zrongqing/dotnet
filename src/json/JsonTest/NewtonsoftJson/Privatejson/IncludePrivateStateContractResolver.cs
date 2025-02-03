﻿using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JsonTest.NewtonsoftJson.Privatejson;

public class IncludePrivateStateContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);

        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            if (property != null)
            {
                prop.Writable = property.HasSetter();
            }
            else
            {
                var field = member as FieldInfo;
                if (field != null) prop.Writable = true;
            }
        }

        if (!prop.Readable)
        {
            var field = member as FieldInfo;
            if (field != null) prop.Readable = true;
        }

        return prop;
    }

    protected override List<MemberInfo> GetSerializableMembers(Type objectType)
    {
        const BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        var properties = objectType.GetProperties(BindingFlags); //.Where(p => p.HasSetter() && p.HasGetter());
        var fields = objectType.GetFields(BindingFlags);

        var allMembers = properties.Cast<MemberInfo>().Union(fields);
        return allMembers.ToList();
    }
}
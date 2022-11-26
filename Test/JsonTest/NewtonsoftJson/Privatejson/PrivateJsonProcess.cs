using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest.NewtonsoftJson.Privatejson
{
    internal class PrivateJsonProcess
    {
        public static void Process()
        {
            Console.WriteLine("一般来说, Json序列化 是不能够序列化对象的私有属性的");

            Console.WriteLine("序列化私有对象方式一: 接管 JsonSerializerSettings ");
            PrivateData privateOne = new($"序列化私有对象方式一");
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new IncludePrivateStateContractResolver();
            var jsonCopy = JsonConvert.SerializeObject(privateOne, serializerSettings);
            var clonedObject = JsonConvert.DeserializeObject<PrivateData>(jsonCopy, serializerSettings);
            clonedObject = null;
            jsonCopy = null;

            Console.WriteLine("序列化私有对象方式二: 添加 DataContract ");
            PrivateData privateTwo = new($"序列化私有对象方式二", "测试1");
            privateTwo.NoDataMemberPublic = "123";
            jsonCopy = JsonConvert.SerializeObject(privateTwo);
            clonedObject = JsonConvert.DeserializeObject<PrivateData>(jsonCopy);
        }
    }
}

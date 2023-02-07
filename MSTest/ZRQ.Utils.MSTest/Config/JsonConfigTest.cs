using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZRQ.Utils.Config;

namespace ZRQ.Utils.MSTest.Config
{
    public class JsonData : JsonConfig<JsonData>
    {

    }

    [TestClass]
    public class JsonConfigTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            JsonData jsonData = new();
            jsonData.Save();
        }
    }
}

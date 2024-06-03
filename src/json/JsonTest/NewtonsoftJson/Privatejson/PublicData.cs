using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest.NewtonsoftJson.Privatejson
{
    public class PublicData
    {
        public PublicData(string publicValue)
        {
            PublicProperty = publicValue;
        }

        public string PublicProperty { get; set; } = "this is public set property";
    }
}

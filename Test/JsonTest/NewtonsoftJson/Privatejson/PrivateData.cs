using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest.NewtonsoftJson.Privatejson
{
    [DataContract]
    internal class PrivateData
    {
        public PrivateData(string privateValue, string noDataMemberPrivate = "")
        {
            PrivateProperty = privateValue;
            NoDataMemberPrivate = noDataMemberPrivate;
        }

        public string NoDataMemberPrivate { get; private set; } = string.Empty;

        public string NoDataMemberPublic { get; set; } = string.Empty;

        [DataMember]
        public string PrivateProperty { get; private set; } = "this is private set property";
    }
}

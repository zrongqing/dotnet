using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest
{
    internal class NewtonsoftJsonTest
    {
        public static void Process()
        {
            string str = @"{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }";
            string strAA = "{\"ok\":true,\"msg\":\"\",\"data\":[\"HWG_BUPIN\",\"HWT150655\"]}";
            dynamic? results = JsonConvert.DeserializeObject<dynamic>(strAA);
            if (results == null) return;

            var isOk = results.ok;

            var name = results.Name;
            var value = results.aaaaa;
        }
    }
}

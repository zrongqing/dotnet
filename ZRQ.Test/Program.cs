// See https://aka.ms/new-console-template for more information
using System.Data.SqlTypes;
using System.Text.Json;
using System.Text.RegularExpressions;
using ZRQ.Test;

Console.WriteLine("Hello, World!");
//JsonData jsonData = new();
//try
//{
//    string jsonString = JsonSerializer.Serialize(jsonData);
//}
//catch (Exception e)
//{
//    Console.WriteLine(e);
//    throw;
//}

JsonData.Inst.Save();

Console.ReadKey();
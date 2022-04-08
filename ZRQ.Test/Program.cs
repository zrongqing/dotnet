// See https://aka.ms/new-console-template for more information
using System.Data.SqlTypes;
using System.Text.RegularExpressions;


string reportNumber = "001";

var matches = Regex.Matches(reportNumber, "[0-9]+");
var numbers = new List<string>();
foreach (Match match in matches)
{
    numbers.Add(match.Value);
}

int lastIndex = reportNumber.LastIndexOf(numbers[numbers.Count - 1]);
if (lastIndex == 0)
{
}
else
{
    String Prefix = reportNumber.Substring(0, lastIndex);
}



Console.WriteLine("Hello, World!");


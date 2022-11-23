// See https://aka.ms/new-console-template for more information
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using ZRQ.Test;

Console.WriteLine("Hello, World!");

People people = new() { Name = "ceshi" };
Console.WriteLine(people.Equals(default(People)));

People defaultPeople = new();
Console.WriteLine(defaultPeople.Equals(default(People)));

PeopleTwo peopleTwo = new();

Object ob = new();

var aa = peopleTwo as Interface1;


if (people is Interface1)
{
    var b = (PeopleTwo)peopleTwo;
    var asd = peopleTwo as Interface1;

    //var bvb = people as PeopleTwo;

}

Console.ReadKey();

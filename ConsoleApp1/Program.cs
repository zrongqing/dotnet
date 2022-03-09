// See https://aka.ms/new-console-template for more information

using ConsoleApp1;

List<ClassA> classAs = new List<ClassA>();

ClassA classA0 = new ClassA(0);
ClassA classA1 = new ClassA(1);

classAs.Add(classA0);
classAs.Add(classA1);

if (classAs.Contains(classA0))
{

}

Console.WriteLine("Hello, World!");

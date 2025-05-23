﻿namespace TypeTest;

internal class Program
{
    public static List<Type> ElementTypes = new()
    {
        typeof(HostObjAttributes),
        typeof(InsertableObject)
    };

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        PipeType pipeType = new();

        TTT(pipeType);
    }

    private static void TTT(ElementType pipeType)
    {
        var type = pipeType.GetType();

        var parentType = ElementTypes[0];
        var isSub = type.IsSubclassOf(parentType);

        var isMepCurveType = typeof(MEPCurveType).IsSubclassOf(type);
    }
}
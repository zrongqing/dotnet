namespace ZRQ.Utils;

public static class LineCounter
{
    #region How to using

    // Use the first argument as the directory to search, or default to the current directory
    private static void HowUsing(string[] args)
    {
        var totalLineCount = 0;
        string directory;
        if (args.Length > 0)
            directory = args[0];
        else
            directory = Directory.GetCurrentDirectory();
        totalLineCount = DirectoryCountLines(directory);
        Console.WriteLine(totalLineCount);
    }

    #endregion How to using

    private static int CountLines(string file)
    {
        var lineCount = 0;
        string? line;
        FileStream stream = new(file, FileMode.Open);
        StreamReader reader = new(stream);
        line = reader.ReadLine();
        while (line is object)
        {
            if (line.Trim() != "") lineCount++;
            line = reader.ReadLine();
        }

        reader.Dispose(); // Automatically closes the stream
        return lineCount;
    }

    private static int DirectoryCountLines()
    {
        return DirectoryCountLines(
            Directory.GetCurrentDirectory());
    }

    private static int DirectoryCountLines(string directory)
    {
        return DirectoryCountLines(directory, "*.cs");
    }

    private static int DirectoryCountLines(
        string directory, string extension)
    {
        var lineCount = 0;
        foreach (var file in
                 Directory.GetFiles(directory, extension))
            lineCount += CountLines(file);

        foreach (var subdirectory in
                 Directory.GetDirectories(directory))
            lineCount += DirectoryCountLines(subdirectory);

        return lineCount;
    }
}
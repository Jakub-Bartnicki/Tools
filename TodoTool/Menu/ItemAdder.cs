namespace TodoTool.Menu;

internal static class ItemAdder
{
    internal static string AddItem()
    {
        Console.WriteLine("Quest:");

        ConsoleKeyInfo keyInfo;
        var buffer = new List<char>();
        do
        {
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                return "";
            }

            if (keyInfo.Key != ConsoleKey.Enter && 
                keyInfo.Key != ConsoleKey.Backspace)
            {
                buffer.Add(keyInfo.KeyChar);
            }

            if (keyInfo.Key == ConsoleKey.Backspace && 
                buffer.Count > 0)
            {
                buffer.RemoveAt(buffer.Count - 1);
                RemoveCharInConsole(buffer);
            }
        }
        while (keyInfo.Key != ConsoleKey.Enter);

        var textLine = new string(buffer.ToArray());
        return textLine;
    }

    private static void RemoveCharInConsole(List<char> buffer)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(buffer.ToArray());
    }
}

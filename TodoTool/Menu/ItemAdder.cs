namespace TodoTool.Menu;

internal static class ItemAdder
{
    internal static string AddItem()
    {
        Console.WriteLine("\bQuest:");

        ConsoleKeyInfo keyInfo;
        var text = new List<char>();
        do
        {
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                return "";
            }

            if (keyInfo.Key != ConsoleKey.Enter)
            {
                text.Add(keyInfo.KeyChar);
            }
        }
        while (keyInfo.Key != ConsoleKey.Enter);

        var textLine = new string(text.ToArray());
        return textLine;
    }
}

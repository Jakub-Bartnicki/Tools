using TodoTool.Utils;

namespace TodoTool.Menu;

internal static class ItemRewriter
{
    private static string caret = Consts.Caret;
    private static List<char> buffer = new();

    internal static string UpdateItem(string text)
    {
        Console.Clear();

        buffer = text.ToCharArray().ToList();
        Console.Write(caret);
        Console.WriteLine(buffer.ToArray());
        Console.SetCursorPosition(Console.CursorLeft + buffer.Count, Console.CursorTop - 1);

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        while (keyInfo.Key != ConsoleKey.Enter)
        {
            HandleKeyPress(keyInfo);
            keyInfo = Console.ReadKey(true);
        }

        return new string(buffer.ToArray());
    }

    private static void RewriteLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(caret);
        Console.Write(buffer.ToArray());
    }

    private static void HandleKeyPress(ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.LeftArrow:
                HandleLeftArrowKey();
                break;
            case ConsoleKey.RightArrow:
                HandleRightArrowKey();
                break;
            case ConsoleKey.Home:
                HandleHomeKey();
                break;
            case ConsoleKey.End:
                HandleEndKey();
                break;
            case ConsoleKey.Backspace:
                HandleBackspaceKey();
                break;
            case ConsoleKey.Delete:
                HandleDeleteKey();
                break;
            default:
                HandleCharKey(keyInfo);
                break;
        }
    }

    private static void HandleLeftArrowKey()
    {
        Console.SetCursorPosition(Math.Max(Console.CursorLeft - 1, caret.Length), Console.CursorTop);
    }

    private static void HandleRightArrowKey()
    {
        Console.SetCursorPosition(Math.Min(Console.CursorLeft + 1, caret.Length + buffer.Count), Console.CursorTop);
    }

    private static void HandleHomeKey()
    {
        Console.SetCursorPosition(caret.Length, Console.CursorTop);
    }

    private static void HandleEndKey()
    {
        Console.SetCursorPosition(caret.Length + buffer.Count, Console.CursorTop);
    }

    private static void HandleBackspaceKey()
    {
        if (Console.CursorLeft <= caret.Length)
        {
            return;
        }
        var cursorColumnAfterBackspace = Math.Max(Console.CursorLeft - 1, caret.Length);
        buffer.RemoveAt(Console.CursorLeft - caret.Length - 1);
        RewriteLine();
        Console.SetCursorPosition(cursorColumnAfterBackspace, Console.CursorTop);
    }

    private static void HandleDeleteKey()
    {
        if (Console.CursorLeft >= caret.Length + buffer.Count)
        {
            return;
        }
        var cursorColumnAfterDelete = Console.CursorLeft;
        buffer.RemoveAt(Console.CursorLeft - caret.Length);
        RewriteLine();
        Console.SetCursorPosition(cursorColumnAfterDelete, Console.CursorTop);
    }

    private static void HandleCharKey(ConsoleKeyInfo keyInfo)
    {
        var character = keyInfo.KeyChar;
        if (character < 32)
            return;
        var cursorAfterNewChar = Console.CursorLeft + 1;
        if (cursorAfterNewChar > Console.WindowWidth || caret.Length + buffer.Count >= Console.WindowWidth - 1)
        {
            return;
        }
        buffer.Insert(Console.CursorLeft - caret.Length, character);
        RewriteLine();
        Console.SetCursorPosition(cursorAfterNewChar, Console.CursorTop);
    }
}

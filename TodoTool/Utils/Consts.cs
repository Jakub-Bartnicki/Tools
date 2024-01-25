using TodoTool.Enums;

namespace TodoTool.Utils;

internal static class Consts
{
    internal static string Caret = "> ";
    internal static string Empty = "  ";
    internal static string Done = "+ ";
    internal static string NotDone = "- ";
    internal static string Del = "del";

    internal static Dictionary<ConsoleKey, MenuOption> MenuOptionKeyDict = new() {
        {ConsoleKey.Enter, MenuOption.Enter},
        {ConsoleKey.A, MenuOption.KeyA},
        {ConsoleKey.U, MenuOption.KeyE},
        {ConsoleKey.D, MenuOption.KeyDelete}
        }; 
}

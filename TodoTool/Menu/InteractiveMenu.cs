using TodoTool.Enums;
using TodoTool.Todo;
using TodoTool.Utils;

namespace TodoTool.Menu;

internal sealed class InteractiveMenu
{
    private static List<Option> options = new();
    private static int index = 0;
    private readonly TodoList _todoList;

    public InteractiveMenu()
    {
        _todoList = new TodoList();
    }

    internal void Start()
    {
        var textLines = _todoList;
        PrepareOptions();

        WriteMenu();

        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    if (index + 1 < options.Count) index++;
                    break;
                case ConsoleKey.UpArrow:
                    if (index - 1 >= 0) index--;
                    break;
                case ConsoleKey.Enter:
                    options[index].Selected.Invoke(MenuOption.Enter);
                    _todoList.ToggleItem(index);
                    break;
                case ConsoleKey.U:
                    options[index].Selected.Invoke(MenuOption.KeyU);
                    _todoList.UpdateItem(options[index].Text, index);
                    break;
                case ConsoleKey.R:
                    options.RemoveAt(index);
                    _todoList.RemoveItem(index);
                    break;
                case ConsoleKey.A:
                    options.Add(new Option(""));
                    var option = options[options.Count - 1];
                    option.Selected.Invoke(MenuOption.KeyA);
                    option.Text.Trim();
                    if (option.Text == "" && option.Text == "\r")
                    {
                        options.RemoveAt(options.Count - 1);
                    }
                    _todoList.AddItem(option.Text);
                    break;
                default:
                    break;
            }

            WriteMenu();
        }
        while (keyInfo.Key != ConsoleKey.Escape);
    }


    void WriteMenu()
    {
        Console.Clear();

        for (int i = 0; i <  options.Count; i++)
        {
            if (options[i] == options[index])
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(Consts.Caret);
                Console.WriteLine(options[i].Text);
            }
            else
            {
                var color = _todoList.IsTodoItemDone(i) ? ConsoleColor.Green : ConsoleColor.Yellow;
                Console.ForegroundColor = color;
                Console.Write(Consts.Empty);
                Console.WriteLine(options[i].Text);
            }
        }

        Console.ResetColor();
    }

    private void PrepareOptions()
    {
        foreach (var todoItem in _todoList.TodoItems)
        {
            var option = new Option(todoItem.Text);
            options.Add(option);
        }
    }
}

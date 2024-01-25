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
        PrepareOptions();

        WriteMenu();

        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    MoveUp();
                    break;
                case ConsoleKey.UpArrow:
                    MoveDown();
                    break;
                case ConsoleKey.Enter:
                    ToggleItem();
                    break;
                case ConsoleKey.A:
                    AddItem();
                    break;
                case ConsoleKey.E:
                    EditItem();
                    break;
                case ConsoleKey.Delete:
                    RemoveItem();
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
                var color = _todoList.IsTodoItemDone(index) ? ConsoleColor.Green : ConsoleColor.Yellow;
                Console.ForegroundColor = color;
                Console.Write(Consts.Caret);
                Console.WriteLine(options[i].Text);
            }
            else
            {
                var color = _todoList.IsTodoItemDone(i) ? ConsoleColor.DarkGreen : ConsoleColor.DarkYellow;
                Console.ForegroundColor = color;
                Console.Write(Consts.Empty);
                Console.WriteLine(options[i].Text);
            }
        }

        Console.ResetColor();
    }

    private void MoveUp()
    {
        if (index + 1 < options.Count) index++;
    }

    private void MoveDown()
    {
        if (index - 1 >= 0) index--;
    }

    private void ToggleItem()
    {
        options[index].Selected.Invoke(MenuOption.Enter);
        _todoList.ToggleItem(index);
    }

    private void EditItem()
    {
        options[index].Selected.Invoke(MenuOption.KeyE);
        _todoList.EditItem(options[index].Text, index);
    }

    private void RemoveItem()
    {
        options.RemoveAt(index);
        _todoList.RemoveItem(index);

        if (index == options.Count) index--;
    }

    private void AddItem()
    {
        options.Add(new Option(""));
        var option = options[options.Count - 1];
        option.Selected.Invoke(MenuOption.KeyA);
        option.Text.Trim();
        if (option.Text == "" && option.Text == "\r")
        {
            options.RemoveAt(options.Count - 1);
        }
        _todoList.AddItem(option.Text);
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

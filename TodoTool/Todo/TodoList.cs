using TodoTool.Handlers;
using TodoTool.Utils;

namespace TodoTool.Todo;

internal sealed class TodoList
{
    private readonly FileHandler _fileHandler;
    internal List<TodoItem> TodoItems { get; private set; }

    internal TodoList()
    {
        _fileHandler = new FileHandler();
        TodoItems = new List<TodoItem>();

        var textLines = _fileHandler.ReadFile();

        foreach (var line in textLines)
        {
            var todoItem = new TodoItem(line);
            TodoItems.Add(todoItem);
        }
    }

    internal bool IsTodoItemDone(int index)
    {
        return TodoItems[index].IsDone;
    }

    internal void ToggleItem(int index)
    {
        TodoItems[index].ToggleIsDone();
        Save();
    }

    internal void AddItem(string textLine)
    {
        textLine = Consts.NotDone + textLine;
        var todoItem = new TodoItem(textLine);
        TodoItems.Add(todoItem);
        Save();
    }

    internal void EditItem(string textLine, int index)
    {
        TodoItems[index].Edit(textLine);
        Save();
    }

    internal void RemoveItem(int index)
    {
        TodoItems.RemoveAt(index);
        Save();
    }

    private void Save()
    {
        var text = TodoItemsToStringList();
        _fileHandler.UpdateFile(text);
    }

    private List<string> TodoItemsToStringList()
    {
        var list = new List<string>();

        foreach (var item in TodoItems)
        {
            var text = item.Status + item.Text;
            list.Add(text);
        }

        return list;
    }
}

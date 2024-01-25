using TodoTool.Utils;

namespace TodoTool.Todo;

internal sealed class TodoItem
{
    internal string Text { get; set; }
    private bool _isDone;
    internal bool IsDone { get { return _isDone; } private set 
        {
            _isDone = value;
            Status = value ? Consts.Done : Consts.NotDone;
        }
    }
    internal string Status { get; set; }

    public TodoItem(string text)
    {
        Text = text.Substring(2);
        IsDone = HasBeenDone(text);
    }

    internal void Update(string textLine)
    {
        Text = textLine;
    }

    internal void ToggleIsDone()
    {
        IsDone = !IsDone;
    }

    private bool HasBeenDone(string text)
    {
        var status = text.Substring(0, 2);

        if (status == Consts.Done) return true;
        else if (status == Consts.NotDone) return false;

        throw new ArgumentException("Wrong todo item", text);
    }
}

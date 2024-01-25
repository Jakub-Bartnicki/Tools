using Cocona;
using TodoTool.Menu;
using TodoTool.Todo;
using TodoTool.Utils;

CoconaApp.Run((
    [Option('d', Description = "Get Todo List for exact day, example: 1999/12/31")] string? date,
    [Option('y', Description = "Get Todo List for yesterday")] bool ? yesterday,
    [Option('t', Description = "Get Todo List for tomorrow")] bool? tomorrow 
    ) =>
{
    var todoListDate = DateConverter.GetDateForTodoList(date, yesterday, tomorrow);
    var todoList = new TodoList(todoListDate);
    var menu = new InteractiveMenu(todoList);

    menu.Start();
});
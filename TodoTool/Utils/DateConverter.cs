namespace TodoTool.Utils;

internal static class DateConverter
{
    internal static DateOnly GetDateForTodoList(string? date, bool? yesterday, bool? tomorrow)
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        DateOnly todoListDate = dateNow;

        if (date != null) return DateOnly.Parse(date);
        if (yesterday != null && (bool)yesterday) return todoListDate.AddDays(-1);
        if (tomorrow != null && (bool)tomorrow) return todoListDate.AddDays(1);

        return todoListDate;
    }
}

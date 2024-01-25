namespace TodoTool.Handlers;

internal sealed class FileHandler
{
    private string DirPath { get; set; }
    private string FilePath { get; set; }

    public FileHandler()
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        DirPath = $"./{dateNow.Month}-{dateNow.Year}";

        if (!Directory.Exists(DirPath))
            Directory.CreateDirectory(DirPath);

        FilePath = $"{DirPath}/{dateNow.Day}-{dateNow.Month}-{dateNow.Year}.txt";
        if (!File.Exists(FilePath))
        {
            File.CreateText(FilePath).Close();
        }
    }

    internal List<string> ReadFile()
    {
        try
        {
            var lines = new List<string>();
            var fileData = File.ReadAllLines(FilePath);

            foreach (var line in fileData)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(line);
                lines.Add(line);
            }
            Console.ResetColor();
            return lines;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
            Console.ResetColor();
            return null;
        }
    }

    internal void UpdateFile(List<string> text)
    {
        File.WriteAllLines(FilePath, text);
    }
}

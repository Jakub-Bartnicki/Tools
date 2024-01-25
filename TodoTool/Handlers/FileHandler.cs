namespace TodoTool.Handlers;

internal sealed class FileHandler
{
    private string DirPath { get; set; }
    private string FilePath { get; set; }

    internal FileHandler(DateOnly date)
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now);

        bool isHistoricalData = dateNow > date.AddDays(1) || dateNow < date.AddDays(-1);

        FindFile(date, isHistoricalData);
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
            Console.WriteLine("Unable to handle non created historical/future TodoLists\n\n", ex);
            Console.ResetColor();
            Environment.Exit(1);
            return null;
        }
    }

    private void FindFile(DateOnly date, bool isHistoricalData)
    {
        try
        {
            var parentDirPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments), "TodoLists");
        
            if (!Directory.Exists(parentDirPath))
                Directory.CreateDirectory(parentDirPath);
            DirPath = Path.Combine(parentDirPath, $"./{date.Month}-{date.Year}");

            if (!Directory.Exists(DirPath) && !isHistoricalData) { 
                Directory.CreateDirectory(DirPath);
            }

            FilePath = $"{DirPath}/{date.Day}-{date.Month}-{date.Year}.txt";

            if (!File.Exists(FilePath) && !isHistoricalData)
            {
                File.CreateText(FilePath).Close();
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unable to handle non created historical/future TodoLists\n\n", ex);
            Environment.Exit(1);
        }
    }

    internal void UpdateFile(List<string> text)
    {
        File.WriteAllLines(FilePath, text);
    }
}

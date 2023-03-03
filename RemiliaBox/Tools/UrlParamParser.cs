using System.Web;

namespace RemiliaBox.Tools;

public class UrlParamParser : IBoxCommand
{
    public bool Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Not enough arguments.");
            Console.WriteLine("usage: remiliabox urlparse <URL>");
            return false;
        }
        
        Console.WriteLine($"Parse target: {args[1]}\n");

        var uri = new Uri(args[1]);
        var queries = HttpUtility.ParseQueryString(uri.Query);

        foreach (var query in queries.AllKeys)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{query}: ");
            Console.ForegroundColor = defaultColor;
            Console.Write($"{queries[query]}\n");
        }

        return true;
    }
}
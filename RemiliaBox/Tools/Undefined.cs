namespace RemiliaBox.Tools;

public class Undefined : IBoxCommand
{
    public bool Execute(string[] args)
    {
        Console.WriteLine("[remiliabox] undefined command: " + args[0]);
        return false;
    }
}
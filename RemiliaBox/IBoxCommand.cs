namespace RemiliaBox;

public interface IBoxCommand
{
    public bool Execute(string[] args);
}
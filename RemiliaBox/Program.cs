using RemiliaBox;
using RemiliaBox.Tools;

namespace RemiliaBox;

class Program
{
    public static void Main(string[] args)
    {
        args = Environment.GetCommandLineArgs();

        args[0] = Path.GetFileNameWithoutExtension(args[0]);
        var cmdArgsPosition = args[0] == "remiliabox" ? 1 : 0;
        IBoxCommand? executeCommand = null;

        // remiliabox から実行されており、引数がない場合
        if (cmdArgsPosition == 1 && args.Length <= 1)
        {
            Help();
            return;
        }
        
        switch (args[cmdArgsPosition])
        {
            case "base64":
                executeCommand = new Base64();
                break;
            
            default:
                executeCommand = new Undefined();
                break;
        }

        if (executeCommand == null) return;
        executeCommand.Execute(cmdArgsPosition == 0 ? args : args.Skip(cmdArgsPosition).ToArray());
    }

    private static void Help()
    {
        
    }
}


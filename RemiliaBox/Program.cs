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
        IBoxCommand executeCommand;

        // remiliabox から実行されており、引数がない場合
        if (cmdArgsPosition == 1 && args.Length <= 1)
        {
            Help();
            return;
        }

        executeCommand = args[cmdArgsPosition] switch
        {
            "base64" => new Base64(),
            "urlparse" => new UrlParamParser(),
            _ => new Undefined()
        };
        
        executeCommand.Execute(cmdArgsPosition == 0 ? args : args.Skip(cmdArgsPosition).ToArray());
    }

    private static void Help()
    {
        
    }
}


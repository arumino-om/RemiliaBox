using System.Text;

namespace RemiliaBox.Tools;

public class Base64 : IBoxCommand 
{
    public bool Execute(string[] args)
    {
        if (args.Length < 3)
        {
            Help();
            return true;
        }

        // 引数解析
        string? command = null;
        string? data = null;
        string? out_path = null;
        bool data_consider_filepath = false;

        string? previousArg = null;
        foreach (var arg in args.Skip(1))
        {
            switch (arg)
            {
                case "decode":
                case "d": 
                case "encode":
                case "e":
                    if (previousArg is "--out" or "-o") goto default;
                    command = arg;
                    break;
                                
                case "--file":
                case "-f":
                    if (previousArg is "--out" or "-o") goto default;
                    data_consider_filepath = true;
                    break;
                
                case "--out":
                    // 処理は default で行う
                    break;
                
                default:
                    if (previousArg is "--out" or "-o") out_path = arg;
                    else data = arg;
                    break;
            }
            previousArg = arg;
        }

        if (data == null)
        {
            Console.WriteLine("[base64] you must set <data>");
            return false;
        }
        
        //実行
        switch (command)
        {
            case "encode":
            case "e":
                if (data_consider_filepath && !File.Exists(data))
                {
                    Console.WriteLine("[base64] file isn't found.");
                    return false;
                }
                
                var encodedData = Convert.ToBase64String(data_consider_filepath ? File.ReadAllBytes(data) : Encoding.Default.GetBytes(data));
                
                if (out_path != null) File.WriteAllText(out_path, encodedData);
                else Console.WriteLine(encodedData);
                break;
                
            case "decode":
            case "d":
                if (data_consider_filepath && !File.Exists(data))
                {
                    Console.WriteLine("[base64] file isn't found.");
                    return false;
                }
                
                var decodedData = Convert.FromBase64String(data_consider_filepath ? File.ReadAllText(data) : data);

                if (out_path != null) File.WriteAllBytes(out_path, decodedData);
                else Console.WriteLine(Encoding.Default.GetString(decodedData));
                break;
        }

        return true;
    }

    public void Help()
    {
        Console.WriteLine("base64 - encode and decode base64");
        Console.WriteLine("usage: base64 <command> [--out <filepath>] [--file] <data>\n");
        Console.WriteLine("avaliable command:");
        Console.WriteLine("  encode, e - Encode data to base64");
        Console.WriteLine("  decode, d - Decode base64 encoded data\n");
        Console.WriteLine("avaliable options:");
        Console.WriteLine("  --out <filepath>   - write encoded or decoded data to <filepath>");
        Console.WriteLine("  --file, -f         - <data> will considered a file path.");
    }
}
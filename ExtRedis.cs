using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

public class ExtRedis
{

    [DllExport("RVExtensionVersion", CallingConvention = CallingConvention.Winapi)]
    public static void RvExtensionVersion(StringBuilder output, int outputSize)
    {
        output.Append("Test-Extension v1.0");
    }

    [DllExport("RVExtension", CallingConvention = CallingConvention.Winapi)]
    public static int RvExtension(StringBuilder output, int outputSize,
        [MarshalAs(UnmanagedType.LPStr)] string function)
    {
        if (function == "Connect")
        {
            if(RedisController.RedisConnect())
            {
                output.Append("Connected");
                return 0;
            } else
            {
                output.Append("ERROR: Couldn't connect to the database");
                return 1;
            }
        }

        return 0;
    }

    [DllExport("RVExtensionArgs", CallingConvention = CallingConvention.Winapi)]
    public static int RvExtensionArgs(StringBuilder output, int outputSize,
        [MarshalAs(UnmanagedType.LPStr)] string function,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] args, int argCount)
    {
        if(!RedisController.Connected)
        {
            output.Append("No Connection to the database");
            return 0;
        }
        //output.Append(argCount + " " + args[0] + " ");
        switch (function)
        {      
            case "Set":
                RedisController.RedisSet(args[0].Trim('"'), args[1].Trim('"'));
                break;
            case "Get":
                output.Append(RedisController.RedisGet(args[0].Trim('"')));
                break;
            case "HMSet":
                string key = args[0]; //Get first param which should be the key
                args = args.Skip(1).Take(args.Count() - 1).ToArray(); //Skip the key and leave the pairs left
                IEnumerable<KeyValuePair<string, string>> map = new KeyValuePair<string,string>[0]; //Make the map with 0 because we concat it later
                bool error = false;
                args.ToList().ForEach(x =>
                {
                    string[] data = SQFUtil.ParamParse(x.Substring(1, x.Length - 1).Substring(0, x.Length - 2)).ToArray(); //Trim the first and last index of the string and give to the help function
                    if (data.Length == 2)
                    {
                        map = map.Concat(new[] { new KeyValuePair<string, string>(data[0], data[1]) });
                    } else
                    {
                        output.Append("ERROR: Key values did not have the amount of 2, got " + data.Length + " instead");
                        error = true;
                        return;
                    }
                });
                if (error) { return 1; }
                //RedisController.RedisHMSet(args[0].Trim('"'), )
                break;
            default:
                output.Append("Error, That is not a function");
                return 1;
        }
        return 0;
    }

    public static void Main()
    {
        RedisController.RedisConnect();
    }
}

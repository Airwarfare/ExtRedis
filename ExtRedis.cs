using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

public class ExtRedis
{
    public const string Version = "V0.7.1";

    [DllExport("RVExtensionVersion", CallingConvention = CallingConvention.Winapi)]
    public static void RvExtensionVersion(StringBuilder output, int outputSize)
    {
        output.Append(Version);
    }

    [DllExport("RVExtension", CallingConvention = CallingConvention.Winapi)]
    public static int RvExtension(StringBuilder output, int outputSize,
        [MarshalAs(UnmanagedType.LPStr)] string function)
    {
        switch (function)
        {
            case "Connect":
                bool Connection = RedisController.RedisConnect();
                if (Connection)
                {
                    output.Append("Connected");
                    return 0;
                }
                else
                {
                    output.Append(RedisController.ErrorMessage);
                    return 1;
                }
            case "Scan":
                if (RedisController.Connected)
                    output.Append(SQFUtil.SQFConvert(RedisController.RedisScan()));
                return 0;
            case "Version":
                output.Append(Version);
                break;
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
            if (function == "Connect")
            {
                if (argCount == 2)
                {
                    bool connected = RedisController.RedisConnect(args[0].Trim('"'), int.Parse(args[1]));
                    if(!connected) { output.Append(RedisController.ErrorMessage); return 1; }
                    output.Append("Connected");
                    return 0;
                } else if (argCount == 3)
                {
                    bool connected = RedisController.RedisConnect(args[0].Trim('"'), int.Parse(args[1]), args[2].Trim('"'));
                    if (!connected) { output.Append(RedisController.ErrorMessage); return 1; }
                    output.Append("Connected");
                    return 0;
                }
            }
            else
            {
                output.Append("No Connection to the database");
                return 1;
            }
        }
        args = args.Select(x => x.Trim('"')).ToArray();
        

        //This is slower than the previous switch statement I used, but its much more maintainable, will be keeping this for now
        //In the future if I find something that is just as maintainable but faster I will rewrite this section to implement that
        Type[] types = new Type[]
        {
            typeof(Strings),
            typeof(Lists),
            typeof(Sets),
            typeof(SortedSets),
            typeof(Hashes),
            typeof(Hyperlog),
            typeof(Management)
        };

        foreach (var item in types)
        {
            MethodInfo method = item.GetMethod(function);
            if (method != null)
            {
                output.Append(method.Invoke(item, new object[] { args }));
                break;
            }
        }

        return 0;
    }
}

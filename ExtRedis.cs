using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

public class ExtRedis
{

    [DllExport("RVExtensionVersion", CallingConvention = CallingConvention.Winapi)]
    public static void RvExtensionVersion(StringBuilder output, int outputSize)
    {
        output.Append("Test-Extension v1.0");
    }

    [DllExport("RVExtension", CallingConvention = CallingConvention.Winapi)]
    public static void RvExtension(StringBuilder output, int outputSize,
        [MarshalAs(UnmanagedType.LPStr)] string function)
    {
        if (function == "Connect")
        {
            RedisController.RedisConnect();
        }
    }

    [DllExport("RVExtensionArgs", CallingConvention = CallingConvention.Winapi)]
    public static int RvExtensionArgs(StringBuilder output, int outputSize,
        [MarshalAs(UnmanagedType.LPStr)] string function,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] args, int argCount)
    {        
        if (function == "Set")
        {
            RedisController.RedisSet(args[0].Trim('"'));
        } else if (function == "Get")
        {
            output.Append(RedisController.RedisGet(args[0].Trim('"')));
        }
        return 0;
    }

    public static void Main()
    {
        RedisController.RedisConnect();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

public class ExtRedis
{
    public const string Version = "V0.0.01";

    [DllExport("RVExtensionVersion", CallingConvention = CallingConvention.Winapi)]
    public static void RvExtensionVersion(StringBuilder output, int outputSize)
    {
        output.Append("ExtRedis V0.01");
    }

    [DllExport("RVExtension", CallingConvention = CallingConvention.Winapi)]
    public static int RvExtension(StringBuilder output, int outputSize,
        [MarshalAs(UnmanagedType.LPStr)] string function)
    {
        switch (function)
        {
            case "Connect":
                if (RedisController.RedisConnect())
                {
                    output.Append("Connected");
                    return 0;
                }
                else
                {
                    output.Append("ERROR: Couldn't connect to the database");
                    return 1;
                }
            case "Scan":
                int i = 0;
                output.Append("[");
                var scan = RedisController.RedisScan().ToList();
                foreach (var item in scan)
                { 
                    string comma = "";
                    if (i != scan.Count - 1) { comma = ","; }
                    output.Append("\"" + item + "\"" + comma);
                    i++;
                }
                output.Append("]");
                break;
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
                    if(!connected) { output.Append("ERROR: Couldn't connect to the database"); return 1; }
                    output.Append("Connected");
                    return 0;
                } else if (argCount == 3)
                {
                    bool connected = RedisController.RedisConnect(args[0], int.Parse(args[1]), args[2]);
                    if (!connected) { output.Append("ERROR: Couldn't connect to the database"); return 1; }
                    output.Append("Connected");
                    return 0;
                }
            }
            else
            {
                output.Append("No Connection to the database");
                return 0;
            }
        }
        args = args.Select(x => x.Trim('"')).ToArray();
        //I believe doing a switch statement here is a faster way but its kind of gross
        //The switch should compile down to a jump table, but I want a better way to write this, even if it is slower
        //May do some reflection stuff to cast the name of the function and call it rather than this long switch table
        //The table will stay for now as I get the functionality down but will be getting removed later
        //"premature optimization is the root of all evil"
        switch (function)
        {
            #region Strings
            case "Set":
                Strings.Set(args);
                break;
            case "Get":
                output.Append(Strings.Get(args));
                break;
            case "Append":
                Strings.Append(args);
                break;
            case "BitCount":
                Strings.BitCount(args);
                break;
            case "SetNx":
                Strings.SetNx(args);
                break;
            case "SetRange":
                Strings.SetRange(args);
                break;
            case "StrLen":
                Strings.StrLen(args);
                break;
            case "MSet":
                Strings.MSet(args);
                break;
            case "MSetNx":
                Strings.MSetNx(args);
                break;
            case "GetRange":
                Strings.GetRange(args);
                break;
            case "Incr":
                Strings.Incr(args);
                break;
            case "IncrBy":
                Strings.IncrBy(args);
                break;
            case "IncrByFloat":
                Strings.IncrByFloat(args);
                break;
            case "Decr":
                Strings.Decr(args);
                break;
            case "DecrBy":
                Strings.DecrBy(args);
                break;
            case "Del":
                Strings.Del(args);
                break; 
            case "Expire":
                Strings.Expire(args);
                break;
            case "Ttl":
                Strings.Ttl(args);
                break;
            #endregion

            #region Lists
            case "RPush":
                Lists.RPush(args);
                break;
            case "RPushX":
                Lists.RPushX(args);
                break;
            case "LPush":
                Lists.RPush(args);
                break;
            case "LRange":
                Lists.LRange(args);
                break;
            case "LIndex":
                Lists.LIndex(args);
                break;
            case "LInsert":
                Lists.LInsert(args);
                break;
            case "LLen":
                Lists.LLen(args);
                break;
            case "LPop":
                Lists.LPop(args);
                break;
            case "LSet":
                Lists.LSet(args);
                break;
            case "LTrim":
                Lists.LTrim(args);
                break;
            case "RPop":
                Lists.RPop(args);
                break;
            case "RPopLPush":
                Lists.RPopLPush(args);
                break;
            case "BLPop":
                Lists.BLPop(args);
                break;
            case "BRPop":
                Lists.BRPop(args);
                break;
            #endregion
            case "HMSet":
                string key = args[0].Trim('"'); //Get first param which should be the key
                args = args.Skip(1).Take(args.Count() - 1).ToArray(); //Skip the key and leave the pairs left
                Dictionary<string, string> map = new Dictionary<string, string>(); //Make the map with 0 because we concat it later
                bool error = false;
                args.ToList().ForEach(x =>
                {
                    string[] data = SQFUtil.ParamParse(x.Substring(1, x.Length - 1).Substring(0, x.Length - 2)).ToArray(); //Trim the first and last index of the string and give to the help function
                    if (data.Length == 2)
                    {
                        map.Add(data[0].Trim('"'), data[1]);
                    } else
                    {
                        output.Append("ERROR: Key values did not have the amount of 2, got " + data.Length + " instead");
                        error = true;
                        return;
                    }
                });
                if (error) { return 1; } //Don't set anything if the values are wrong
                RedisController.RedisHMSet(key, map); //Set the values to the database
                break;

            case "HGetAll":
                var hold = RedisController.RedisHGetAll(args[0].Trim('"'));
                output.Append("[");
                int i = 0;
                foreach (var item in hold)
                {
                    string comma = "";
                    if (hold.Count - 1 != i)
                    {
                        comma = ",";
                    }
                    output.Append("[\"" + item.Key + "\", " + item.Value + "]" + comma);
                    i++; 
                }
                output.Append("]");
                return 0;
            case "Scan":
                output.Append(RedisController.RedisScan(int.Parse(args[0].Trim('"')), args[1].Trim('"')));
                break;
            case "HScan":
                if (argCount < 2) { return 0; } //Min of 2 args
                Tuple<string, string>[] scan = new Tuple<string, string>[0];

                if(argCount == 2)
                    scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')));
                else if(argCount == 3)
                    scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')), args[2].Trim('"'));
                else if(argCount == 4)
                    scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')), args[2].Trim('"'), int.Parse(args[3]));
                output.Append("[");
                int j = 0;
                foreach (var item in scan)
                {
                    string comma = "";
                    if (j != scan.Length - 1) { comma = ","; }
                    output.Append("[\"" + item.Item1 + "\",\"" + item.Item2 + "\"]" + comma);
                    j++;
                }
                output.Append("]");
                return 0;
            default:
                output.Append("Error, That is not a function");
                return 1;
        }
        return 0;
    }
}

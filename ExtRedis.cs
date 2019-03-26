﻿using System;
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
        switch (function)
        {      
            case "Set":
                RedisController.RedisSet(args[0].Trim('"'), args[1].Trim('"'));
                break;
            case "Get":
                output.Append(RedisController.RedisGet(args[0].Trim('"')));
                break;
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

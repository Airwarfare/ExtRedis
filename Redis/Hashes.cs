using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Hashes
{
    public static object HMSet(string[] args)
    {
        string key = args[0].Trim('"'); //Get first param which should be the key
        args = args.Skip(1).Take(args.Count() - 1).ToArray(); //Skip the key and leave the pairs left
        Dictionary<string, string> map = new Dictionary<string, string>(); //Make the map with 0 because we concat it later
        bool error = false;
        string errorMessage = "";
        args.ToList().ForEach(x =>
        {
            List<string> input = SQFUtil.ParamParse(x);
            if (input.Count % 2 != 0) { errorMessage = "ERROR: Not even number of parameters"; error = true; return; };
            SQFUtil.ParamParse(input).ToList().ForEach(y => map.Add(y.Key, y.Value));
        });
        if (error) { return errorMessage; } //Don't set anything if the values are wrong
        RedisController.RedisHMSet(key, map); //Set the values to the database
        return 0;
    }

    public static string HGetAll(string[] args)
    {
        return SQFUtil.SQFConvert(RedisController.RedisHGetAll(args[0].Trim('"')));
    }

    public static object HScan(string[] args)
    {
        if (args.Length < 2) { return 0; } //Min of 2 args
        Tuple<string, string>[] scan = null;
        if (args.Length == 4)
            scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')), args[2].Trim('"'), int.Parse(args[3]));
        else if (args.Length == 3)
            scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')), args[2].Trim('"'));
        else
            scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')));
        return SQFUtil.SQFConvert(scan.ToDictionary(x => x.Item1, x => x.Item2));
    }

    public static string HGet(string[] args)
    {
        return RedisController.RedisHGet(args[0], args[1]);
    }

    public static void HSet(string[] args)
    {
        RedisController.RedisHSet(args[0], args[1], args[2]);
    }

    public static void HSetNx(string[] args)
    {
        RedisController.RedisHSetNx(args[0], args[1], args[2]);
    }

    public static void HIncrBy(string[] args)
    {
        RedisController.RedisHIncrBy(args[0], args[1], long.Parse(args[2]));
    }

    public static void HDel(string[] args)
    {
        RedisController.RedisHDel(args[0], SQFUtil.ParamParse(args[1]).ToArray());
    }

    public static bool HExists(string[] args)
    {
        return RedisController.RedisHExists(args[0], args[1]);
    }

    public static string HKeys(string[] args)
    {
        return SQFUtil.SQFConvert(RedisController.RedisHKeys(args[0]));
    }

    public static long HLen(string[] args)
    {
        return RedisController.RedisHLen(args[0]);
    }

    public static string HVals(string[] args)
    {
        return SQFUtil.SQFConvert(RedisController.RedisHVals(args[0]));
    }
}
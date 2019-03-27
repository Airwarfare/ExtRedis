using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SortedSets
{
    public static void ZAdd(string[] args)
    {
        RedisController.RedisZAdd(args[0], SQFUtil.ParamParse(args[1]).ToArray());
    }

    public static long ZCard(string[] args)
    {
        return RedisController.RedisZCard(args[0]);
    }

    public static long ZCount(string[] args)
    {
        return RedisController.RedisZCount(args[0], args[1], args[2]);
    }

    public static void ZIncrBy(string[] args)
    {
        RedisController.RedisZIncrBy(args[0], int.Parse(args[1]), args[2]);
    }

    public static string ZRange(string[] args)
    {
        string[] output = null;
        if (args.Length == 4)
            output = RedisController.RedisZRange(args[0], long.Parse(args[1]), long.Parse(args[2]), bool.Parse(args[3]));
        else
            output = RedisController.RedisZRange(args[0], long.Parse(args[1]), long.Parse(args[2]));
        return SQFUtil.SQFConvert(output);
    }

    public static long? ZRank(string[] args)
    {
        return RedisController.RedisZRank(args[0], args[1]);
    }

    public static void ZRem(string[] args)
    {
        RedisController.RedisZRem(args[0], SQFUtil.ParamParse(args[1]));
    }

    public static void ZRemRangeByRank(string[] args)
    {
        RedisController.RedisZRemRangeByRank(args[0], long.Parse(args[1]), long.Parse(args[2]));
    }

    public static void ZRemRangeByScore(string[] args)
    {
        if (args.Length == 4)
            RedisController.RedisZRemRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]), bool.Parse(args[3]));
        else if (args.Length == 5)
            RedisController.RedisZRemRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]), bool.Parse(args[3]), bool.Parse(args[4]));
        else
            RedisController.RedisZRemRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]));
    }
    
    public static double? ZScore(string[] args)
    {
        return RedisController.RedisZScore(args[0], args[1]);
    }

    public static string ZRangeByScore(string[] args)
    {
        string[] output = null;
        if (args.Length == 8)
            output = RedisController.RedisZRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]), bool.Parse(args[3]), bool.Parse(args[4]), bool.Parse(args[5]), long.Parse(args[6]), long.Parse(args[7]));
        else if (args.Length == 7)
            output = RedisController.RedisZRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]), bool.Parse(args[3]), bool.Parse(args[4]), bool.Parse(args[5]), long.Parse(args[6]));
        else if (args.Length == 6)
            output = RedisController.RedisZRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]), bool.Parse(args[3]), bool.Parse(args[4]), bool.Parse(args[5]));
        else if (args.Length == 5)
            output = RedisController.RedisZRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]), bool.Parse(args[3]), bool.Parse(args[4]));
        else if (args.Length == 4)
            output = RedisController.RedisZRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]), bool.Parse(args[3]));
        else
            output = RedisController.RedisZRangeByScore(args[0], double.Parse(args[1]), double.Parse(args[2]));
        return SQFUtil.SQFConvert(output);
    }

    public static string ZScan(string[] args)
    {
        Tuple<string, double>[] tuples = null;
        if (args.Length == 4)
            tuples = RedisController.RedisZScan(args[0], int.Parse(args[1]), args[2], long.Parse(args[4]));
        else if (args.Length == 3)
            tuples = RedisController.RedisZScan(args[0], int.Parse(args[1]), args[2]);
        else
            tuples = RedisController.RedisZScan(args[0], int.Parse(args[1]));
        return SQFUtil.SQFConvert(tuples.ToDictionary(x => x.Item1, x => x.Item2.ToString()), false);
    }
}
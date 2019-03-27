using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Lists
{
    public static void RPush(string[] args)
    {
        RedisController.RedisRPush(args[0], SQFUtil.ParamParse(args[1]));
    }

    public static void RPushX(string[] args)
    {
        RedisController.RedisRPushX(args[0], SQFUtil.ParamParse(args[1]));
    }

    public static void LPush(string[] args)
    {
        RedisController.RedisLPush(args[0], SQFUtil.ParamParse(args[1]));
    }

    public static string LRange(string[] args)
    {
        return SQFUtil.SQFConvert(RedisController.RedisLRange(args[0], long.Parse(args[1]), long.Parse(args[2])));
    }

    public static string LIndex(string[] args)
    {
        return RedisController.RedisLIndex(args[0], long.Parse(args[1]));
    }

    public static void LInsert(string[] args)
    {
        RedisController.RedisLInsert(args[0], (CSRedis.RedisInsert)int.Parse(args[1]), args[2], args[3]);
    }

    public static long LLen(string[] args)
    {
        return RedisController.RedisLLen(args[0]);
    }

    public static string LPop(string[] args)
    {
        return RedisController.RedisLPop(args[0]);
    }

    public static void LSet(string[] args)
    {
        RedisController.RedisLSet(args[0], long.Parse(args[1]), args[2]);
    }

    public static void LTrim(string[] args)
    {
        RedisController.RedisLTrim(args[0], long.Parse(args[1]), long.Parse(args[2]));
    }

    public static string RPop(string[] args)
    {
        return RedisController.RedisRPop(args[0]);
    }

    public static string RPopLPush(string[] args)
    {
        return RedisController.RedisRPopLPush(args[0], args[1]);
    }

    public static void BLPop(string[] args)
    {
        RedisController.RedisBLPop(int.Parse(args[0]), SQFUtil.ParamParse(args[1]).ToArray());
    }

    public static void BRPop(string[] args)
    {
        RedisController.RedisBRPop(int.Parse(args[0]), SQFUtil.ParamParse(args[1]).ToArray());
    }
}
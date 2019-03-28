using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Strings
{
    public static void Set(string[] args)
    {
        RedisController.RedisSet(args[0], args[1]);
    }

    public static string Get(string[] args)
    {
        return RedisController.RedisGet(args[0]);
    }

    public static void Append(string[] args)
    {
        RedisController.RedisAppend(args[0], args[1]);
    }

    public static long BitCount(string[] args)
    {
        if (args.Length == 2)
            return RedisController.RedisBitCount(args[0], long.Parse(args[1]));
        else if (args.Length == 3)
            return RedisController.RedisBitCount(args[0], long.Parse(args[1]), long.Parse(args[2]));
        else
            return RedisController.RedisBitCount(args[0]);
    }

    public static void SetNx(string[] args)
    {
        RedisController.RedisSetNx(args[0], args[1]);
    }

    public static void SetRange(string[] args)
    {
        RedisController.RedisSetRange(args[0], uint.Parse(args[1]), args[2]);
    }

    public static long StrLen(string[] args)
    {
        return RedisController.RedisStrLen(args[0]);
    }

    public static void MSet(string[] args)
    {
        RedisController.RedisMSet(SQFUtil.ParamParse(args[0]).ToArray());
    }

    public static void MSetNx(string[] args)
    {
        RedisController.RedisMSetNx(SQFUtil.ParamParse(args[0]).ToArray());
    }

    public static void GetRange(string[] args)
    {
        RedisController.RedisGetRange(args[0], long.Parse(args[1]), long.Parse(args[2]));
    }

    public static string MGet(string[] args)
    {
        return SQFUtil.SQFConvert(RedisController.RedisMGet(args));
    }

    public static void Incr(string[] args)
    {
        RedisController.RedisIncr(args[0]);
    }

    public static void IncrBy(string[] args)
    {
        RedisController.RedisIncrBy(args[0], long.Parse(args[1]));
    }

    public static void IncrByFloat(string[] args)
    {
        RedisController.RedisIncrByFloat(args[0], double.Parse(args[1]));
    }

    public static void Decr(string[] args)
    {
        RedisController.RedisDecr(args[0]);
    }

    public static void DecrBy(string[] args)
    {
        RedisController.RedisDecrBy(args[0], long.Parse(args[1]));
    }

    public static void Del(string[] args)
    {
        RedisController.RedisDel(args);
    }

    public static void Expire(string[] args)
    {
        RedisController.RedisExpire(args[0], int.Parse(args[1]));
    }

    public static void Ttl(string[] args)
    {
        RedisController.RedisTtl(args[0]);
    }

    public static string Scan(string[] args)
    {
        string[] output = null;
        if  (args.Length == 2)
            output = RedisController.RedisScan(int.Parse(args[0]), args[1]);
        else if(args.Length == 3)
            output = RedisController.RedisScan(int.Parse(args[0]), args[1], long.Parse(args[2]));
        else
            output = RedisController.RedisScan(int.Parse(args[0]));
        return SQFUtil.SQFConvert(output);
    }

    public static string Keys(string[] args)
    {
        return SQFUtil.SQFConvert(RedisController.RedisKeys(args[0]));
    }
}
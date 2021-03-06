﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Sets
{
    public static void SAdd(string[] args)
    {
        RedisController.RedisSAdd(args[0], SQFUtil.ParamParse(args[1]).ToArray());
    }

    public static long SCard(string[] args)
    {
        return RedisController.RedisSCard(args[0]);
    }

    public static void SRem(string[] args)
    {
        RedisController.RedisSRem(args[0], SQFUtil.ParamParse(args[1]));
    }

    public static bool SIsMember(string[] args)
    {
        return RedisController.RedisSIsMember(args[0], args[1]);
    }

    public static string SMembers(string[] args)
    {
        return SQFUtil.SQFConvert(RedisController.RedisSMembers(args[0]), false);
    }

    public static void SUnion(string[] args)
    {
        RedisController.RedisSUnion(args);
    }

    public static void SInter(string[] args)
    {
        RedisController.RedisSInter(args);
    }

    public static void SMove(string[] args)
    {
        RedisController.RedisSMove(args[0], args[1], args[2]);
    }

    public static string SPop(string[] args)
    {
        return RedisController.RedisSPop(args[0]);
    }

    public static string SScan(string[] args)
    {
        string[] output = null;
        if (args.Length == 4)
            output = RedisController.RedisSScan(args[0], int.Parse(args[1]), args[2], long.Parse(args[3]));
        else if (args.Length == 3)
            output = RedisController.RedisSScan(args[0], int.Parse(args[1]), args[2]);
        else
            output = RedisController.RedisSScan(args[0], int.Parse(args[1]));

        return SQFUtil.SQFConvert(output, false);
    }
}
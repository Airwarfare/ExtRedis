using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class RedisController
{
    public static bool Connected = false;
    public static RedisClient Redis { get; set; }
    public static string ErrorMessage { get; private set; }

    #region Connection
    public static bool RedisConnect(string ip, int port, string password)
    {
        try
        {
            Redis = new RedisClient(ip, port);
            Redis.Auth(password);
            Connected = Redis.Connect(100);
            return Connected;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }

    public static bool RedisConnect(string ip, int port)
    {
        try
        {
            Redis = new RedisClient(ip, port);
            Connected = Redis.Connect(100);       
            return Connected;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }

    public static bool RedisConnect()
    {
        try
        {
            Redis = new RedisClient("localhost");
            Connected = Redis.Connect(100);
            return Connected;
        } catch(Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
    #endregion

    #region Setters
    public static void RedisHMSet(string key, Dictionary<string, string> map)
    {
        Redis.HMSet(key, map);
    }

    public static void RedisSet(string key, object value)
    {
        Redis.Set(key, value);
    }

    //Setter
    public static void RedisAppend(string key, object value)
    {
        Redis.Append(key, value);
    }

    //Setter
    public static void RedisSetNx(string key, object value)
    {
        Redis.SetNx(key, value);
    }

    //Setter
    public static void RedisSetRange(string key, uint offset, object value)
    {
        Redis.SetRange(key, offset, value);
    }

    //Setter
    public static void RedisMSet(params string[] keyValues)
    {
        Redis.MSet(keyValues);
    }

    //Setter
    public static void RedisMSetNx(params string[] keyValues)
    {
        Redis.MSetNx(keyValues);
    }

    //Setter
    public static void RedisIncr(string key)
    {
        Redis.Incr(key);
    }

    //Setter
    public static void RedisIncrBy(string key, long amount)
    {
        Redis.IncrBy(key, amount);
    }

    //Setter
    public static void RedisIncrByFloat(string key, double amount)
    {
        Redis.IncrByFloat(key, amount);
    }

    //Setter
    public static void RedisDecr(string key)
    {
        Redis.Decr(key);
    }

    //Setter
    public static void RedisDecrBy(string key, long amount)
    {
        Redis.DecrBy(key, amount);
    }

    //Setter
    public static void RedisExpire(string key, int time)
    {
        Redis.Expire(key, time);
    }

    //Setter
    public static void RedisRPush(string key, params object[] values)
    {
        Redis.RPush(key, values);
    }

    //Setter
    public static void RedisRPushX(string key, params object[] values)
    {
        Redis.RPushX(key, values);
    }

    //Setter
    public static void RedisLPush(string key, params object[] values)
    {
        Redis.LPush(key, values);
    }

    //Setter
    public static void RedisLInsert(string key, RedisInsert insertType, string pivot, object value)
    {
        Redis.LInsert(key, insertType, pivot, value);
    }

    //Setter
    public static void RedisLSet(string key, long index, object value)
    {
        Redis.LSet(key, index, value);
    }

    //Setter
    public static void RedisLTrim(string key, long start, long stop)
    {
        Redis.LTrim(key, start, stop);
    }

    //Setter
    public static void RedisBLPop(int timeout, params string[] keys)
    {
        Redis.BLPopWithKey(timeout, keys);
    }

    //Setter
    public static void RedisBRPop(int timeout, params string[] keys)
    {
        Redis.BRPop(timeout, keys);
    }

    //Setter
    public static void RedisSAdd(string key, params object[] values)
    {
        Redis.SAdd(key, values);
    }


    //Setter
    public static void RedisSRem(string key, params object[] values)
    {
        Redis.SRem(key, values);
    }

    //Setter
    public static void RedisSUnion(params string[] keys)
    {
        Redis.SUnion(keys);
    }
    //Setter
    public static void RedisSInter(params string[] keys)
    {
        Redis.SInter(keys);
    }
    //Setter
    public static void RedisSMove(string source, string destination, object member)
    {
        Redis.SMove(source, destination, member);
    }
    //Setter
    public static void RedisZAdd(string key, params string[] values)
    {
        Redis.ZAdd(key, values);
    }

    //Setter
    public static void RedisZRem(string key, params object[] members)
    {
        Redis.ZRem(key, members);
    }

    //Setter
    public static void RedisZRemRangeByRank(string key, long start, long stop)
    {
        Redis.ZRemRangeByRank(key, start, stop);
    }

    //Setter
    public static void RedisZRemRangeByScore(string key, double min, double max, bool exclusiveMin = false, bool exclusiveMax = false)
    {
        Redis.ZRemRangeByScore(key, min, max, exclusiveMin, exclusiveMax);
    }

    //Setter
    public static void RedisZIncrBy(string key, int amount, string member)
    {
        Redis.ZIncrBy(key, amount, member);
    }

    //Setter
    public static void RedisHSetNx(string key, string field, object value)
    {
        Redis.HSetNx(key, field, value);
    }

    //Setter
    public static void RedisHSet(string key, string field, object value)
    {
        Redis.HSet(key, field, value);
    }

    //Setter
    public static void RedisHIncrBy(string key, string field, long amount)
    {
        Redis.HIncrBy(key, field, amount);
    }

    //Setter
    public static void RedisPfAdd(string key, params object[] objects)
    {
        Redis.PfAdd(key, objects);
    }

    //Setter
    public static void RedisPfMerge(string destKey, params string[] keys)
    {
        Redis.PfMerge(destKey, keys);
    }
    #endregion

    #region Getters
    public static string RedisGet(string key)
    {
        return Redis.Get(key);
    }

    public static Dictionary<string, string> RedisHGetAll(string key)
    {
        return Redis.HGetAll(key);
    }

    public static string[] RedisScan(int cursor = 0, string pattern = null, long? count = null)
    {
        return Redis.Scan(cursor, pattern, count).Items;
    }

    public static Tuple<string,string>[] RedisHScan(string key, int cursor, string pattern = null, long? count = null)
    {
        return Redis.HScan(key, cursor, pattern, count).Items;
    }

    public static string[] RedisSScan(string key, int cursor, string pattern = null, long? count = null)
    {
        return Redis.SScan(key, cursor, pattern, count).Items;
    }

    public static Tuple<string, double>[] RedisZScan(string key, int cursor, string pattern = null, long? count = null)
    {
        return Redis.ZScan(key, cursor, pattern, count).Items;
    }

    //Getter
    public static long RedisStrLen(string key)
    {
        return Redis.StrLen(key);
    }

    //Getter
    public static string RedisGetRange(string key, long start, long end)
    {
        return Redis.GetRange(key, start, end);
    }

    //Getter
    public static string[] RedisMGet(params string[] keyValues)
    {
        return Redis.MGet(keyValues);
    }

    //Getter
    public static long RedisTtl(string key)
    {
        return Redis.Ttl(key);
    }

    //Getter
    public static string[] RedisLRange(string key, long start, long end)
    {
        return Redis.LRange(key, start, end);
    }

    //Getter
    public static string RedisLIndex(string key, long index)
    {
        return Redis.LIndex(key, index);
    }

    //Getter
    public static long RedisLLen(string key)
    {
        return Redis.LLen(key);
    }

    //Getter
    public static long RedisSCard(string key)
    {
        return Redis.SCard(key);
    }

    //Getter
    public static bool RedisSIsMember(string key, object value)
    {
        return Redis.SIsMember(key, value);
    }

    //Getter
    public static string[] RedisSMembers(string key)
    {
        return Redis.SMembers(key);
    }

    //Getter
    public static string RedisSPop(string key)
    {
        return Redis.SPop(key);
    }

    //Getter
    public static long RedisZCard(string key)
    {
        return Redis.ZCard(key);
    }

    //Getter
    public static long RedisZCount(string key, string min, string max)
    {
        return Redis.ZCount(key, min, max);
    }

    //Getter
    public static string[] RedisZRange(string key, long start, long stop, bool withScores = false)
    {
        return Redis.ZRange(key, start, stop, withScores);
    }

    //Getter
    public static long? RedisZRank(string key, string member)
    {
        return Redis.ZRank(key, member);
    }

    //Getter
    public static double? RedisZScore(string key, string member)
    {
        return Redis.ZScore(key, member);
    }

    //Getter
    public static string[] RedisZRangeByScore(string key, double min, double max, bool exclusiveMin = false, bool exclusiveMax = false, bool withScores = false, long? offset = null, long? count = null)
    {
        return Redis.ZRangeByScore(key, min, max, withScores, exclusiveMin, exclusiveMax, offset, count);
    }

    //Getter
    public static string RedisHGet(string key, string field)
    {
        return Redis.HGet(key, field);
    }

    //Getter
    public static bool RedisHExists(string key, string field)
    {
        return Redis.HExists(key, field);
    }

    //Getter
    public static string[] RedisHKeys(string key)
    {
        return Redis.HKeys(key);
    }

    //Getter
    public static long RedisHLen(string key)
    {
        return Redis.HLen(key);
    }

    //Getter
    public static string[] RedisHVals(string key)
    {
        return Redis.HVals(key);
    }

    //Getter
    public static long RedisPfCount(params string[] keys)
    {
        return Redis.PfCount(keys);
    }

    //Getter
    public static long RedisBitCount(string key, long? start = null, long? end = null)
    {
        return Redis.BitCount(key, start, end);
    }
    #endregion

    #region Delete
    public static void RedisDel(params string[] keys)
    {
        Redis.Del(keys);
    }

    //Redis doesn't have a DELETE WHERE clause, next best thing
    //Don't use KEYS for this, redis is single threaded and will prevent other clients from getting their requests fullfilled
    public static void RedisDelByPattern(string pattern)
    {
        string[] keys = RedisScan(0, pattern);
        RedisDel(keys);
    }

    //Delete
    public static void RedisHDel(string key, params string[] fields)
    {
        Redis.HDel(key, fields);
    }
    #endregion

    public static string RedisSelect(int index)
    {
        return Redis.Select(index);
    }

    #region Setters & Getters

    //Setter & Getter
    public static string RedisLPop(string key)
    {
        return Redis.LPop(key);
    }

    //Setter & Getter
    public static string RedisRPop(string key)
    {
        return Redis.RPop(key);
    }

    //Setter & Getter
    public static string RedisRPopLPush(string source, string destination)
    {
        return Redis.RPopLPush(source, destination);
    }

    #endregion

    //Getter
    public static string[] RedisKeys(string pattern)
    {
        return Redis.Keys(pattern);
    }
    //!IMPORTANT, PUB/SUB, NEED TO INJECT SQF FOR THIS FEATURE
}
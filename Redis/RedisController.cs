using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RedisController
{
    public static bool Connected = false;
    public static RedisClient Redis { get; set; }

    #region Connection
    public static bool RedisConnect(string ip, int port, string password)
    {
        try
        {
            Redis = new RedisClient(ip, port);
            Redis.Auth(password);
            Connected = true;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool RedisConnect(string ip, int port)
    {
        try
        {
            Redis = new RedisClient(ip, port);
            Connected = true;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool RedisConnect()
    {
        try
        {
            Redis = new RedisClient("localhost");
            Connected = true;
            return true;
        } catch(Exception ex)
        {
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

    #endregion

    public static string RedisSelect(int index)
    {
        return Redis.Select(index);
    }

    //Setter
    public static void RedisAppend(string key, object value)
    {
        Redis.Append(key, value);
    }

    //Setter
    public static void RedisBitCount(string key, long? start = null, long? end = null)
    {
        Redis.BitCount(key, start, end);
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

    //Getter
    public static long RedisStrLen(string key)
    {
        return Redis.StrLen(key);
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

    //Getter
    public static long RedisTtl(string key)
    {
        return Redis.Ttl(key);
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
}
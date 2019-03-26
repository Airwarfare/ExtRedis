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

    public static void RedisSet(string key, string value)
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
}
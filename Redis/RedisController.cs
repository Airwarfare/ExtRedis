using Sider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RedisController
{
    public static bool Connected = false;
    public static RedisClient Redis { get; set; }
    public static bool RedisConnect()
    {
        try
        {
            Redis = new RedisClient();
            Connected = true;
            return true;
        } catch(Exception ex)
        {
            return false;
        }
    }

    public static void RedisHMSet(string key, IEnumerable<KeyValuePair<string, string>> map)
    {
        Redis.HMSet(key, map);
    }

    public static void RedisSet(string key, string value)
    {
        Redis.Set(key, value);
    }

    public static string RedisGet(string key)
    {
        return Redis.Get(key);
    }

    public static KeyValuePair<string, string>[] RedisHGETALL(string key)
    {
        return Redis.HGetAll(key);
    }
}
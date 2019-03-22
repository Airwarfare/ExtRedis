using Sider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RedisController
{
    public static RedisClient Redis { get; set; }
    public static void RedisConnect()
    {
        Redis = new RedisClient();
    }

    public static void RedisSet(string value)
    {
        Redis.Set("mykey", value);
    }

    public static string RedisGet(string key)
    {
        return Redis.Get(key);
    }
}
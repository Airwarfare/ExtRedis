using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Management
{
    public static string Select(string[] args)
    {
        return RedisController.RedisSelect(int.Parse(args[0]));
    }

    public static string FlushDB(string[] args)
    {
        return RedisController.RedisFlushDB();
    }
    
    public static void FlushAll(string[] args)
    {
        RedisController.RedisFlushAll();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Hyperlog
{
    public static void PFAdd(string[] args)
    {
        RedisController.RedisPfAdd(args[0], SQFUtil.ParamParse(args[1]));
    }

    public static long PFCount(string[] args)
    {
        return RedisController.RedisPfCount(args);
    }

    public static void PFMerge(string[] args)
    {
        RedisController.RedisPfMerge(args[0], SQFUtil.ParamParse(args[1]).ToArray());
    }
}
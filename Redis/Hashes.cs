using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Hashes
{
    public static object HMSet(string[] args)
    {
        string key = args[0].Trim('"'); //Get first param which should be the key
        args = args.Skip(1).Take(args.Count() - 1).ToArray(); //Skip the key and leave the pairs left
        Dictionary<string, string> map = new Dictionary<string, string>(); //Make the map with 0 because we concat it later
        bool error = false;
        string errorMessage = "";
        args.ToList().ForEach(x =>
        {
            string[] data = SQFUtil.ParamParse(x).ToArray(); //Trim the first and last index of the string and give to the help function
            if (data.Length == 2)
            {
                map.Add(data[0].Trim('"'), data[1]);
            }
            else
            {
                errorMessage = "ERROR: Key values did not have the amount of 2, got " + data.Length + " instead";
                error = true;
                return;
            }
        });
        if (error) { return errorMessage; } //Don't set anything if the values are wrong
        RedisController.RedisHMSet(key, map); //Set the values to the database
        return 0;
    }

    public static string HGetAll(string[] args)
    {
        var hold = RedisController.RedisHGetAll(args[0].Trim('"'));
        string output = "[";
        int i = 0;
        foreach (var item in hold)
        {
            string comma = "";
            if (hold.Count - 1 != i)
            {
                comma = ",";
            }
            output += "[\"" + item.Key + "\", " + item.Value + "]" + comma;
            i++;
        }
        output += "]";
        return output;
    }

    public static object HScan(string[] args)
    {
        if (args.Length < 2) { return 0; } //Min of 2 args
        Tuple<string, string>[] scan = new Tuple<string, string>[0];
        StringBuilder output = new StringBuilder();
        if (args.Length == 2)
            scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')));
        else if (args.Length == 3)
            scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')), args[2].Trim('"'));
        else if (args.Length == 4)
            scan = RedisController.RedisHScan(args[0].Trim('"'), int.Parse(args[1].Trim('"')), args[2].Trim('"'), int.Parse(args[3]));
        output.Append("[");
        int j = 0;
        foreach (var item in scan)
        {
            string comma = "";
            if (j != scan.Length - 1) { comma = ","; }
            output.Append("[\"" + item.Item1 + "\"," + item.Item2 + "]" + comma);
            j++;
        }
        output.Append("]");
        return output;
    }
}
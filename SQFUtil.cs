using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SQFUtil
{
    //Takes an input of "string", <params> and splits them
    public static List<string> ParamParse(string input)
    {
        int opened = 0;
        int lastIndex = 0;
        List<string> StringParseList = new List<string>();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == ',' && opened == 1)
            {
                StringParseList.Add(input.Substring(lastIndex + 1, (i - lastIndex) - 1));
                lastIndex = i;
                continue;
            }
            if (input[i] == '[')
            {
                opened++;
            }
            else if (input[i] == ']')
            {
                opened--;
            }
            if (i == input.Length - 1)
            {
                StringParseList.Add(input.Substring(lastIndex + 1, (i - lastIndex) - 1));
            }
        }
        return StringParseList;
    }

    public static Dictionary<string, string> ParamParse(List<string> input)
    {
        Dictionary<string, string> output = new Dictionary<string, string>();
        for (int i = 0; i < input.Count; i += 2)
            output.Add(input[i].Trim('"'), input[i + 1]);
        return output;
    }

    public static string SQFConvert(string[] args)
    {
        if (args.Length == 0) { return ""; }
        string output = "[";
        for (int i = 0; i < args.Length; i++)
        {
            string comma = "";
            if (i != args.Length - 1) { comma = ","; }
            output += "\"" + args[i] + "\"" + comma;
        }
        output += "]";
        return output;
    }

    public static string SQFConvert(Dictionary<string, string> args)
    {
        if (args.Count == 0) { return ""; }
        string output = "[";
        int i = 0;
        foreach (var item in args)
        {
            string comma = "";
            if (i != args.Count - 1) { comma = ","; }
            output += "[\"" + item.Key + "\", \"" + item.Value + "\"]" + comma;
            i++;
        }
        output += "]";
        return output;
    }
}
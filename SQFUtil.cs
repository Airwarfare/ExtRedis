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
}
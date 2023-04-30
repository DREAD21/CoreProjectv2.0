using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.HelpFunctions
{
    public static class HelpFunctions
    {

        public static string[] getName(string names)
        {
            string[] result = new string[names.Length];
            return names.Split(' ');
        }
    }
}
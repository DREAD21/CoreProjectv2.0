using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models
{
    public class Types
    {
        public Types(string _name)
        {
            name = _name;
        }
        string name { get; set; }
    }
}
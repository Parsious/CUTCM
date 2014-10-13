using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUTCM
{
    
    public class Char_Attribute
    {
        public string attribute_name { get; set; }
        public int attribute_value { get; set; }
        public int attribute_feat { get; set; }

        public Char_Attribute()
        {
            
        }

        public Char_Attribute(string name, int value)
        {
            attribute_name = name;
            attribute_value = value;
        }



    }
}

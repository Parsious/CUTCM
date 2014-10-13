using System.Collections.Generic;

namespace CUTCM
{
    public class Char_Skills
    {
        public string skill_name { get; set; }
        public string skill_attribute { get; set; }
        public bool skill_has_spec { get; set; }
        public List<string> level_1_spec { get; set; }
        public List<string> level_2_spec { get; set; }
        public int skill_level { get; set; }
        public string extra_stuff { get; set; }

        public Char_Skills(string name, string attribute,  int level)
        {
            skill_name = name;
            skill_attribute = attribute;
            skill_level = level;
            skill_has_spec = false;
            level_1_spec = new List<string>();
            level_2_spec = new List<string>();
        }

        public Char_Skills(string name, string attribute, int level, string special)
        {
            skill_name = name;
            skill_attribute = attribute;
            skill_level = level;
            skill_has_spec = false;
            level_1_spec = new List<string>();
            level_2_spec = new List<string>();
            extra_stuff = special;
        }

        public Char_Skills()
        {
            skill_level = 0;
            skill_has_spec = false;
            level_1_spec = new List<string>();
            level_2_spec = new List<string>();
        }

        public void add_spec(string name, int level)
        {
            skill_has_spec = true;
            switch (level)
            {
                case 1:
                    level_1_spec.Add(name);
                    break;
                case 2:
                    level_2_spec.Add(name);
                    break;
                default:
                    break;
            }
        }
    }
}

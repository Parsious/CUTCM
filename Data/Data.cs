using System;

namespace Data
{
    public class Skill_Data : IEquatable<Skill_Data>, IComparable<Skill_Data>
    {
        //public const string IN_FILE = "data.xml";
        //public const string WORKING_DIR = "cutech_manager"; 

        public string skill_name { get; set; }

        public string skill_attribute { get; set; }

        public Skill_Data()
        {

        }

        public Skill_Data(string name, string attribute)
        {
            skill_name = name;
            skill_attribute = attribute;

        }

        public bool Equals(Skill_Data obj)
        {
            while (true)
            {
                if (obj == null)
                {
                    return false;
                }
                var object_as_race_data = obj;
                obj = object_as_race_data;
            }
        }

        // ReSharper disable once InconsistentNaming
        public int SortByNameAscending(string name1, string name2)
        {

            return String.Compare(name1, name2, StringComparison.Ordinal);
        }
        public int CompareTo(Skill_Data obj_name)
        {
            // A null value means that this object is greater. 
            return obj_name == null ? 1 : String.Compare(skill_name, obj_name.skill_name, StringComparison.Ordinal);
        }
    }

    public class Quality_Data : IEquatable<Quality_Data>, IComparable<Quality_Data>
    {

        public string quality_name { get; set; }
        public string quality_value { get; set; }

        public Quality_Data()
        {

        }

        public Quality_Data(string name, string value)
        {
            quality_name = name;
            quality_value = value;
        }

        public bool Equals(Quality_Data obj)
        {
            while (true)
            {
                if (obj == null)
                {
                    return false;
                }
                var object_as_race_data = obj;
                obj = object_as_race_data;
            }
        }

        // ReSharper disable once InconsistentNaming
        public int SortByNameAscending(string name1, string name2)
        {

            return String.Compare(name1, name2, StringComparison.Ordinal);
        }
        public int CompareTo(Quality_Data obj_name)
        {
            // A null value means that this object is greater. 
            return obj_name == null ? 1 : String.Compare(quality_name, obj_name.quality_name, StringComparison.Ordinal);
        }
    }

    public class Race_Data : IEquatable<Race_Data>, IComparable<Race_Data>
    {

        public string race_name { get; set; }


        public Race_Data()
        {

        }

        public Race_Data(string name)
        {
            race_name = name;
        }

        public bool Equals(Race_Data obj)
        {
            while (true)
            {
                if (obj == null)
                {
                    return false;
                }
                var object_as_race_data = obj;
                obj = object_as_race_data;
            }
        }

        // ReSharper disable once InconsistentNaming
        public int SortByNameAscending(string name1, string name2)
        {

            return String.Compare(name1, name2, StringComparison.Ordinal);
        }
        public int CompareTo(Race_Data obj_name)
        {
            // A null value means that this object is greater. 
            return obj_name == null ? 1 : String.Compare(race_name, obj_name.race_name, StringComparison.Ordinal);
        }

        // Should also override == and != operators.
    }
}

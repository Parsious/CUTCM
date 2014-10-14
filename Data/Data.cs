using System;
using System.Dynamic;
using Microsoft.Win32;

namespace Data
{
    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public class Psychic_Data : IEquatable<Psychic_Data>, IComparable<Psychic_Data>
    {
        public string power_name { get; set; }

        public Psychic_Data()
        {
            
        }

        public Psychic_Data(string name)
        {
            power_name = name;
        }

        public bool Equals(Psychic_Data other)
        {
            while (true)
            {
                if (other == null)
                {
                    return false;
                }
                var object_as_race_data = other;
                other = object_as_race_data;
            }
        }

        public int CompareTo(Psychic_Data other)
        {
            return other == null ? 1 : String.Compare(power_name, other.power_name, StringComparison.Ordinal);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Spell_Data : IEquatable<Spell_Data>, IComparable<Spell_Data>
    {

        public string spell_name { get; set; }
        public string spell_type { get; set; }
        public int spell_order { get; set; }
        public string spell_insan_dif { get; set; }
        public string spell_insan_con { get; set; }
        public int spell_min_int { get; set; }
        public int spell_min_ten { get; set; }
        public int spell_occult_level { get; set; }
        public bool spell_legality { get; set; }



        public Spell_Data()
        {

        }

        /// <summary>
        ///  this is the primary constructor for the Spell_data class
        /// </summary>
        /// <param name="name">name of the spell</param>
        /// <param name="type">type of the spell</param>
        /// <param name="order">spell order</param>
        /// <param name="insan_dif">insanity check dif</param>
        /// <param name="insan_con">insanity check consiquence</param>
        /// <param name="min_int">minimum int</param>
        /// <param name="min_ten">minimum ten</param>
        /// <param name="occult_level">minimum occult </param>
        /// <param name="legality">spell legality</param>
        public Spell_Data(string name, string type, int order, string insan_dif, string insan_con, int min_int, int min_ten, int occult_level, bool legality)
        {
            spell_name = name;
            spell_insan_con = insan_con;
            spell_insan_dif = insan_dif;
            spell_legality = legality;
            spell_min_int = min_int;
            spell_min_ten = min_ten;
            spell_order = order;
            spell_type = type;
            spell_occult_level = occult_level;

        }

        public bool Equals(Spell_Data other)
        {
            while (true)
            {
                if (other == null)
                {
                    return false;
                }
                var object_as_race_data = other;
                other = object_as_race_data;
            }
        }

        public int CompareTo(Spell_Data other)
        {
            return other == null ? 1 : String.Compare(spell_name, other.spell_name, StringComparison.Ordinal);
        }
    }
}

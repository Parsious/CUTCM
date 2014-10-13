using System.Collections.Specialized;
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Data;
using Data_Manager;

// ReSharper restore RedundantUsingDirective

namespace CUTCM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        #region Constants

        public const string AGI = "agi";
        public const string INT = "int";
        public const string PER = "per";
        public const string PRE = "pre";
        public const string STR = "str";
        public const string TEN = "ten";

        public const int AGI_ATT = 0 ;
        public const int INT_ATT = 1 ;
        public const int PER_ATT = 2 ;
        public const int PRE_ATT = 3 ;
        public const int STR_ATT = 4 ;
        public const int TEN_ATT = 5 ;
        
        public const string ACTIONS = "actions";
        public const string REFLEX = "reflex";
        public const string ORGONE = "orgone";
        public const string VITIALITY = "vitality";
        public const string MOVEMENT = "move";
        public const string DRAMA = "drama";

        public const string DATA = "data";
        public const string CHARACTER = "character";

        public const string ASSET = "asset";
        public const string DRAWBACK = "drawback";

        #endregion
        
        #region private variables

        private Background_Data _background_data = new Background_Data();
        private Character_Data _character_data = new Character_Data();
        private const string DATA_FILE = "data.xml";
        private const string DATA_DIR = "cutech_data";
        private readonly XmlSerializer _data_ser = new XmlSerializer(typeof (Background_Data));
        private int[] att_points_spent = {0, 0, 0, 0, 0, 0};
        private int _spec_count;
        private bool _initial = true;

        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            
            InitializeComponent();

            load_file(DATA, DATA_FILE, DATA_DIR);

            populate_data(_background_data);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data_type"></param>
        /// <param name="file_name"></param>
        /// <param name="path_name"></param>
        private void load_file(string data_type, string file_name, string path_name)
        {

            string file_handle = string.Format(@"{0}{1}", create_path(path_name), file_name);
            test_txt.AppendText("data file to be found at : " + file_handle + "\n");
            
            if (!File.Exists(file_handle))
            {
                test_txt.AppendText("no data file yet\n");
            }
            else
            {
                test_txt.AppendText(" this is where we load the file\n");
                try
                {
                    var input = new FileStream(file_handle, FileMode.Open);
                    test_txt.AppendText(" file opend\n");
                    switch (data_type)
                    {
                        case DATA :
                            test_txt.AppendText(" this ones the data file\n");
                            _background_data = (Background_Data)_data_ser.Deserialize(input);
                            _background_data.sort();
                            break;
                        case CHARACTER :
                            test_txt.AppendText(" this ones the data file\n");
                            throw new NotImplementedException();
                            break;
                        default :
                            test_txt.AppendText(" i have no clue how we got here \n");
                            break;
                    }
                    
                    //_background_data = (Background_Data)_data_ser.Deserialize(input);
                    input.Close();
                }
                catch (Exception)
                {
                    var create_file = new StreamWriter(file_handle);
                    create_file.Close();
                }
                //data.sort();
            }
            

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string create_path(string path)
        {
            string dir = string.Format(@"{0}\{1}\", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passed_data"></param>
        private void populate_data(Background_Data passed_data)
        {
            foreach (var item in passed_data.race_list)
            {
                race_combo.Items.Add(item.race_name);
            }
            foreach (var item in passed_data.skill_list)
            {
                new_skill_name_combo.Items.Add(item.skill_name);
            }
            faction_combo.ItemsSource = passed_data.faction_list;
            virtue_combo.ItemsSource = passed_data.virtue_list;
            flaw_combo.ItemsSource = passed_data.flaw_list;

            _character_data.char_skill_points = passed_data.skill_points;
            _character_data.char_attribute_points = passed_data.attribute_points;
            _character_data.char_cheat_points = passed_data.cheat_points;

            update_attribute_points();
            update_cheat_ponts();
            update_skill_points();

            //cheats_txt.Text = string.Format("{0}", _character_data.char_cheat_points);
            //attribute_text.Text = string.Format("{0}", _character_data.char_attribute_points);
            //skill_qualities_text.Text = skill_text.Text = string.Format("{0}", _character_data.char_skill_points = passed_data.skill_points);

            agi_att_val_c_text.Text =
                agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);
            int_att_val_c_text.Text =
                int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[INT_ATT].attribute_value);
            per_att_val_c_text.Text =
                per_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[PER_ATT].attribute_value);
            pre_att_val_c_text.Text =
                pre_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[PRE_ATT].attribute_value);
            str_att_val_c_text.Text =
                str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[STR_ATT].attribute_value);
            ten_att_val_c_text.Text =
                ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[TEN_ATT].attribute_value);

            agi_att_feat_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_feat);
            int_att_feat_text.Text = string.Format("{0}", _character_data.char_attributes[INT_ATT].attribute_feat);
            per_att_feat_text.Text = string.Format("{0}", _character_data.char_attributes[PER_ATT].attribute_feat);
            pre_att_feat_text.Text = string.Format("{0}", _character_data.char_attributes[PRE_ATT].attribute_feat);
            str_att_feat_text.Text = string.Format("{0}", _character_data.char_attributes[STR_ATT].attribute_feat);
            ten_att_feat_text.Text = string.Format("{0}", _character_data.char_attributes[TEN_ATT].attribute_feat);

            foreach (var item in _background_data.asset_list)
            {
                asset_combo.Items.Add(item.quality_name);
            }
            foreach (var item in _background_data.drawback_list)
            {
                drawback_combo.Items.Add(item.quality_name);
            }
            calc_sec_atts();

        }

        /// <summary>
        /// 
        /// </summary>
        public void update_skill_points()
        {
            skill_qualities_text.Text = skill_text.Text = string.Format("{0}", _character_data.char_skill_points );
        }

        /// <summary>
        /// 
        /// </summary>
        public void update_cheat_ponts()
        {
            cheats_txt.Text = string.Format("{0}", _character_data.char_cheat_points);
        }

        /// <summary>
        /// 
        /// </summary>
        public void update_attribute_points()
        {
            attribute_text.Text = string.Format("{0}", _character_data.char_attribute_points);
        }

        #region secondary Attributes
       /// <summary>
       /// 
       /// </summary>
        private void calc_sec_atts()
        {
            calc_vitality();
            calc_orgone();
            calc_movement();
            calc_reflex();
            calc_actions();
            _character_data.char_secondary_attributes[DRAMA] = 10;
            display_sec_atts();
        }

        /// <summary>
        /// 
        /// </summary>
        private void display_sec_atts()
        {
            display_movement(_character_data.char_secondary_attributes[MOVEMENT]);
            //test_txt.AppendText(string.Format("{0} is {1}\n", ACTIONS, _character_data.char_secondary_attributes[ACTIONS]));
            //test_txt.AppendText(string.Format("{0} is {1}\n",REFLEX, _character_data.char_secondary_attributes[REFLEX]));
            //test_txt.AppendText(string.Format("{0} is {1}\n",ORGONE, _character_data.char_secondary_attributes[ORGONE]));
            //test_txt.AppendText(string.Format("{0} is {1}\n",VITIALITY, _character_data.char_secondary_attributes[VITIALITY]));
            //test_txt.AppendText(string.Format("{0} is {1}\n",DRAMA, _character_data.char_secondary_attributes[DRAMA]));
            //test_txt.AppendText(string.Format("{0} is {1}\n",ACTIONS, _character_data.char_secondary_attributes[ACTIONS]));
 

            actions_text.Text = string.Format("{0}",_character_data.char_secondary_attributes[ACTIONS]);
            reflexes_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[REFLEX]); 
            orgone_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[ORGONE]);
            vitality_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[VITIALITY]);
            drama_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[DRAMA]);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agi_att_val_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            calc_movement();
            calc_reflex();
            calc_actions();
            actions_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[ACTIONS]);
            reflexes_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[REFLEX]);
            display_movement(_character_data.char_secondary_attributes[MOVEMENT]);


            //test_txt.AppendText(string.Format("{0} is {1}\n", ACTIONS, _character_data.char_secondary_attributes[ACTIONS]));
            //test_txt.AppendText(string.Format("{0} is {1}\n", REFLEX, _character_data.char_secondary_attributes[REFLEX]));
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void int_att_val_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            calc_reflex();
            calc_orgone();
            reflexes_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[REFLEX]);
            orgone_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[ORGONE]);

            //test_txt.AppendText(string.Format("{0} is {1}\n", REFLEX, _character_data.char_secondary_attributes[REFLEX]));
            //test_txt.AppendText(string.Format("{0} is {1}\n", ORGONE, _character_data.char_secondary_attributes[ORGONE]));
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void per_att_val_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            calc_reflex();
            calc_actions();
            actions_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[ACTIONS]);
            reflexes_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[REFLEX]);


            //test_txt.AppendText(string.Format("{0} is {1}\n", ACTIONS, _character_data.char_secondary_attributes[ACTIONS]));
            //test_txt.AppendText(string.Format("{0} is {1}\n", REFLEX, _character_data.char_secondary_attributes[REFLEX]));
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void str_att_val_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            calc_vitality();
            calc_movement();
            display_movement(_character_data.char_secondary_attributes[MOVEMENT]);
            vitality_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[VITIALITY]);

            //test_txt.AppendText(string.Format("{0} is {1}\n", VITIALITY, _character_data.char_secondary_attributes[VITIALITY]));
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ten_att_val_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            calc_orgone();
            calc_vitality();
            orgone_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[ORGONE]);
            vitality_text.Text = string.Format("{0}", _character_data.char_secondary_attributes[VITIALITY]);

            //test_txt.AppendText(string.Format("{0} is {1}\n", VITIALITY, _character_data.char_secondary_attributes[VITIALITY]));
            //test_txt.AppendText(string.Format("{0} is {1}\n", ORGONE, _character_data.char_secondary_attributes[ORGONE]));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="move_raw"></param>
        private void display_movement(int move_raw)
        {
            int mph = 0, ypt_n = 0, ypt_c = 0;
            switch (move_raw)
            {
                case 1 :
                    mph = 3;
                    ypt_n = 7;
                    ypt_c = 1;
                    break;
                case 2 :
                case 3 :
                     mph = 5;
                    ypt_n = 13;
                    ypt_c = 3;
                    break;
                case 4 :
                     mph = 7;
                    ypt_n = 17;
                    ypt_c = 4;
                    break;
                case 5 :
                     mph = 9;
                    ypt_n = 22;
                    ypt_c = 5;
                    break;
                case 6 :
                     mph = 11;
                    ypt_n = 27;
                    ypt_c = 6;
                    break;
                case 7 :
                     mph = 13;
                    ypt_n = 32;
                    ypt_c = 8;
                    break;
                case 8 :
                     mph = 15;
                    ypt_n = 37;
                    ypt_c = 9;
                    break;
                case 9 :
                     mph = 17;
                    ypt_n = 42;
                    ypt_c = 10;
                    break;
                case 10:
                     mph = 19;
                    ypt_n = 47;
                    ypt_c = 11;
                    break;
                case 11:
                     mph = 21;
                    ypt_n = 52;
                    ypt_c = 12;
                    break;
                case 12:
                     mph = 23;
                    ypt_n = 57;
                    ypt_c = 13;
                    break;
                case 13:
                     mph = 25;
                    ypt_n = 62;
                    ypt_c = 14;
                    break;
                case 14:
                     mph = 27;
                    ypt_n = 67;
                    ypt_c = 15;
                    break;
                default:
                    break;

            }
            movement_mph_text.Text = mph+"";
            movement_ypt_text.Text = ypt_n + "";
            movement_cautious_text.Text = ypt_c + "";
            //test_txt.AppendText(string.Format("{0} is raw {4} | {1} ( {2} / {3} )\n", MOVEMENT, mph, ypt_n, ypt_c, move_raw));
        }

        /// <summary>
        /// 
        /// </summary>
        private void calc_vitality()
        {
               
            _character_data.char_secondary_attributes[VITIALITY] = 
                ((_character_data.char_attributes[STR_ATT].attribute_value + 
                _character_data.char_attributes[TEN_ATT].attribute_value) / 2) + 5;
        }

        /// <summary>
        /// 
        /// </summary>
        private void calc_orgone()
        {
            _character_data.char_secondary_attributes[ORGONE] =
               (_character_data.char_attributes[INT_ATT].attribute_value +
               _character_data.char_attributes[TEN_ATT].attribute_value)/2 +5 ;
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void calc_movement()
        {
            _character_data.char_secondary_attributes[MOVEMENT] =
            (_character_data.char_attributes[AGI_ATT].attribute_value +
             _character_data.char_attributes[STR_ATT].attribute_value) / 2;
        }

        /// <summary>
        /// 
        /// </summary>
        private void calc_reflex()
        {
            _character_data.char_secondary_attributes[REFLEX] =
                (_character_data.char_attributes[AGI_ATT].attribute_value+
                    _character_data.char_attributes[PER_ATT].attribute_value+
                    _character_data.char_attributes[INT_ATT].attribute_value)/3;
        }

        /// <summary>
        /// 
        /// </summary>
        private void calc_actions()
        {
            
            var ave= (_character_data.char_attributes[AGI_ATT].attribute_value +
                 _character_data.char_attributes[PER_ATT].attribute_value) / 2;
            int final;
            if (ave < 7)
            {
                final = 1;
            }else if (ave > 8)
            {
                final = 3;
            }
            else
            {
                final = 2;
            }
            //test_txt.AppendText(string.Format("{0} is {1} |  {2} \n", ACTIONS, ave, final));
            _character_data.char_secondary_attributes[ACTIONS] = final;
        }
        #endregion

        #region Skill methods
        /// <summary>
        /// 
        /// </summary>
        private void get_initial_skills()
        {

            if (_initial)
            {
                add_skill("Literacy", 2);
                add_skill("Regional Knowledge - ", 2);

                //if (_character_data.char_race != null)
                //{
                //    test_txt.AppendText(_character_data.char_race + " may need to do some language stuff here\n");
                //}  
                _initial = false;
            }
            list_current_skills();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skill_to_match"></param>
        /// <param name="level"></param>
        private void add_skill(string skill_to_match, int level)
        {
            Skill_Data found = null;
            foreach (var item  in _background_data.skill_list)
            {
                if (item.skill_name == skill_to_match)
                {
                    found = item;
                }

            }
            if (found != null)
            {
                switch (skill_to_match)
                {
                    case "Language - Nazzadi":
                        test_txt.AppendText("language skill found " + skill_to_match);
                        _character_data.char_skills_list.Add(new Char_Skills("Language - ", found.skill_attribute, 4,
                            "Nazzadi"));
                        break;
                    case "Language - English":
                        test_txt.AppendText("language skill found " + skill_to_match);
                        _character_data.char_skills_list.Add(new Char_Skills("Language - ", found.skill_attribute, 4,
                            "English"));
                        break;
                    default:
                        if (skill_to_match[skill_to_match.Length - 2] == '-')
                        {
                            test_txt.AppendText("found your skill but yuou need more info \"" + found.skill_name +
                                                "\"\n");

                            var skill_information_request_diag = new Skill_Diag(string.Format("The skill \"{0}\" requires more information :", found.skill_name));

                            skill_information_request_diag.ShowDialog();
                            if (skill_information_request_diag.DialogResult == true)
                            {
                                _character_data.char_skills_list.Add(new Char_Skills(found.skill_name, found.skill_attribute, level, Transporter.transporter_string_1));
                                
                                test_txt.AppendText("\"" + found.skill_name + Transporter.transporter_string_1 +
                                                    "\" added to the list \n");
                            }
                            else
                            {
                                test_txt.AppendText("\"" + found.skill_name +
                                                    "\" not added as insufficent information was found \n");
                            }

                        }
                        else
                        {
                            _character_data.char_skills_list.Add(new Char_Skills(found.skill_name, found.skill_attribute,
                                level));
                            test_txt.AppendText("found your skill \"" + found.skill_name + "\"\n");
                        }
                        break;
                }

            }
            else
            {
                test_txt.AppendText("god damn skill \"" + skill_to_match +
                                    "\"  bloody there isnt it what the hell do i do now \n");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="skill_to_match"></param>
        /// <param name="spec_name"></param>
        /// <param name="spec_level"></param>
        private void add_char_spec_button(string skill_to_match, string spec_name, int spec_level)
        {
            foreach (var item in _character_data.char_skills_list)
            {
                if (item.skill_name == skill_to_match)
                {
                    item.add_spec(spec_name, spec_level);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_skill_button_Click(object sender, RoutedEventArgs e)
        {
            if (new_skill_name_combo.SelectedIndex == -1)
            {
                test_txt.AppendText("no skill name to add ... im not even goign to try  \n");
            }
            else if (string.IsNullOrWhiteSpace(new_skill_value_text.Text))
            {
                test_txt.AppendText("im not bloody trying to add a skill with no levels in it \n");
            }
            else if (int.Parse(new_skill_value_text.Text) < 0 || int.Parse(new_skill_value_text.Text) > 5)
            {
                test_txt.AppendText(
                    "are you havin a giggle make .. try again with a reasonable value  -- hint between 1 and 5 \n");
            }
            else
            {
                bool allready_have_skill = false;
                foreach (var item in _character_data.char_skills_list)
                {
                    if (item.skill_name == new_skill_name_combo.SelectedItem.ToString())
                    {
                        allready_have_skill = true;
                        update_skill_numbers(-int.Parse(new_skill_value_text.Text) + item.skill_level);
                        item.skill_level = int.Parse(new_skill_value_text.Text);
                    }
                }

                if (!allready_have_skill)
                {
                    test_txt.AppendText("well i sent it to add_skill\n");
                    update_skill_numbers(- int.Parse(new_skill_value_text.Text));
                    add_skill(new_skill_name_combo.SelectedItem.ToString(), int.Parse(new_skill_value_text.Text));
                }
            }
            list_current_skills();
        }

        /// <summary>
        /// 
        /// </summary>
        private void list_current_skills()
        {
            current_skill_text.Clear();
            //_character_data.char_skills_list.Sort();
            foreach (var item in _character_data.char_skills_list)
            {
                current_skill_text.AppendText(item.skill_name + item.extra_stuff + " " + item.skill_level + " (" +
                                              item.skill_attribute + ") \n");
                if (item.skill_has_spec)
                {
                    foreach (var item_2 in item.level_1_spec)
                    {
                        current_skill_text.AppendText(string.Format("\tFocus : {0}\n", item_2));
                    }
                    foreach (var item_2 in item.level_2_spec)
                    {
                        current_skill_text.AppendText(string.Format("\tSpecialisation : {0}\n", item_2));
                    }

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_spec_button_Click(object sender, RoutedEventArgs e)
        {
            string skill_for_spec = null;
            if (new_skill_name_combo.SelectedIndex != -1)
            {
                skill_for_spec = new_skill_name_combo.SelectedItem.ToString();
                test_txt.AppendText("selected skill beofre pass to spec was " + skill_for_spec + "\n");
            }
            var new_specilisation = new Specs(_character_data.char_skills_list, skill_for_spec, true);
            new_specilisation.ShowDialog();
            if (new_specilisation.DialogResult == true)
            {
                _spec_count ++;
                if (_spec_count == 1)
                {
                    update_skill_numbers(-1);
                    //skill_text.Text = string.Format("{0}", int.Parse(skill_text.Text) - 1);
                }
                test_txt.AppendText(string.Format("new spec called \n on skill {0} with name {1} at level {2} \n",
                    Transporter.transporter_string_1, Transporter.transporter_string_2, Transporter.transporter_int));
                add_char_spec_button(Transporter.transporter_string_1, Transporter.transporter_string_2,
                    Transporter.transporter_int);
            }
            else
            {
                test_txt.AppendText(string.Format("new spec called \n but somethign was not right about its exit  \n"));
            }
            if (_spec_count == 2)
            {
                add_spec_button.IsEnabled = false;
            }
            list_current_skills();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skill_to_remove"></param>
        private void remove_skill(string skill_to_remove)
        {

            test_txt.AppendText(string.Format("remove skill called on {0}\n", skill_to_remove));
            test_txt.AppendText(string.Format("========================================="));
            foreach (var item in _character_data.char_skills_list)
            {
                test_txt.AppendText(string.Format("skill - {0} \n", item.skill_name));
            }
            test_txt.AppendText(string.Format("========================================="));


            var index_to_remove = -1;
            
            for (var index = 0; index < _character_data.char_skills_list.Count; index++)
            {
                if (_character_data.char_skills_list[index].skill_name == skill_to_remove)
                {
                    test_txt.AppendText(string.Format("found skill in char dataat index {0} \n", index));
                    index_to_remove = index;
                }
               
            }
            if (index_to_remove != -1)
            {
                _character_data.char_skills_list.RemoveAt(index_to_remove);
            }
            else
            {
                test_txt.AppendText(string.Format("skill |{0}| not found \n", skill_to_remove));
                    
            }
            
            list_current_skills();
        }

        #endregion

       
        
        
        #region mostly finished stuff



        

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_output_window_Click(object sender, RoutedEventArgs e)
        {
            output_box.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_warnings_Click(object sender, RoutedEventArgs e)
        {
            warning_light_gradient_stop.Color = Colors.LimeGreen;
            warning_indercator_label.Content = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_menu_item_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// TODO need to work on new menu item ... needs diag box and other logic to start new build 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void new_menu_item_Click(object sender, RoutedEventArgs e)
        {
            output_box.AppendText(
                "new character created - Probably need some sort of diag box here to ask if your bloody shure");
            warning_light_gradient_stop.Color = Colors.Red;
            warning_indercator_label.Content = "please check Output window for message";
            _character_data = create_new_character_item();
        }

        /// <summary>
        /// TODO need to add diag box and stuff to close off old char nicely and init loaded character into the right places 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_menu_item_Click(object sender, RoutedEventArgs e)
        {
            output_box.AppendText(
                "new character being opened - Pprobably need some sort of diag box here to ask if your bloody shure");
            warning_light_gradient_stop.Color = Colors.Red;
            warning_indercator_label.Content = "please check Output window for message";
            //_character_data = create_new_character_item();
        }

        /// <summary>
        /// TODO need to add save diags and then push name into recent list ... actually need to add recent list to the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_menu_item_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            //_character_data = create_new_character_item();
        }

        /// <summary>
        /// TODO same as save actually perhaps some extras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_as_menu_item_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO need to add diag to make sure you actuall want to close and check if you want to save 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cose_menu_item_Click(object sender, RoutedEventArgs e)
        {
            output_box.AppendText(
                "current character closed - Probably need some sort of diag box here to ask if your bloody shure");
            warning_light_gradient_stop.Color = Colors.Red;
            warning_indercator_label.Content = "please check Output window for message";
            //_character_data = create_new_character_item();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Character_Data create_new_character_item()
        {
            _initial = true;
            return new Character_Data();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void test_data_button_Click(object sender, RoutedEventArgs e)
        {
            _background_data.display_list(new[]
            {
                output_box,
                output_box,
                output_box,
                output_box,
                output_box,
                output_box
            }, false, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void test_combos_Click(object sender, RoutedEventArgs e)
        {

            if (flaw_combo.SelectedIndex != -1)
            {
                test_txt.AppendText(flaw_combo.SelectedItem + "\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_test_button_Click(object sender, RoutedEventArgs e)
        {
            test_txt.Clear();
        }

        #endregion

        

        /// <summary>
        ///  TODO: need to add logic to get the + and - buttons to turn on and off
        /// </summary>

        #region attribute buttons
        
        ///
        private void agi_p_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) - 3);
            update_cheat_numbers(-3);
            _character_data.char_attributes[0].attribute_value += 1;
            agi_att_val_c_text.Text =
                agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[0].attribute_value);
            agi_att_c_spent_text.Text = string.Format("{0}", int.Parse(agi_att_c_spent_text.Text) + 1);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agi_p_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) - 1);
            update_attribute_numbers(-1);
            _character_data.char_attributes[0].attribute_value += 1;
            att_points_spent[0] += 1;
            agi_att_val_c_text.Text =
                agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[0].attribute_value);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agi_n_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) + 1);
            update_attribute_numbers(1);
            _character_data.char_attributes[0].attribute_value -= 1;
            att_points_spent[0] -= 1;
            agi_att_val_c_text.Text =
                agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[0].attribute_value);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agi_n_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) + 3);
            update_cheat_numbers(3);
            _character_data.char_attributes[0].attribute_value -= 1;
            agi_att_val_c_text.Text =
                agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[0].attribute_value);
            agi_att_c_spent_text.Text = string.Format("{0}", int.Parse(agi_att_c_spent_text.Text) - 1);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void int_n_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) + 1);
            update_attribute_numbers(1);
            _character_data.char_attributes[1].attribute_value -= 1;
            att_points_spent[1] -= 1;
            int_att_val_c_text.Text =
                int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[1].attribute_value);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void int_p_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) - 1);
            update_attribute_numbers(-1);
            _character_data.char_attributes[1].attribute_value += 1;
            att_points_spent[1] += 1;
            int_att_val_c_text.Text =
                int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[1].attribute_value);
            int_n_att_button.IsEnabled = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void int_p_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) - 3);
            update_cheat_numbers(-3);
            _character_data.char_attributes[1].attribute_value += 1;
            int_att_val_c_text.Text =
                int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[1].attribute_value);
            int_att_c_spent_text.Text = string.Format("{0}", int.Parse(int_att_c_spent_text.Text) + 1);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void int_n_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) + 3);
            update_cheat_numbers(3);
            _character_data.char_attributes[1].attribute_value -= 1;
            int_att_val_c_text.Text =
                int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[1].attribute_value);
            int_att_c_spent_text.Text = string.Format("{0}", int.Parse(int_att_c_spent_text.Text) - 1);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void per_p_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) - 3);
            update_cheat_numbers(-3);
            _character_data.char_attributes[2].attribute_value += 1;
            per_att_val_c_text.Text =
                per_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[2].attribute_value);
            per_att_c_spent_text.Text = string.Format("{0}", int.Parse(per_att_c_spent_text.Text) + 1);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void per_n_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) + 3);
            update_cheat_numbers(3);
            _character_data.char_attributes[2].attribute_value -= 1;
            per_att_val_c_text.Text =
                per_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[2].attribute_value);
            per_att_c_spent_text.Text = string.Format("{0}", int.Parse(per_att_c_spent_text.Text) - 1);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void per_n_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) + 1);
            update_attribute_numbers(1);
            _character_data.char_attributes[2].attribute_value -= 1;
            att_points_spent[2] -= 1;
            per_att_val_c_text.Text =
                per_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[2].attribute_value);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void per_p_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) - 1);
            update_attribute_numbers(-1);
            _character_data.char_attributes[2].attribute_value += 1;
            att_points_spent[2] += 1;
            per_att_val_c_text.Text =
                per_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[2].attribute_value);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pre_p_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) - 1);
            update_attribute_numbers(-1);
            _character_data.char_attributes[3].attribute_value += 1;
            att_points_spent[3] += 1;
            pre_att_val_c_text.Text =
                pre_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[3].attribute_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pre_n_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) + 1);
            update_attribute_numbers(1);
            _character_data.char_attributes[3].attribute_value -= 1;
            att_points_spent[3] -= 1;
            pre_att_val_c_text.Text =
                pre_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[3].attribute_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pre_p_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) - 3);
            update_cheat_numbers(-3);
            _character_data.char_attributes[3].attribute_value += 1;
            pre_att_val_c_text.Text =
                pre_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[3].attribute_value);
            pre_att_c_spent_text.Text = string.Format("{0}", int.Parse(pre_att_c_spent_text.Text) + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pre_n_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) + 3);
            update_cheat_numbers(3);
            _character_data.char_attributes[3].attribute_value -= 1;
            pre_att_val_c_text.Text =
                pre_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[3].attribute_value);
            pre_att_c_spent_text.Text = string.Format("{0}", int.Parse(pre_att_c_spent_text.Text) - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void str_p_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) - 3);
            update_cheat_numbers(-3);
            _character_data.char_attributes[4].attribute_value += 1;
            str_att_val_c_text.Text =
                str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[4].attribute_value);
            str_att_c_spent_text.Text = string.Format("{0}", int.Parse(str_att_c_spent_text.Text) + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void str_n_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) + 3);
            update_cheat_numbers(3);
            _character_data.char_attributes[4].attribute_value -= 1;
            str_att_val_c_text.Text =
                str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[4].attribute_value);
            str_att_c_spent_text.Text = string.Format("{0}", int.Parse(str_att_c_spent_text.Text) - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void str_p_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) - 1);
            update_attribute_numbers(-1);
            _character_data.char_attributes[4].attribute_value += 1;
            att_points_spent[4] += 1;
            str_att_val_c_text.Text =
                str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[4].attribute_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void str_n_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) + 1);
            update_attribute_numbers(1);
            _character_data.char_attributes[4].attribute_value -= 1;
            att_points_spent[4] -= 1;
            str_att_val_c_text.Text =
                str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[4].attribute_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ten_p_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) - 1);
            update_attribute_numbers(-1);
            _character_data.char_attributes[5].attribute_value += 1;
            att_points_spent[5] += 1;
            ten_att_val_c_text.Text =
                ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[5].attribute_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ten_n_att_button_Click(object sender, RoutedEventArgs e)
        {
            //attribute_text.Text = string.Format("{0}", int.Parse(attribute_text.Text) + 1);
            update_attribute_numbers(1);
            _character_data.char_attributes[5].attribute_value -= 1;
            att_points_spent[5] -= 1;
            ten_att_val_c_text.Text =
                ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[5].attribute_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ten_p_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) - 3);\
            update_cheat_numbers(-3);
            _character_data.char_attributes[5].attribute_value += 1;
            ten_att_val_c_text.Text =
                ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[5].attribute_value);
            ten_att_c_spent_text.Text = string.Format("{0}", int.Parse(ten_att_c_spent_text.Text) + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ten_n_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) + 3);
            update_cheat_numbers(3);
            _character_data.char_attributes[5].attribute_value -= 1;
            ten_att_val_c_text.Text =
                ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[5].attribute_value);
            ten_att_c_spent_text.Text = string.Format("{0}", int.Parse(ten_att_c_spent_text.Text) - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skill_p_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) - 1);
            update_cheat_numbers(-1);
            skill_cheat_text.Text = string.Format("{0}", int.Parse(skill_cheat_text.Text) + 1);
            update_skill_numbers(2);
            //skill_text.Text = string.Format("{0}", int.Parse(skill_text.Text) + 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skill_n_cheat_button_Click(object sender, RoutedEventArgs e)
        {
            //cheats_txt.Text = string.Format("{0}", int.Parse(cheats_txt.Text) + 1);
            update_cheat_numbers(1);
            skill_cheat_text.Text = string.Format("{0}", int.Parse(skill_cheat_text.Text) - 1);
            update_skill_numbers(-2);
            //skill_text.Text = string.Format("{0}", int.Parse(skill_text.Text) - 2);

        }

        #endregion


        #region numbers updates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void update_cheat_numbers(int value)
        {
            _character_data.char_cheat_points += value;
            update_cheat_ponts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void update_skill_numbers(int value)
        {
            //skill_qualities_text.Text = skill_text.Text = string.Format("{0}", int.Parse(skill_text.Text) + value);
            _character_data.char_skill_points += value;
            update_skill_points();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void update_attribute_numbers(int value)
        {
            _character_data.char_attribute_points += value;
            update_attribute_points();

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        //private void display_qualities()
        private void display_qualities()
        {
            assets_text.Clear();
            //_character_data.char_assets_1.Sort();
            //_character_data.char_drawbacks_1.Sort();
            foreach (var item in _character_data.char_assets)
            {
                assets_text.AppendText(string.Format("{0} ( {1} ) \n", item.quality_name, item.quality_value));
            }
            drawbacks_txt.Clear();
            foreach (var item in _character_data.char_drawbacks)
            {
                drawbacks_txt.AppendText(string.Format("{0} ( {1} ) \n", item.quality_name, item.quality_value));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        private void add_quality(string name, string value, string type, bool initial)
        {

            switch (type)
             {
                case DRAWBACK:
                    _character_data.char_drawbacks.Add(new Quality_Data(name, value));
                     if (!initial)
                     {
                         update_skill_numbers(int.Parse(value));
                     }
                    break;
                case ASSET:
                    _character_data.char_assets.Add(new Quality_Data(name, value));
                     if (!initial)
                     {
                         update_skill_numbers(-int.Parse(value));
                     }
                    break;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quality_to_remove"></param>
        /// <param name="type"></param>
        public void remove_quality(string quality_to_remove, string type)
        {
            var index_to_remove = -1;
            switch (type)
            {
                case ASSET:

                    test_txt.AppendText(string.Format("remove skill called on {0}\n", quality_to_remove));
                    for (var index = 0; index < _character_data.char_assets.Count; index++)
                    {
                        if (_character_data.char_assets[index].quality_name != quality_to_remove) continue;
                        test_txt.AppendText(string.Format("found skill in char dataat index {0} \n", index));
                        index_to_remove = index;

                    }
                    _character_data.char_assets.RemoveAt(index_to_remove);
                    break;

                case DRAWBACK :
                    test_txt.AppendText(string.Format("remove skill called on {0}\n", quality_to_remove));
                    for (var index = 0; index < _character_data.char_drawbacks.Count; index++)
                    {
                        if (_character_data.char_drawbacks[index].quality_name != quality_to_remove) continue;
                        test_txt.AppendText(string.Format("found skill in char dataat index {0} \n", index));
                        index_to_remove = index;
                        
                    }
                    if (index_to_remove != -1)
                    {
                        _character_data.char_drawbacks.RemoveAt(index_to_remove);
                    }
                    break;
            }
            list_current_skills();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_asset_button_Click(object sender, RoutedEventArgs e)
        {
            string value = "";
            bool got_quality = false;
            string search_term = asset_combo.SelectedItem.ToString();

            if (asset_combo.SelectedIndex != -1)
            {
                foreach (var item in _background_data.asset_list.Where(item => item.quality_name == search_term))
                {
                    value = item.quality_value;
                }
                foreach (var item in _character_data.char_assets) 
                {
                    if (item.quality_name == search_term )
                    {
                        if (search_term[search_term.Length - 1] != '*')
                        {
                            got_quality = true;
                        }
                    }
                }
                if (!got_quality)
                {
                    if (value.Length > 1)
                    {
                        add_mult_point_quality(search_term, value, "asset");
                    }
                    else
                    {
                        add_quality(search_term, value, "asset", false);
                    }
                }
                else
                {
                    test_txt.AppendText("you allready have this asset\n");
                }
            }
            //_background_data.asset_list.Sort();
            display_qualities();
        }

        /// <summary>
        /// TODO: sort asset and drawback lists 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_drawback_button_Click(object sender, RoutedEventArgs e)
        {
            string value = "";
            bool got_quality = false;
            string search_term = drawback_combo.SelectedItem.ToString();

            if (drawback_combo.SelectedIndex != -1)
            {
                // despite what i think each time when i look at the code this is needed 
                foreach (var item in _background_data.drawback_list.Where(item => item.quality_name == search_term))
                {
                    value = item.quality_value;
                }
                foreach (var item in _character_data.char_drawbacks)
                {
                    if (item.quality_name == search_term)
                    {
                        if (search_term[search_term.Length - 1] != '*')
                        {
                            got_quality = true;
                        }
                    }
                }

                if (!got_quality)
                {
                    if (value.Length > 1)
                    {
                        add_mult_point_quality(search_term, value, "drawback");
                    }
                    else
                    {
                        add_quality(search_term, value, "drawback", false);
                    }
                }
                else
                {
                    test_txt.AppendText("you allready have this drawback\n");
                }
            }
            //_background_data.drawback_list.Sort();
           display_qualities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        private void add_mult_point_quality(string name, string value, string type)
        {
            var quality = new Quality_Multi_Diag(name, value, type);
            quality.ShowDialog();

            if (quality.DialogResult != true) return;
            switch (Transporter.transporter_string_2)
            {
                case "drawback":
                    _character_data.char_drawbacks.Add(new Quality_Data(Transporter.transporter_string_1, Transporter.transporter_int.ToString()));
                    update_skill_numbers(Transporter.transporter_int);
                    break;
                case "asset":
                    _character_data.char_assets.Add(new Quality_Data(Transporter.transporter_string_1, Transporter.transporter_int.ToString()));
                    update_skill_numbers(-Transporter.transporter_int);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void faction_combo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string new_vlaue = faction_combo.SelectedItem.ToString();
            get_initial_skills();
            test_txt.AppendText(string.Format( "origional  value : {0} \tnew  value : {1}\n", _character_data.char_alegiance , new_vlaue ));
            _character_data.char_alegiance = new_vlaue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void race_combo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var new_vlaue = race_combo.SelectedItem.ToString();
            var old_value = _character_data.char_race;
            var race_diag = new Nazzadi_Language_Diag();

            get_initial_skills();
            
            test_txt.AppendText(string.Format("origional  value : {0} \tnew  value : {1}\n", old_value, new_vlaue));
            _character_data.char_race = new_vlaue;

            switch (old_value)
            {
                case "Human" :
                    remove_skill("Language* - English");
                    update_skill_numbers(-2);
                    update_attribute_numbers(-1);
                    break; 
                
                case "Nazzadi" :
                    remove_quality("Nightvision", ASSET);
                    remove_skill("Language* - Nazzadi");
                    remove_skill("Language* - English");
                    _character_data.char_attributes[AGI_ATT].attribute_value -= 1;
                    agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);
                    break;

                case "Xenomix (Amlati)":
                    remove_quality("Nightvision", ASSET);
                    remove_quality("Misfit", DRAWBACK);
                    remove_quality("Alluring*", ASSET);
                    remove_skill("Language* - English");
                    update_skill_numbers(-1);
                    switch (_character_data.xenomix_att_chosen)
                    {
                        case AGI_ATT :
                            _character_data.xenomix_att_chosen = -1;
                            _character_data.char_attributes[AGI_ATT].attribute_value -= 1;
                            agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);
                            break;
                        case TEN_ATT:
                            _character_data.xenomix_att_chosen = -1;
                            _character_data.char_attributes[TEN_ATT].attribute_value -= 1;
                            agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[TEN_ATT].attribute_value);
                            break;
                        default :
                            test_txt.AppendText("nothing to do in attribute removal\n");
                            break;
                    }
                    break;

                case "Xenomix White (Sidoci)":
                    remove_quality("White", ASSET);

                    remove_quality("Nightvision", ASSET);
                    remove_quality("Alluring*", ASSET);

                    remove_quality("Latent Para-Psychic", ASSET);
                    remove_quality("Erupted Para-Psychic", ASSET);

                    remove_quality("Misfit", DRAWBACK);
                    remove_quality("Watched", DRAWBACK);
                    remove_skill("Language* - English");

                    _character_data.char_attributes[TEN_ATT].attribute_value -= 1;
                                ten_att_val_c_text.Text = ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[TEN_ATT].attribute_value);
                                _character_data.xenomix_att_chosen = TEN_ATT;
                    break;
                
                case "Migou":
                    _character_data.char_attributes[AGI_ATT].attribute_value -= 2;
                    agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);

                    _character_data.char_attributes[INT_ATT].attribute_value -= 2;
                    int_att_val_c_text.Text = int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[INT_ATT].attribute_value);
                                        
                    _character_data.char_attributes[STR_ATT].attribute_value += 1;
                    str_att_val_c_text.Text = str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[STR_ATT].attribute_value);

                    remove_skill("Language* - English");
                    remove_skill("Language* - Nazzadi");

                    remove_skill(string.Format("Language* - {0}", _character_data.migou_lan_1));
                    remove_skill(string.Format("Language* - {0}", _character_data.migou_lan_2));

                    remove_skill("Occult*");

                    break;
                
                case "Sanctified":
                     _character_data.char_attributes[AGI_ATT].attribute_value -= 1;
                    agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);

                    _character_data.char_attributes[INT_ATT].attribute_value -= 2;
                    int_att_val_c_text.Text = int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[INT_ATT].attribute_value);
                                        
                    _character_data.char_attributes[STR_ATT].attribute_value += 2;
                    str_att_val_c_text.Text = str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[STR_ATT].attribute_value);

                    _character_data.char_attributes[TEN_ATT].attribute_value -= 1;
                    ten_att_val_c_text.Text = ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[TEN_ATT].attribute_value);

                    remove_skill("Language* - English");
                    remove_skill("Language* - Nazzadi");

                    remove_skill(string.Format("Language* - {0}", _character_data.migou_lan_1));
                    remove_skill(string.Format("Language* - {0}", _character_data.migou_lan_2));

                    remove_skill("Occult*");

                    remove_skill("Language* - Arabic");
                    remove_skill("Language* - Latin");
                    remove_skill("Language* - R'lyehan");

                    break;
                default:
                    break;
            }

            switch (new_vlaue)
            {
                case "Human" :
                    add_skill("Language* - English", 4);
                    update_skill_numbers(2);
                    update_attribute_numbers(1);
                    break; 
                
                case "Nazzadi" :
                    add_quality("Nightvision", "3", ASSET, true);
                    _character_data.char_attributes[AGI_ATT].attribute_value += 1;
                    agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);
                    race_diag.ShowDialog();
                    if (race_diag.DialogResult == true)
                    {
                        var race_lang_choice = Transporter.transporter_string_1.Split(' ')[1];
                        test_txt.AppendText(string.Format("it cant be this easy -  {0} \n", race_lang_choice));
                        switch (race_lang_choice)
                        {
                            case "English":
                                add_skill("Language* - English", 4);
                                break;
                            case "Nazzadi":
                                add_skill("Language* - Nazzadi", 4);
                                break;
                            case "Both":
                                add_skill("Language* - English", 2);
                                add_skill("Language* - Nazzadi", 2);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        test_txt.AppendText("sommin went wrong here \n");
                    }
                    
                    break;

                case "Xenomix (Amlati)":
                    //test_txt.AppendText("xenomix chosen \n");
                    add_quality("Nightvision", "3", ASSET, true);
                    add_quality("Alluring*", "1", ASSET, true);
                    add_quality("Misfit", "1", DRAWBACK, true);
                    add_skill("Language* - English", 4);
                    update_skill_numbers(1);
                    var race_att = new Xenomix_Race_Diag();

                    race_att.ShowDialog();
                    if (race_att.DialogResult == true)
                    {
                        var race_att_choice = Transporter.transporter_string_1.Split(' ')[1];
                        test_txt.AppendText(string.Format("xenomix attribute chosen as  -  {0} \n", race_att_choice));
                        switch (race_att_choice)
                        {
                            case "Agility":
                                _character_data.char_attributes[AGI_ATT].attribute_value += 1;
                                agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);
                                _character_data.xenomix_att_chosen = AGI_ATT;
                                break;
                            case "Tenacity":
                                _character_data.char_attributes[TEN_ATT].attribute_value += 1;
                                ten_att_val_c_text.Text = ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[TEN_ATT].attribute_value);
                                _character_data.xenomix_att_chosen = TEN_ATT;
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case "Xenomix White (Sidoci)":
                    add_quality("White", "4", ASSET, false);

                    add_quality("Nightvision", "3", ASSET, true);
                    add_quality("Alluring*", "1", ASSET, true);

                    add_quality("Latent Para-Psychic", "4", ASSET, true);
                    add_quality("Erupted Para-Psychic", "2", ASSET, true); 

                    add_quality("Misfit", "4", DRAWBACK, true);
                    add_quality("Watched", "3", DRAWBACK, true);
                    add_skill("Language* - English", 4);

                    _character_data.char_attributes[TEN_ATT].attribute_value += 1;
                    ten_att_val_c_text.Text = ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);
                    _character_data.xenomix_att_chosen = TEN_ATT;
                    
                    break;


                case "Migou":
                    _character_data.char_attributes[AGI_ATT].attribute_value += 2;
                    agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);

                    _character_data.char_attributes[INT_ATT].attribute_value += 2;
                    int_att_val_c_text.Text = int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[INT_ATT].attribute_value);
                                        
                    _character_data.char_attributes[STR_ATT].attribute_value -= 1;
                    str_att_val_c_text.Text = str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[STR_ATT].attribute_value);


                    add_skill("Language* - English", 4);
                    add_skill("Language* - Nazzadi", 4);

                    var skill_information_request_diag = new Skill_Diag(string.Format("The skill \"Language* - \" requires more information :"));
                    test_txt.AppendText("this is the first of the race language skills ---- 1 ---- \n");
                    skill_information_request_diag.ShowDialog();
                    if (skill_information_request_diag.DialogResult == true)
                    {
                        _character_data.char_skills_list.Add(new Char_Skills("Language* - " + Transporter.transporter_string_1, "Intellect", 2));

                        test_txt.AppendText("\"" + "Language* - " + Transporter.transporter_string_1 + "\" added to the list \n");

                        _character_data.migou_lan_1 = Transporter.transporter_string_1;
                    }
                    else
                    {
                        test_txt.AppendText("\"" + "Language* - " + "\" not added as insufficent information was found \n");
                    }

                    var skill_information_request_diag_1 = new Skill_Diag(string.Format("The skill \"Language* - \" requires more information :"));
                    test_txt.AppendText("this is the second of the race language skills ---- 2 ---- \n");
                    skill_information_request_diag_1.ShowDialog();
                    if (skill_information_request_diag_1.DialogResult == true)
                    {
                        _character_data.char_skills_list.Add(new Char_Skills("Language* - " + Transporter.transporter_string_1, "Intellect", 2));

                        test_txt.AppendText("\"" + "Language* - " + Transporter.transporter_string_1 + "\" added to the list \n");

                        _character_data.migou_lan_2 = Transporter.transporter_string_1;
                    }
                    else
                    {
                        test_txt.AppendText("\"" + "Language* - " + "\" not added as insufficent information was found \n");
                    }

                    add_skill("Occult*", 2);
                    
                    break;


                case "Sanctified":
                    test_txt.AppendText("we are in the scantified cood  for race adds\n");

                     _character_data.char_attributes[AGI_ATT].attribute_value += 1;
                    agi_att_val_c_text.Text = agi_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[AGI_ATT].attribute_value);

                    _character_data.char_attributes[INT_ATT].attribute_value += 2;
                    int_att_val_c_text.Text = int_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[INT_ATT].attribute_value);
                                        
                    _character_data.char_attributes[STR_ATT].attribute_value -= 2;
                    str_att_val_c_text.Text = str_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[STR_ATT].attribute_value);

                    _character_data.char_attributes[TEN_ATT].attribute_value += 1;
                    ten_att_val_c_text.Text = ten_att_val_text.Text = string.Format("{0}", _character_data.char_attributes[TEN_ATT].attribute_value);
                    add_skill("Language* - English", 4);
                    add_skill("Language* - Nazzadi", 4);

                    var skill_information_request_diag_2 = new Skill_Diag(string.Format("The skill \"Language* - \" requires more information :"));
                    test_txt.AppendText("this is the first of the race language skills ---- 1 ---- \n");
                    skill_information_request_diag_2.ShowDialog();
                    if (skill_information_request_diag_2.DialogResult == true)
                    {
                        _character_data.char_skills_list.Add(new Char_Skills("Language* - " + Transporter.transporter_string_1, "Intellect", 2));

                        test_txt.AppendText("\"" + "Language* - " + Transporter.transporter_string_1 + "\" added to the list \n");

                        _character_data.migou_lan_1 = Transporter.transporter_string_1;
                    }
                    else
                    {
                        test_txt.AppendText("\"" + "Language* - " + "\" not added as insufficent information was found \n");
                    }

                    var skill_information_request_diag_3 = new Skill_Diag(string.Format("The skill \"Language* - \" requires more information :"));
                    test_txt.AppendText("this is the second of the race language skills ---- 2 ---- \n");
                    skill_information_request_diag_3.ShowDialog();
                    if (skill_information_request_diag_3.DialogResult == true)
                    {
                        _character_data.char_skills_list.Add(new Char_Skills("Language* - " + Transporter.transporter_string_1, "Intellect", 2));

                        test_txt.AppendText("\"" + "Language* - " + Transporter.transporter_string_1 + "\" added to the list \n");

                        _character_data.migou_lan_2 = Transporter.transporter_string_1;
                    }
                    else
                    {
                        test_txt.AppendText("\"" + "Language* - " + "\" not added as insufficent information was found \n");
                    }
                    add_skill("Occult*", 2);

                    _character_data.char_skills_list.Add(new Char_Skills("Language* - Arabic", "Intellect", 2));
                    _character_data.char_skills_list.Add(new Char_Skills("Language* - Latin", "Intellect", 2));
                    _character_data.char_skills_list.Add(new Char_Skills("Language* - R'lyehan", "Intellect", 2));

                    
                    break;
                default:
                    break;
            }
            display_qualities();
            list_current_skills();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void virtue_combo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string thingie = virtue_combo.SelectedItem.ToString();
            get_initial_skills();
            test_txt.AppendText(string.Format("origional  value : {0} \tnew  value : {1}\n", _character_data.char_virtue, thingie));
            _character_data.char_virtue = thingie;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flaw_combo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string thingie = flaw_combo.SelectedItem.ToString();
            get_initial_skills();
            test_txt.AppendText(string.Format("origional  value : {0} \tnew  value : {1}\n", _character_data.char_flaw, thingie));
            _character_data.char_flaw = thingie;
        }
    }

    /// <summary>
// ReSharper disable CSharpWarnings::CS1570
    /// TODO need to add a sort feature for the Char_Skills class, the qualities calllses and this class  so we can sort that list it nay be as simple as implimenting the equals and comparable stuff in Character_data class but we may also need to trow IEquatable<> and  IComparable<> at the Char_skills class 
// ReSharper restore CSharpWarnings::CS1570
    /// 
    /// </summary>
    public class Character_Data : IEquatable<Character_Data>, IComparable<Character_Data>
    {
        public string char_name { get; set; }
        public string char_race { get; set; }
        public string char_alegiance { get; set; }
        public string char_callsign { get; set; }
        public string char_profession { get; set; }
        public string char_virtue { get; set; }
        public string char_flaw { get; set; }
        public string char_player { get; set; }

        public string migou_lan_1 { get; set; }

        public string migou_lan_2 { get; set; }

        public Char_Attribute[] char_attributes;
        public Dictionary<string, int> char_attributes_feat { get; set; }
        public Dictionary<string, int> char_secondary_attributes { get; set; }

//ReSharper disable CSharpWarnings::CS1570
        /// <summary>
        /// TODO: assets and drawbacks need to be changed from a dictionaly to another List<object> as it turns outhtat you can have more than one occurance of some qualities ...  joy oh well that will have to happen some time soon</object> 
        /// 
        /// </summary>
// ReSharper restore CSharpWarnings::CS1570
        //public Dictionary<string, int> char_assets { get; set; }
        //public Dictionary<string, int> char_drawbacks { get; set; }
        
        public List<Quality_Data> char_drawbacks { get; set; } 
        public List<Quality_Data> char_assets { get; set; } 

        public List<Char_Skills> char_skills_list { get; set; }
        public int char_skill_points { get; set; }
        public int char_attribute_points { get; set; }
        public int char_cheat_points { get; set; }
        public bool char_creation_finailised { get; set; }

        public int xenomix_att_chosen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Character_Data()
        {
            char_attributes = new[]
            {
                new Char_Attribute(MainWindow.AGI, 0),
                new Char_Attribute(MainWindow.INT, 0),
                new Char_Attribute(MainWindow.PER, 0),
                new Char_Attribute(MainWindow.PRE, 0),
                new Char_Attribute(MainWindow.STR, 0),
                new Char_Attribute(MainWindow.TEN, 0)
            };

            char_secondary_attributes = new Dictionary<string, int>
            {
                {MainWindow.ACTIONS,0}, 
                {MainWindow.REFLEX,0 }, 
                {MainWindow.ORGONE,0 }, 
                {MainWindow.VITIALITY,0 }, 
                {MainWindow.MOVEMENT,0 }, 
                {MainWindow.DRAMA,0 }
            };
            //char_assets = new Dictionary<string, int>();
            //char_drawbacks = new Dictionary<string, int>();
            char_assets = new List<Quality_Data>();
            char_drawbacks = new List<Quality_Data>();
            char_skills_list = new List<Char_Skills>();
            char_creation_finailised = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Character_Data other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Character_Data other)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// this is one of the most ugly hacks i could come up with quickly to get the diag interaction working ... 
    /// with luck when i get time ill be able to convert it to a deligate/event structure... 
    /// that said even with it being as ugly as it is i have to say it bloody works 
    /// </summary>
    public static class Transporter
    {
        public static string transporter_string_1 { get; set; }
        public static string transporter_string_2 { get; set; }
        public static int transporter_int { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace Data_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        private Background_Data _background_data = new Background_Data();
        private Powers_Data _powers_data = new Powers_Data();
        private const string IN_FILE = "data.xml";
        private const string WORKING_DIR = "cutech_data";
        private readonly string _data_file_path;
        private readonly XmlSerializer _data_ser = new XmlSerializer(typeof(Background_Data));

        public MainWindow()
        {
            InitializeComponent();


            _data_file_path = string.Format(@"{0}{1}", create_path(), IN_FILE);
            if (!File.Exists(_data_file_path))
            {
                output_box.AppendText("no data file yet\nwe looked here : "+ _data_file_path);
            }
            else
            {
                output_box.AppendText(" this is where we load the file\n");
                load_bg_datafile(_data_file_path);
                output_box.AppendText("data file to be found at : " + _data_file_path + "\n" );
            }


            
        }

        /// <summary>
        /// TODO: need to change the load code to the same code as ussed in the character Manager and also add code for load and save of the power XML
        /// </summary>
        /// <returns></returns>
        private static string create_path()
        {
            string dir = string.Format(@"{0}\{1}\", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), WORKING_DIR);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }

        private void load_bg_datafile(string input_fh)
        {
            try
            {
                var input = new FileStream(input_fh, FileMode.Open);
                _background_data = (Background_Data)_data_ser.Deserialize(input);
                input.Close();
            }
            catch (Exception)
            {
                var create_file = new StreamWriter(input_fh);
                create_file.Close();
            }
            _background_data.sort();
            bg_display();
        }
 
        private void save_bg_data_file(string data_file_path)
        {
           _background_data.sort();
            var writer = new StreamWriter(data_file_path);
            _data_ser.Serialize(writer, _background_data);
            writer.Close();
        }

        #region menu items
        private void file_exit_menuitem_Click(object sender, RoutedEventArgs e)
        {
            save_bg_data_file(_data_file_path);
            Close();
        }
        #endregion

        #region test buttons
        private void clear_output_button_Click(object sender, RoutedEventArgs e)
        {
            output_box.Clear();
        }
 
        private void load_data_button_Click(object sender, RoutedEventArgs e)
        {
            load_bg_datafile(_data_file_path);
        }

        private void save_data_button_Click(object sender, RoutedEventArgs e)
        {

            save_bg_data_file(_data_file_path);
        }
        private void display_button_Click(object sender, RoutedEventArgs e)
        {
            bg_display();
        }
        #endregion

        #region utility
        /// <summary>
        /// 
        /// </summary>
        private void bg_display()
        {
            _background_data.sort();
            _background_data.display_list(new[]
            {
                race_text, 
                skills_text, 
                qualities_text, 
                virtues_text, 
                flaws_text, 
                faction_text
            }, true, false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void power_display()
        {
            _powers_data.sort();
            _powers_data.display_list(new[]
            {
                spell_text,
                psychic_text
            } ,true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ucase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            char[] output = input.ToCharArray();
            output[0] = char.ToUpper(output[0]);
            return new string(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skill_level"></param>
        /// <returns></returns>
        private int convert_skill_to_num(string skill_level)
        {
            if (string.IsNullOrEmpty(skill_level))
            {
                return -1;
            }
            switch (skill_level)
            {
                case "None":
                    return 0;
                case "Student":
                    return 1;
                case "Novice":
                    return 2;
                case "Adept":
                    return 3;
                case "Expert":
                    return 4;
                case "Master":
                    return 5;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text_box_text"></param>
        /// <returns></returns>
        private int int_from_txt(string text_box_text)
        {
            if (string.IsNullOrEmpty(text_box_text))
            {
                return 0;
            }
            if (text_box_text.Split(' ').Length > 1)
                text_box_text = text_box_text.Split(' ')[0];
            try
            {
                return Convert.ToInt16(text_box_text);
            }
            catch (FormatException)
            {

                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text_box_text"></param>
        /// <returns></returns>
        private string get_from_Text(IEnumerable<string> text_box_text)
        {
            // ucase the cirst letter of each word
            return text_box_text.Aggregate("", (current, t) => current + ucase(t));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="combo_box_text"></param>
        /// <returns></returns>
        private string get_from_combo(IList<string> combo_box_text)
        {
            // discard the first part of the string 
            if (combo_box_text.Count == 1)
            {
                return "";
            }
            else
            {
                string new_p = "";
                for (var index = 1; index < combo_box_text.Count; index++)
                {
                    new_p += ucase(combo_box_text[index]);
                }
                return new_p;
            }

        }
        #endregion

        #region background data
        private void add_race_button_Click(object sender, RoutedEventArgs e)
        {
            int issue = 0;
            string name = ucase(add_race_name_text.Text);

            if (name == "")
            {
                issue = 1;
                output_box.AppendText("there was an issue int he name field i really do need to add input validation \n");
            }
            if (issue == 0)
            {

                add_race_name_text.Clear();

                output_box.AppendText("new Race name is : " + name + "\n");
                _background_data.race(new Race_Data(name));
            }
            bg_display();
        }

        private void add_skill_button_Click(object sender, RoutedEventArgs e)
        {
            var name = ucase(skill_name_text.Text);

            var attribute = ucase(skill_attribute_combo.SelectedItem.ToString().Split(' ')[1]);

            skill_name_text.Clear();
            output_box.AppendText(" skill stuff \n name " + name + " assoicated Attribute " + attribute + "\n");
            _background_data.skill(new Skill_Data(name, attribute));
            bg_display();
        }

        private void add_quality_button_Click(object sender, RoutedEventArgs e)
        {
            var name = ucase(quality_name_text.Text);
            var value = quality_value_text.Text;
            var type = ucase(quality_type_combo.SelectedItem.ToString().Split(' ')[1]);

            quality_name_text.Clear();
            quality_value_text.Clear();

            output_box.AppendText("quality : " + name + " ( " + type + " ) " + value + " points\n");
            var return_str = _background_data.quality(new Quality_Data(name, value), type);
            output_box.AppendText("quality : " + return_str +"\n");
            bg_display();
        }

        private void add_virute_button_Click(object sender, RoutedEventArgs e)
        {
            int issue = 0;
            string name = ucase(virtue_name_text.Text);

            if (name == "")
            {
                issue = 1;
                output_box.AppendText("there was an issue int he name field i really do need to add input validation \n");
            }
            if (issue == 0)
            {

                virtue_name_text.Clear();

                output_box.AppendText("new virtue name is : " + name + "\n");
                _background_data.virtue(name);
            }
            bg_display();
        }

        private void add_flaw_button_Click(object sender, RoutedEventArgs e)
        {
            int issue = 0;
            string name = ucase(flaw_name_text.Text);

            if (name == "")
            {
                issue = 1;
                output_box.AppendText("there was an issue int he name field i really do need to add input validation \n");
            }
            if (issue == 0)
            {

                flaw_name_text.Clear();

                output_box.AppendText("new flaw name is : " + name + "\n");
                _background_data.flaw(name);
            }

            bg_display();
        }

        private void add_faction_button_Click(object sender, RoutedEventArgs e)
        {
            int issue = 0;
            string name = ucase(faction_name_text.Text);

            if (name == "")
            {
                issue = 1;
                output_box.AppendText("there was an issue int he name field i really do need to add input validation \n");
            }
            if (issue == 0)
            {

                faction_name_text.Clear();

                output_box.AppendText("new faction name is : " + name + "\n");
                _background_data.faction(name);
            }
            bg_display();
        }
        #endregion

        #region power Data
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_spell_button_Click(object sender, RoutedEventArgs e)
        {


            string type = get_from_combo(spell_type_combo.SelectedItem.ToString().Split(' '));
            
            string insan_diff = get_from_combo(spell_insanity_dif_combo.SelectedItem.ToString().Split(' '));

            string occult_level = get_from_combo(spell_occult_req_combo.SelectedItem.ToString().Split(' '));

            int occult_int = convert_skill_to_num(occult_level);

            string name = get_from_Text(spell_name_text.Text.Split(' '));
            string insan_con = get_from_combo(spell_insanity_conseqence_text.Text.Split(' '));
            
            string order = get_from_combo(spell_order_combo.SelectedItem.ToString().Split(' '));

            int order_int = 0;
            switch (order)
            {
                case "First Order":
                    order_int = 1;
                    break;
                case "Second Order":
                    order_int = 2;
                    break;
                case "Third Order":
                    order_int = 3;
                    break;
                default :
                    order_int = -1;
                    break;
            }
            
            int min_int = 0;
            int min_ten = 0;

            if (!string.IsNullOrEmpty(spell_int_req_text.Text))
            {
                 min_int = int_from_txt(spell_int_req_text.Text); 
            }
            if (!string.IsNullOrEmpty(spell_int_req_text.Text))
            {
                 min_ten = int_from_txt(spell_ten_req_text.Text); 
            }



            bool legal; 
            if (spell_legal_check.IsChecked == true)
            {
                legal = true;
            }
            else
            {
                legal = false;
            }
            
            output_box.AppendText("=========  new spell to be added soon initial data is  ==========\n");
            output_box.AppendText(string.Format("====  name           \t: {0}  \n", name ));
            output_box.AppendText(string.Format("====  type           \t: {0}  \n", type));
            output_box.AppendText(string.Format("====  order          \t: {0} \t{1} \n", order, order_int));
            output_box.AppendText(string.Format("====  insanity diff  \t: {0}  \n", insan_diff));
            output_box.AppendText(string.Format("====  insanity cons  \t: {0}  \n", insan_con));
            output_box.AppendText(string.Format("====  min int        \t: {0}  \n", min_int));
            output_box.AppendText(string.Format("====  min ten        \t: {0}  \n", min_ten));
            output_box.AppendText(string.Format("====  occult level   \t: {0}  \n", occult_level));
            output_box.AppendText(string.Format("====  legality       \t: {0}  \n", legal));
            output_box.AppendText("=================================================================\n");


            _powers_data.spell(new Spell_Data(name,type,order_int,insan_diff,insan_con,min_int,min_ten,occult_int,legal));


            power_display();

        }


        #endregion

        private void quality_value_text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Powers_Data
    {
        public List<Spell_Data> spell_list;
        public List<Psychic_Data> psychic_list; 


        /// <summary>
        /// 
        /// </summary>
        public Powers_Data()
        {
            spell_list = new List<Spell_Data>();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void sort()
        {
            spell_list.Sort();
            psychic_list.Sort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab_boxs"></param>
        /// <param name="clear"></param>
        public void display_list(TextBox[] tab_boxs, bool clear)
        {
            display_spell(tab_boxs[0], clear);
            display_psychic(tab_boxs[1], clear);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        /// <param name="clear"></param>
        private void display_psychic(TextBox box, bool clear)
        {
            if (clear)
            {
                box.Clear();
            }
            foreach (var item in psychic_list)
            {
                box.AppendText(item + "\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        /// <param name="clear"></param>
        private void display_spell(TextBox box, bool clear)
        {
            if (clear)
            {
                box.Clear();
            }
            foreach (var item in spell_list)
            {
                box.AppendText(item + "\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to_add"></param>
        public void spell(Spell_Data to_add)
        {
            spell_list.Add(to_add);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class Background_Data
    {
        // ReSharper disable FieldCanBeMadeReadOnly.Local

        public List<Race_Data> race_list;
        public List<Skill_Data> skill_list;
        public List<Quality_Data> asset_list;
        public List<Quality_Data> drawback_list;
        public List<string> virtue_list;
        public List<string> flaw_list;
        public List<string> faction_list; 
        public int attribute_points { get; set; }
        public int skill_points { get; set; }
        public int cheat_points { get; set; }

        // ReSharper restore FieldCanBeMadeReadOnly.Local        
        public Background_Data()
        {
            race_list = new List<Race_Data>();
            skill_list = new List<Skill_Data>();
            asset_list = new List<Quality_Data>();
            drawback_list = new List<Quality_Data>();
            virtue_list = new List<string>();
            flaw_list = new List<string>();
            faction_list = new List<string>();
            attribute_points = 36;
            skill_points = 20;
            cheat_points = 6;


        }

        public void sort()
        {
            race_list.Sort();
            skill_list.Sort();
            asset_list.Sort();
            drawback_list.Sort();
            virtue_list.Sort();
            flaw_list.Sort();
            faction_list.Sort();
        }

        public void virtue(string to_add)
        {
            virtue_list.Add(to_add);
        }

        public void faction(string to_add)
        {
            faction_list.Add(to_add);
        }

        public void flaw(string to_add)
        {
            flaw_list.Add(to_add);
        }

        public void race(Race_Data to_add)
        {
            race_list.Add(to_add);
        }

        public void skill(Skill_Data to_add)
        {
            skill_list.Add(to_add);
        }

        public string quality(Quality_Data to_add, string type)
        {
            switch (type)
            {
                case "Asset":
                    asset_list.Add(to_add);
                    return "Asset added";
                    break;
                case "Drawback":
                    drawback_list.Add(to_add);
                    return "Drawback added";
                    break;
            }
            return "nothing added";
        }

        internal void display_race(TextBox box, bool clear)
        {
            if (clear)
            {
                box.Clear();
            }
            foreach (var item in race_list)
            {
                box.AppendText(item.race_name + "\n");
            }
        }

        internal void display_skill(TextBox box, bool clear)
        {
            if (clear)
            {
                box.Clear();
            }
            foreach (var item in skill_list)
            {
                box.AppendText(item.skill_name + " \t( " + item.skill_attribute + " )\n");
            }
        }

        internal void display_quaity(TextBox box, bool clear)
        {
            if (clear)
            {
                box.Clear();
            }
            box.AppendText("===== Assets =====\n");
            foreach (var item in asset_list)
            {
                box.AppendText(item.quality_name + " ( " + item.quality_value + " )\n");
            }
            box.AppendText("===== Drawbacks =====\n");
            foreach (var item in drawback_list)
            {
                box.AppendText(item.quality_name + " ( " + item.quality_value + " )\n");
            }
        }

        internal void display_virtue(TextBox box, bool clear)
        {
            if (clear)
            {
                box.Clear();
            }
            foreach (var item in virtue_list)
            {
                box.AppendText(item + "\n");
            }
        }

        internal void display_flaw(TextBox box, bool clear)
        {
            if (clear)
            {
                box.Clear();
            }
            foreach (var item in flaw_list)
            {
                box.AppendText(item + "\n");
            }
        }

        internal void display_faction(TextBox box, bool clear)
        {
            if (clear)
            {
                box.Clear();
            }
            foreach (var item in faction_list)
            {
                box.AppendText(item + "\n");
            }
        }

        public void display_list(TextBox[] tab_boxs, bool clear, bool manager)
        {
            display_race(tab_boxs[0], clear);
            display_skill(tab_boxs[1], clear);
            display_quaity(tab_boxs[2], clear);
            display_virtue(tab_boxs[3], clear);
            display_flaw(tab_boxs[4], clear);
            display_faction(tab_boxs[5], clear);
            if (manager)
            {
                tab_boxs[0].AppendText("character attribute points : "+ attribute_points + "\n" );
                tab_boxs[0].AppendText("character skill points : " + skill_points + "\n");
                tab_boxs[0].AppendText("character cheat points : " + cheat_points + "\n");
            }

        }
    }
}

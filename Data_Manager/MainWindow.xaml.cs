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
                output_box.AppendText("no data file yet\n");
            }
            else
            {
                output_box.AppendText(" this is where we load the file\n");
                load_bg_datafile(_data_file_path);
            }


            output_box.AppendText("data file to be found at : " + _data_file_path + "\n" );
        }

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
            display();
        }
 
        private void save_bg_data_file(string data_file_path)
        {
           _background_data.sort();
            var writer = new StreamWriter(data_file_path);
            _data_ser.Serialize(writer, _background_data);
            writer.Close();
        }  

        private void file_exit_menuitem_Click(object sender, RoutedEventArgs e)
        {
            save_bg_data_file(_data_file_path);
            Close();
        }

        private void clear_output_button_Click(object sender, RoutedEventArgs e)
        {
            output_box.Clear();
        }

        private void display_button_Click(object sender, RoutedEventArgs e)
        {
            display();
        }

        private void display()
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

        private void load_data_button_Click(object sender, RoutedEventArgs e)
        {
            load_bg_datafile(_data_file_path);
        }

        private void save_data_button_Click(object sender, RoutedEventArgs e)
        {

            save_bg_data_file(_data_file_path);
        }

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
            display();
        }

        private void add_skill_button_Click(object sender, RoutedEventArgs e)
        {
            var name = ucase(skill_name_text.Text);

            var attribute = ucase(skill_attribute_combo.SelectedItem.ToString().Split(' ')[1]);

            skill_name_text.Clear();
            output_box.AppendText(" skill stuff \n name " + name + " assoicated Attribute " + attribute + "\n");
            _background_data.skill(new Skill_Data(name, attribute));
            display();
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
            display();
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
            display();
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

            display();
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
            display();
        }
    }

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

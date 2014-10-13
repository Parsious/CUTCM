using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CUTCM
{
    /// <summary>
    /// Interaction logic for Specs.xaml
    /// 
    /// 
    /// </summary>
    public partial class Specs : Window
    {
        public Specs(IEnumerable<Char_Skills> combo_items, string skill, bool creation)
        {
            
            InitializeComponent();
            if (creation) spec_2_radio.IsEnabled = false;
            foreach (var item in combo_items)
            {
                spec_base_skill_combo.Items.Add (item.skill_name);
                if (skill != null)
                {
                    if (spec_base_skill_combo.Items.Contains(skill))
                    {
                        spec_base_skill_combo.SelectedItem = skill;
                    }
                }
            }
            
        }

        private void specs_ok_button_Click(object sender, RoutedEventArgs e)
        {
            if (spec_base_skill_combo.SelectedIndex != -1 && !string.IsNullOrEmpty(spec_name_text.Text))
            {
                Transporter.transporter_string_1 = spec_base_skill_combo.SelectedItem.ToString();
                Transporter.transporter_string_2 = spec_name_text.Text;
                Transporter.transporter_int = spec_1_radio.IsChecked == true ? 1 : 2;
                DialogResult = true;
            }
        }

       

        private void spec_2_radio_Checked(object sender, RoutedEventArgs e)
        {
            //spec_1_radio.IsChecked = false;
        }

        private void spec_1_radio_Checked(object sender, RoutedEventArgs e)
        {
            //spec_2_radio.IsChecked = false;
        }

        
    }
}

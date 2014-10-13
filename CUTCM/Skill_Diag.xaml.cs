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
    /// Interaction logic for Skill_Diag.xaml
    /// </summary>
    public partial class Skill_Diag : Window
    {

        public Skill_Diag(string text)
        {
            InitializeComponent();
            skill_diag_text_block.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(skill_diag_info_text.Text))
            {
                skill_diag_text_block.Text = skill_diag_text_block.Text +"\noh come one  need to add something here -- numpty ";
            }
            else
            {
                //skill_diag_text_block.Text = skill_diag_text_block + "\ndid we make it here ";
                Transporter.transporter_string_1 = skill_diag_info_text.Text;
                this.DialogResult = true;
            }
        }
    }
}

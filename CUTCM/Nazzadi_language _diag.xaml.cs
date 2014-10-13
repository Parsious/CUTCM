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
    /// Interaction logic for Nazzadi_language__diag.xaml
    /// </summary>
    public partial class Nazzadi_Language_Diag : Window
    {
        public Nazzadi_Language_Diag()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (race_lang_combo.SelectedIndex != -1 && !string.IsNullOrEmpty(race_lang_combo.SelectedItem.ToString()))
            {
                Transporter.transporter_string_1 = race_lang_combo.SelectedItem.ToString();
                DialogResult = true;
            }
        }
    }
}

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
    /// Interaction logic for Xenomix_Race_Diag.xaml
    /// </summary>
    public partial class Xenomix_Race_Diag : Window
    {
        public Xenomix_Race_Diag()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (race_att_combo.SelectedIndex != -1 && !string.IsNullOrEmpty(race_att_combo.SelectedItem.ToString()))
            {
                Transporter.transporter_string_1 = race_att_combo.SelectedItem.ToString();
                DialogResult = true;
            }
        }
    }
}

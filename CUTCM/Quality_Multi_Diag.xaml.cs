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
    /// Interaction logic for Quality_Multi_Diag.xaml
    /// </summary>
    public partial class Quality_Multi_Diag 
    {
        private string _quality_name;
        private string _quality_value;
        private string _quality_type;

        public Quality_Multi_Diag()
        {
            InitializeComponent();
        }

        public Quality_Multi_Diag(string name, string value, string type )
        {
            InitializeComponent();
            _quality_name =  name;
            quality_name.AppendText(name); 
            this._quality_value = value;
            _quality_type = type;

            int max = int.Parse((value[value.Length - 1])+"");
            switch (max)
            {
                
                case 2 :
                    radio_2.IsEnabled = true;
                    break;
                case 3 :
                    radio_2.IsEnabled = true;
                    radio_3.IsEnabled = true;
                    break;
                case 4 :
                   radio_2.IsEnabled = true;
                    radio_3.IsEnabled = true;
                    radio_4.IsEnabled = true;
                    break;
                default:

                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Transporter.transporter_string_1 = _quality_name;
            Transporter.transporter_string_2 = _quality_type;
            if (radio_4.IsChecked == true)
            {
                Transporter.transporter_int = 4;
            }
            else if (radio_3.IsChecked == true)
            {
                Transporter.transporter_int = 3;
            }
            else if (radio_2.IsChecked == true)
            {
                Transporter.transporter_int = 2;
            }
            else if (radio_1.IsChecked == true)
            {
                Transporter.transporter_int = 1;
            }

            DialogResult = true;
        }

       
    }
}

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

namespace Calculator
{
    /// <summary>
    /// PrecisionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrecisionWindow : Window
    {
        public PrecisionWindow()
        {
            InitializeComponent();
        }

        private void Precision_Control(object sender, RoutedEventArgs e)
        {
            MainWindow.precision = Convert.ToInt32(precisionControl.Text);
            this.Close();
        }

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isNumberic(e.Text))
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }

        private static bool isNumberic(string _string)
        {
            if (string.IsNullOrEmpty(_string))
                return false;
            foreach (char c in _string)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool flagOp;
        private int cntNum;
        public static int precision;
        private bool flagNum;
        private bool flagPoint;
        private bool firstClickOp;
        private int cntBracket;
        private bool flagRightBracket;
        private bool flagLeftBracket;

        public MainWindow()
        {
            InitializeComponent();
            for(int i=0;i<selectGrid.Children.Count;i++)
            {
                if(selectGrid.Children[i] is Button)
                {
                    (selectGrid.Children[i] as Button).Click += new RoutedEventHandler(Button_Click);
                }
            }
            cntNum = 0;
            precision = 0;
            cntBracket = 0;
            flagOp = false;
            flagNum = false;
            flagPoint = false;
            firstClickOp = false;
            flagLeftBracket = false;
            flagRightBracket = false;
        }

        private int CheckCharacter(String check)
        {
            if (check == "+" || check == "*" || check == "/" || check == "-")
            {
                return 0;
            }
            else if (check == "0" || check == "1" || check == "2" ||
                    check == "3" || check == "4" || check == "5" ||
                    check == "6" || check == "7" || check == "8" || check == "9")
            {
                return 1;
            }
            else if (check == "(")
            {
                return 2;
            }
            else if (check == ")")
            {
                return 3;
            }
            else if(check == "=")
            {
                return 4;
            }
            else if(check == "C")
            {
                return 5;
            }
            else if(check==".")
            {
                return 6;
            }
            else if(check=="<—")
            {
                return 7;
            }
            else 
            {
                return -1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int recCheck = CheckCharacter(btn.Content.ToString());

            if(recCheck == 0)
            {
                if (!flagOp)
                {
                    if (firstClickOp)
                    {
                        String tmp = outputLabel.Content.ToString();
                        tmp += "  ";
                        outputLabel.Content = tmp;
                    }
                    if (flagRightBracket)
                    {
                        flagRightBracket = false;
                        outputLabel.Content += ("" + btn.Content);
                    }
                    else if (flagLeftBracket)
                    {
                        flagLeftBracket = false;
                        outputLabel.Content += ("  " + mainLabel.Content + "  " + btn.Content);
                    }
                    else
                        outputLabel.Content += (mainLabel.Content + "  " + btn.Content);
                    flagOp = true;
                    firstClickOp = true;
                }
                else
                {
                    String tmp = outputLabel.Content.ToString();
                    tmp = tmp.Remove(tmp.Length - 1, 1);
                    tmp += btn.Content.ToString();
                    outputLabel.Content = tmp;
                }
                flagNum = false;
                firstClickOp = true;
            }
            else if(recCheck == 1)
            {
                String tmpNum = btn.Content.ToString();
                if (!flagNum)
                {
                    mainLabel.Content = tmpNum;
                    flagNum = true;
                    flagPoint = false;
                }
                else
                {
                    if (Convert.ToDouble(mainLabel.Content) == 0 && !flagPoint)
                        mainLabel.Content = tmpNum;
                    else
                        mainLabel.Content += tmpNum;
                }
                flagOp = false;
            }
            else if(recCheck == 2)
            {
                flagOp = false;
                flagLeftBracket = true;
                outputLabel.Content += ("  " + btn.Content);
                mainLabel.Content = "0";
                firstClickOp = false;
                cntBracket++;
            }
            else if(recCheck == 3)
            {
                if(cntBracket > 0 && flagNum)
                {
                    flagOp = false;
                    flagRightBracket = true;
                    outputLabel.Content += ("  " + mainLabel.Content + "  " + btn.Content);
                    mainLabel.Content = "0";
                    cntBracket--;
                }
            }
            else if(recCheck == 4)
            {
                String exception = "";
                if (outputLabel.Content != null)
                    exception = outputLabel.Content.ToString();
                if (!flagRightBracket)
                    exception += ("  " + mainLabel.Content.ToString());
                while(cntBracket > 0)
                {
                    exception += ")";
                    cntBracket--;
                }
                String answer = Calculate.startCalculate(exception, precision);
                mainLabel.Content = answer;
                outputLabel.Content = "";
                cntNum = 0;
                cntBracket = 0;
                flagOp = false;
                flagPoint = false;
                flagNum = false;
                firstClickOp = false;
                flagLeftBracket = false;
                flagRightBracket = false;
            }
            else if(recCheck == 5)
            {
                outputLabel.Content = "";
                mainLabel.Content = "0";
                cntNum = 0;
                cntBracket = 0;
                flagOp = false;
                flagPoint = false;
                flagNum = false;
                firstClickOp = false;
            }
            else if(recCheck == 6)
            {
                if(!flagPoint)
                {
                    mainLabel.Content += ".";
                    flagPoint = true;
                    flagNum = true;
                }
            }
            else if(recCheck == 7)
            {
                String tmp = mainLabel.Content.ToString();
                if(tmp.Length == 1)
                {
                    mainLabel.Content = "0";
                    flagNum = false;
                }
                else if (tmp[tmp.Length - 1].Equals('.'))
                {
                    tmp = tmp.Remove(tmp.Length - 1, 1);
                    mainLabel.Content = tmp;
                    flagPoint = false;
                }
                else
                {
                    tmp = tmp.Remove(tmp.Length - 1, 1);
                    mainLabel.Content = tmp;
                }
            }
            else
            {
                MessageBox.Show("出现异常！");
            }
        }

        private void menu_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            menu.Visibility = Visibility.Visible;
        }

        private void menu_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            menu.Visibility = Visibility.Collapsed;
        }

        private void menu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            menu.Visibility = Visibility.Visible;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                if (menu.Visibility != Visibility.Visible) menu.Visibility = Visibility.Visible;
            }
        }

        private void Precision_Click(object sender, RoutedEventArgs e)
        {
            PrecisionWindow newPrecisionWindow = new PrecisionWindow();
            newPrecisionWindow.ShowDialog();
        }
        
    }
}

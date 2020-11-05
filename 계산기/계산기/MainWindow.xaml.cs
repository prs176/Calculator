using System.Windows;
using System.Windows.Controls;

namespace 계산기
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool newButton;
        private bool newHistoty;
        private double result;
        private string operation;
        private bool from;

        public MainWindow()
        {
            InitializeComponent();
            NumberBox.Text = "0";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (NumberBox.Text.Length > 11 && !newButton && NumberBox.Text[NumberBox.Text.Length - 1] != '.')
            {
                return;
            }

            Button btn = sender as Button;

            if (!newButton && NumberBox.Text != "0" && string.Compare(NumberBox.Text, "생일축하합니다") != 0)
            {
                NumberBox.Text += btn.Content.ToString();
            }
            else
            {
                NumberBox.Text = btn.Content.ToString();
                newButton = false;
            }
            if (newHistoty)
            {
                HistoryBox.Text = "";
                newHistoty = false;
            }

            Change_FontSize();
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.Compare(NumberBox.Text, "생일축하합니다") == 0)
            {
                return;
            }

            if (NumberBox.Text.Length > 11)
            {
                return;
            }

            if (double.TryParse(NumberBox.Text + ".", out _))
            {
                NumberBox.Text += ".";
            }
        }

        private void TransformButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.Compare(NumberBox.Text, "생일축하합니다") == 0)
            {
                return;
            }

            NumberBox.Text = (-double.Parse(NumberBox.Text)).ToString();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NumberBox.Text = "0";
            HistoryBox.Text = "";
            operation = null;
            result = 0;
            Change_FontSize();
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            NumberBox.Text = "0";
            Change_FontSize();
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.Compare(NumberBox.Text, "생일축하합니다") == 0)
            {
                return;
            }

            if (newButton)
            {
                return;
            }

            if (NumberBox.Text.Length > 1)
            {
                NumberBox.Text = NumberBox.Text.Remove(NumberBox.Text.Length - 1, 1);
            }
            else
            {
                NumberBox.Text = "0";
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.Compare(NumberBox.Text, "생일축하합니다") == 0)
            {
                return;
            }

            Button btn = sender as Button;
            from = true;

            if (newHistoty)
            {
                HistoryBox.Text = NumberBox.Text;
                HistoryBox.Text += btn.Content.ToString();
                newHistoty = false;
            }
            else if (!newButton)
            {
                HistoryBox.Text += NumberBox.Text;
                HistoryBox.Text += btn.Content.ToString();
            }
            else
            {
                if(HistoryBox.Text.Length > 0)
                {
                    HistoryBox.Text = HistoryBox.Text.Remove(HistoryBox.Text.Length - 1, 1);
                }
                HistoryBox.Text += btn.Content.ToString();
            }

            if (!newButton && operation != null)
            {
                ResultButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }

            if (string.Compare(NumberBox.Text, "생일축하합니다") == 0)
            {
                return;
            }

            operation = btn.Content.ToString();
            result = double.Parse(NumberBox.Text);

            from = false;
            newButton = true;
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.Compare(NumberBox.Text, "생일축하합니다") == 0)
            {
                return;
            }

            if (!from)
            {
                HistoryBox.Text += NumberBox.Text;
                HistoryBox.Text += "=";
                newHistoty = true;
            }

            if (operation == "+")
            {
                NumberBox.Text = (result + double.Parse(NumberBox.Text)).ToString();
            }
            else if (operation == "-")
            {
                NumberBox.Text = (result - double.Parse(NumberBox.Text)).ToString();
            }
            else if (operation == "×")
            {
                NumberBox.Text = (result * double.Parse(NumberBox.Text)).ToString();
            }
            else if (operation == "÷")
            {
                if(NumberBox.Text == "0")
                {
                    NumberBox.Text = "생일축하합니다";
                    result = 0;
                    HistoryBox.Text = "";
                    operation = null;
                    newButton = true;
                    newHistoty = true;
                    Change_FontSize();
                    return;
                }
                else
                {
                    NumberBox.Text = (result / double.Parse(NumberBox.Text)).ToString();
                }
            }

            if (double.Parse(NumberBox.Text) > 1.7E+308 || double.Parse(NumberBox.Text) < -1.7E+308)
            {
                NumberBox.Text = "생일축하합니다";
                newHistoty = true;
                result = 0;
                HistoryBox.Text = "";
            }

            operation = null;
            newButton = true;
            Change_FontSize();
        }

        private void Change_FontSize()
        {
            if (NumberBox.Text.Length > 15)
            {
                NumberBox.FontSize = 30;
            }
            else
            {
                NumberBox.FontSize = 35;
            }
        }
    }
}

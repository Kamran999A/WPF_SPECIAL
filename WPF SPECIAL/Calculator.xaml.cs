using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace WPF_SPECIAL
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        Double Result = 0;
        String Operation = "";
        bool enterValue = false;
        public Calculator()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        public void timer_Tick(object sender, EventArgs e)
        {
            timeLabel.Content = DateTime.Now.ToShortTimeString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void clickButton(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if ((lowerBlock.Text == "0") || (enterValue))
            {
                lowerBlock.Text = "";
                enterValue = false;
            }

            if ((textTrimmer(b.ToolTip.ToString()) == "."))
            {
                if (!lowerBlock.Text.Contains("."))
                {
                    lowerBlock.Text += textTrimmer(b.ToolTip.ToString());
                }
            }
            else
            {
                lowerBlock.Text += textTrimmer(b.ToolTip.ToString());
            }

        }
        private string textTrimmer(String iconToolTipValue)
        {
            return iconToolTipValue[iconToolTipValue.Length - 1].ToString();
        }

        private void operatorClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
           

            if (Result != 0)
            {
                BtnEquals_Click(this, null);
                enterValue = true;
                Operation = textTrimmer(b.ToolTip.ToString());
                upperBlock.Text = System.Convert.ToString(Result) + "   " + Operation;
            }
            else
            {
                Operation = textTrimmer(b.ToolTip.ToString());
                Result = (Double.Parse(lowerBlock.Text));
                lowerBlock.Text = "";
                upperBlock.Text = System.Convert.ToString(Result) + "   " + Operation;
            }
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            switch (Operation)
            {
                case "+":
                    lowerBlock.Text = (Result + Double.Parse(lowerBlock.Text)).ToString();
                    break;
                case "-":
                    lowerBlock.Text = (Result - Double.Parse(lowerBlock.Text)).ToString();
                    break;
                case "*":
                    lowerBlock.Text = (Result * Double.Parse(lowerBlock.Text)).ToString();
                    break;
                case "/":
                    lowerBlock.Text = (Result / Double.Parse(lowerBlock.Text)).ToString();
                    break;
            }
            Result = Double.Parse(lowerBlock.Text);
            Operation = "";
            upperBlock.Text = "";
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            lowerBlock.Text = "";
            upperBlock.Text = "";
            Operation = "";
            enterValue = false;
            Result = 0;
            lowerBlock.Text = "0";
        }

        private void BtnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (lowerBlock.Text.Length > 0)
            {
                lowerBlock.Text = lowerBlock.Text.Remove(lowerBlock.Text.Length - 1, 1);
            }

            if (lowerBlock.Text == "")
            {
                lowerBlock.Text = "0";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    private void ButtonPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (lowerBlock.Text == "0")
                return;

            var newValue = lowerBlock.Text.StartsWith("-") ?
                lowerBlock.Text.Remove(0, 1) :
                lowerBlock.Text.Insert(0, "-");

            lowerBlock.Text = newValue.ToString(CultureInfo.InvariantCulture);
        }

    }
}

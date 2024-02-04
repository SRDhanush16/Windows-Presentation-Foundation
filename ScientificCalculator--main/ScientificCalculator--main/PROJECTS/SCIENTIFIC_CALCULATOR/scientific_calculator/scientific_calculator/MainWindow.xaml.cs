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

namespace scientific_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // public partial class MainWindow : Window
    //{
    // public MainWindow()
    //{
    //  InitializeComponent();
    //}
    //}


    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Division(lastNumber, newNumber);
                        break;

                    case SelectedOperator.Exponent:
                        result = SimpleMath.Expo(lastNumber);
                        break;
                    case SelectedOperator.Naturallog:
                        result = SimpleMath.Naturallog(lastNumber);
                        break;
                    case SelectedOperator.Logbase10:
                        result = SimpleMath.Logbase10(lastNumber);
                        break;
                    case SelectedOperator.Power:
                        result = SimpleMath.Xpown(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Squareroot:
                        result = SimpleMath.Squareroot(lastNumber);
                        break;
                }

                resultLabel.Content = result.ToString();

            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber / 100;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multiplicationButton)
            { selectedOperator = SelectedOperator.Multiplication; }
            if (sender == divisionButton)
            { selectedOperator = SelectedOperator.Division; }
            if (sender == plusButton)
            { selectedOperator = SelectedOperator.Addition; }
            if (sender == minusButton)
            { selectedOperator = SelectedOperator.Subtraction; }

            if (sender == exButton)
            { selectedOperator = SelectedOperator.Exponent; }
            if (sender == lnButton)
            { selectedOperator = SelectedOperator.Naturallog; }
            if (sender == log10Button)
            { selectedOperator = SelectedOperator.Logbase10; }
            if (sender == xnButton)
            { selectedOperator = SelectedOperator.Power; }
            if (sender == sqrtButton)
            { selectedOperator = SelectedOperator.Squareroot; }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {

            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {

            if (resultLabel.Content.ToString().Contains("."))
            {
                // do nothing
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        public enum SelectedOperator
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Exponent,
            Naturallog,
            Logbase10,
            Power,
            Squareroot


        }

        public class SimpleMath
        {
            public static double Add(double n1, double n2)
            {
                return n1 + n2;
            }

            public static double Subtraction(double n1, double n2)
            {
                return n1 - n2;
            }

            public static double Multiply(double n1, double n2)
            {
                return n1 * n2;
            }

            public static double Division(double n1, double n2)
            {
                if (n2 == 0)
                {
                    MessageBox.Show("Division by 0 is not supported", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }

                return n1 / n2;
            }

            public static double Expo(double n1)
            {
                return Math.Exp(n1);
            }
            public static double Naturallog(double n1)
            {
                return Math.Log(n1);
            }
            public static double Logbase10(double n1)
            {
                return Math.Log10(n1);

            }

            public static double Xpown(double n1, double n2)
            {
                return Math.Pow(n1, n2);
            }
            public static double Squareroot(double n1)
            {
                return Math.Sqrt(n1);
            }


        }
    }


}

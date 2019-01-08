using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Operation _selectedOperation;

        private Operation selectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                _selectedOperation = value;
                UpdateAdditionalDisplay();
            }
        }


        private string _mainDisplayString = string.Empty;

        private string mainDisplayString
        {
            get => _mainDisplayString;
            set
            {
                _mainDisplayString = value;
                UpdateMainDisplay();
            }
        }

        private string _additionalDisplayString = string.Empty;

        private string additionalDisplayString
        {
            get => _additionalDisplayString;
            set
            {
                _additionalDisplayString = value;
                UpdateAdditionalDisplay();
            }
        }

        private double? _bufferNumber;
        private double? bufferNumber
        {
            get
            {
                return _bufferNumber;
            }
            set
            {
                _bufferNumber = value;
                additionalDisplayString = _bufferNumber.ToString();
            }
        }

        private bool waitingForAnyNumericInput = true;
        private double currentNumber => double.TryParse(mainDisplayString, out double res) ? res : 0;

        public string MainDisplayText
        {
            get
            {
                return string.IsNullOrWhiteSpace(mainDisplayString) ? "0" : mainDisplayString;
            }
        }

        public string AdditionalDisplayText
        {
            get
            {
                var builder = new StringBuilder();
                var number = string.IsNullOrWhiteSpace(additionalDisplayString) ? "0" : additionalDisplayString;
                char oper = ' ';
                switch (selectedOperation)
                {
                    case Operation.Plus:
                        oper = '+';
                        break;
                    case Operation.Minus:
                        oper = '-';
                        break;
                    case Operation.Divide:
                        oper = '/';
                        break;
                    case Operation.Mult:
                        oper = '*';
                        break;
                    default:
                        break;
                }

                if (oper != ' ')
                    builder.Append($" {oper} ");

                builder.Append(number);

                return builder.ToString();
            }
        }

        private void UpdateMainDisplay()
        {
            MainDisplay.Text = MainDisplayText;
        }

        private void UpdateAdditionalDisplay()
        {
            additionalDisplay.Text = AdditionalDisplayText;
        }

        private void AddText(string str)
        {
            if (str == "," && mainDisplayString.Contains(','))
                return;
            mainDisplayString += str;
            waitingForAnyNumericInput = false;
        }

        private void ShiftNumbers()
        {
            if (!waitingForAnyNumericInput && bufferNumber.HasValue)
            {
                switch (selectedOperation)
                {
                    case Operation.Plus:
                        bufferNumber = bufferNumber + currentNumber;
                        break;
                    case Operation.Minus:
                        bufferNumber = bufferNumber - currentNumber;
                        break;
                    case Operation.Divide:
                        bufferNumber = bufferNumber / currentNumber;
                        break;
                    case Operation.Mult:
                        bufferNumber = bufferNumber * currentNumber;
                        break;
                    case Operation.None:
                        bufferNumber = currentNumber;
                        break;
                    default:
                        break;
                }
                mainDisplayString = string.Empty;
                waitingForAnyNumericInput = true;
            }

            if (!bufferNumber.HasValue)
            {
                bufferNumber = currentNumber;
                mainDisplayString = string.Empty;
                waitingForAnyNumericInput = true;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddText("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddText("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddText("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddText("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddText("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddText("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddText("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddText("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddText("9");
        }

        private void button0_Click(object sender, EventArgs e)
        {
            AddText("0");
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            ShiftNumbers();
            selectedOperation = Operation.None;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            selectedOperation = Operation.Plus;
            ShiftNumbers();
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            selectedOperation = Operation.Minus;
            ShiftNumbers();
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            selectedOperation = Operation.Mult;
            ShiftNumbers();
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            bufferNumber = null;
            selectedOperation = Operation.None;
            mainDisplayString = string.Empty;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (mainDisplayString.Length > 0)
                mainDisplayString = mainDisplayString.Substring(0, mainDisplayString.Length - 1);
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            selectedOperation = Operation.Divide;
            ShiftNumbers();
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            mainDisplayString = string.Empty;
        }

        private void buttonComma_Click(object sender, EventArgs e)
        {
            AddText(",");
        }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Calculator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _memoryValue, _value, _value2, _operation;
        private OperatorType _operator;
        private ICommand _sampleCommand;
        private ICommand _operatorsCommand;
        private ICommand _summaryCommand;
        private ICommand _operationsCommand;
        private ICommand _memoryCommand;


        public OperatorType Operator
        {
            get { return _operator; }
            set
            {
                _operator = value;
                OnPropertyChanged();
                OnPropertyChanged("DisplayValue");
            }
        }

        public string DisplayValue
        {
            get
            {
                string newValue;
                if (string.IsNullOrEmpty(Value2))
                {
                    newValue = Value1;
                }
                else
                {
                    newValue = Value2;
                }
                if (string.IsNullOrEmpty(newValue))
                {
                    return "0";
                }
                return newValue;
            }
            set
            {
                DisplayValue = value;
                OnPropertyChanged();
            }
        }


        public string Value1
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
                OnPropertyChanged("DisplayValue");
            }
        }
        public string Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                OnPropertyChanged();
                OnPropertyChanged("DisplayValue");
            }
        }

        public string Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                OnPropertyChanged();
            }
        }

        public string MemoryValue
        {
            get { return _memoryValue; }
            set
            {
                _memoryValue = value;
            }
        }
        public ICommand SampleCommand
        {
            get { return _sampleCommand; }
            set
            {
                _sampleCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand OperatorsCommand
        {
            get { return _operatorsCommand; }
            set
            {
                _operatorsCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand SummaryCommand
        {
            get { return _summaryCommand; }
            set
            {
                _summaryCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand OperationsCommand
        {
            get { return _operationsCommand; }
            set
            {
                _operationsCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand MemoryCommand
        {
            get { return _memoryCommand; }
            set
            {
                _memoryCommand = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            SampleCommand = new DelegateCommand<string>(SampleCommandExecute);
            OperatorsCommand = new DelegateCommand<string>(OperatorsCommandExecute);
            SummaryCommand = new DelegateCommand(SummaryCommandExecute);
            OperationsCommand = new DelegateCommand<string>(OperationsCommandExecute);
            MemoryCommand = new DelegateCommand<string>(MemoryCommandExecute);
            MemoryValue = "0";
        }
        private string BackSpace(string value)
        {
            if (value.Length > 1)
            {
                value = value.Remove(value.Length - 1);
            }
            else
            {
                value = "0";
            }
            return value;
        }
        private void OperationsCommandExecute(string obj)
        {
            switch (obj)
            {
                case "C":
                    Value1 = null;
                    Value2 = null;
                    Operator = OperatorType.NONE;
                    break;
                case "CE":
                    if (Value2 != null)
                    {
                        Value2 = null;
                    }else
                    {
                        Value1 = null;
                    }
                    break;
                case "back":
                    if (Value2 != null)
                    {
                        Value2 = BackSpace(Value2);
                    } else if (Value1 != null)
                    {
                        Value1 = BackSpace(Value1);
                    }
                    break;
                case "%":
                    if (Value2 != null) {
                        float number = float.Parse(Value2);
                        number = number * float.Parse(Value1) / 100;
                        Value2 = number.ToString();
                    }
                    break;
                case "1/x":
                    if (Value2 != null)
                    {
                        float fraction;
                        fraction = 1 / (float.Parse(Value2));
                        Value2 = fraction.ToString();
                    }
                    break;
                case "squareRoot":
                    if (Value2 != null)
                    {
                        Value2 = SquareRootNumberString(Value2);
                    }else if(Value1 !=null)
                    {
                        Value1 = SquareRootNumberString(Value1);
                    }
                    break;
                case "+/-":                 
                    if (Value2 != null)
                    {
                        Value2 = ReverseNumberString(Value2);
                    }
                    else if (Value2 != null)
                    {
                        Value1 = ReverseNumberString(Value1);
                    }

                    break;
                case ",":
                    if (Value2 == null && Operator != OperatorType.NONE)
                    {
                        Value1 = AddOnlyOneComma(Value1);
                    }
                    else
                    {
                        Value2 = AddOnlyOneComma(Value2);
                    }
                    break;
            }
        }
        private string SquareRootNumberString(string value)
        {
            double number = double.Parse(value);
            number = Math.Sqrt(number);
            return number.ToString();
        }

        private string ReverseNumberString(string value)
        {
            if (value == null)
            {
                return "0";
            }
            float number = float.Parse(value);
            number *= -1;
            return number.ToString();
        }

        private string AddOnlyOneComma(string value)
        {
            if (value == null)
            {
                return "0,";
            }
            if (value.Contains(",") == false)
            {
                value += ",";
            }
            return value;
        }


        public void SummaryCommandExecute()
        {
            if (string.IsNullOrEmpty(Value2) || string.IsNullOrEmpty(Value1))
            {
                return;
            }
            float floatValue1 = float.Parse(Value1);
            float floatValue2 = float.Parse(Value2);
            float summary = 0.0f;
            if (Operator == OperatorType.ADD)
            {
                summary = floatValue1 + floatValue2;
            }
            else if (Operator == OperatorType.SUB)
            {
                summary = floatValue1 - floatValue2;
            }
            else if (Operator == OperatorType.MUL)
            {
                summary = floatValue1 * floatValue2;
            }
            else if(Operator == OperatorType.DIV)
            {
                if (floatValue2 != 0)
                { 
                    summary = floatValue1 / floatValue2;
                }
                else
                {
                    summary = 0;
                }
               
            }

            Value1 = summary.ToString();
            Value2 = null;
            Operator = OperatorType.NONE;
        }

        public void OperatorsCommandExecute(string digit)
        {
            if (Value1 == null)
            {
                Value1 = "0";
            }
            switch (digit)
            {
                case "+":
                    SummaryCommandExecute();
                    Operator = OperatorType.ADD;
                    break;
                case "-":
                    SummaryCommandExecute();
                    Operator = OperatorType.SUB;
                    break;
                case "*":
                    SummaryCommandExecute();
                    Operator = OperatorType.MUL;
                    break;
                case "/":
                    SummaryCommandExecute();
                    Operator = OperatorType.DIV;
                    break;
            }
        }

        private void SampleCommandExecute(string digit)
        {
            if (Operator == OperatorType.NONE)
            {
                if (Value1 != null && Value1.Equals("0"))
                {
                    Value1 = digit;
                }
                else
                {
                    Value1 += digit;
                }
            }
            else
            {
                if (Value2 != null && Value2.Equals("0"))
                {
                    Value2 = digit;
                }
                else
                { 
                    Value2 += digit;
                }
            }
        }
        private void MemoryCommandExecute(string parameter)
        {
            switch (parameter)
            {
                case "MC":
                    MemoryValue = "0";
                    break;
                case "MR":
                    if (Operator != OperatorType.NONE)
                    {
                        Value2 = MemoryValue;
                    }else
                    {
                        Value1 = MemoryValue;
                    }
                    break;
                case "MS":
                    if (Value2 != null)
                    {
                        MemoryValue = Value2;
                    }else if (Value1 != null)
                    {
                        MemoryValue = Value1;
                    }else
                    {
                        MemoryValue = "0";
                    }
                    break;
                case "M+":
                    if (Value2 != null)
                    {
                        AddToMemory(Value2);
                    }
                    else if (Value1 != null)
                    {
                        AddToMemory(Value1);
                    } 
                    break;
                case "M-":
                    if (Value2 != null)
                    {
                        SubstractFromMemory(Value2);
                    }
                    else if (Value1 != null)
                    {
                        SubstractFromMemory(Value1);
                    }
                    break;
            }
        }

        private void SubstractFromMemory(string value)
        {
            float memory = float.Parse(MemoryValue);
            float number = float.Parse(value);
            memory -= number;
            MemoryValue = memory.ToString();
        }

        private void AddToMemory(string value)
        {
            float memory = float.Parse(MemoryValue);
            float number = float.Parse(value);
            memory += number;
            MemoryValue = memory.ToString();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
         

        private void Button_KeyboardInputProviderAcquireFocus(object sender, KeyboardInputProviderAcquireFocusEventArgs e)
        {

        }
 
    }
}

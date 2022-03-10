using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using ReactiveUI;
using Roman;

namespace lab4.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string str;
        bool isOperButtonEnable;
        public MainWindowViewModel()
        {
            str = "";
            IsOperButtonEnable = false;
            OnClickCommand = ReactiveCommand.Create<string, string>((s) => ClickButton(s));
            OnClickEqual = ReactiveCommand.Create(() => Calculation());
            OnClickOper = ReactiveCommand.Create<string, string>((s) => ClickOperButton(s));
        }
        public ReactiveCommand<string, string> OnClickCommand { get;}
        public ReactiveCommand<Unit, int> OnClickEqual { get; }
        public ReactiveCommand<string, string> OnClickOper { get; }
        private delegate RomanNumber Operation(RomanNumber? rn1, RomanNumber? rn2);
        private int Calculation()
        {
            try
            {
                Operation oper = RomanNumber.Mul;
                char charOper = ' ';
                int[] borderOfNum = new int[2];
                string allCharOper = "*/+-", rn1 = "", rn2 = "";
                for (int i = 0; i < 4; i++)
                {
                    switch (i)
                    {
                        case 0:
                            oper = RomanNumber.Mul;
                            charOper = '*';
                            break;
                        case 1:
                            oper = RomanNumber.Div;
                            charOper = '/';
                            break;
                        case 2:
                            oper = RomanNumber.Sub;
                            charOper = '-';
                            break;
                        case 3:
                            oper = RomanNumber.Add;
                            charOper = '+';
                            break;
                    }
                    while (str.IndexOf(charOper) != -1)
                    {
                        borderOfNum[0] = 0;
                        borderOfNum[1] = str.Length - 1;
                        int operPos = str.LastIndexOf(charOper);

                        for (int j = operPos + 1; j < str.Length; j++)
                        {
                            if (allCharOper.IndexOf(str[j]) != -1)
                            {
                                borderOfNum[1] = j - 1;
                                break;
                            }
                        }
                        rn2 = str.Substring(operPos + 1, borderOfNum[1] - operPos);

                        for (int j = operPos - 1; j > 0; j--)
                        {
                            if (allCharOper.IndexOf(str[j]) != -1)
                            {
                                borderOfNum[0] = j + 1;
                                break;
                            }
                        }
                        rn1 = str.Substring(borderOfNum[0], operPos - borderOfNum[0]);
                        Greeting = str.Substring(0, borderOfNum[0]) + oper(new RomanNumberExtend(rn1), new RomanNumberExtend(rn2)).ToString() +
                            str.Substring(borderOfNum[1] + 1, str.Length - 1 - borderOfNum[1]);

                    }
                } 
            }
            catch (RomanNumberException)
            {
                Greeting = "";
                IsOperButtonEnable = false;
            }
            return 0;

        }
        private string ClickOperButton(string s)
        {
            Greeting += s;
            IsOperButtonEnable = false;
            return str;
        }

        private string ClickButton(string s)
        {
            Greeting += s;
            IsOperButtonEnable = true;
            return str;
        }
        public string Greeting
        {
            set
            {
                    this.RaiseAndSetIfChanged(ref str, value);
            }
            get
            {
                return str;
            }
        }

        public bool IsOperButtonEnable
        {
            set
            {
                this.RaiseAndSetIfChanged(ref isOperButtonEnable, value);
            }
            get
            {
                return isOperButtonEnable;
            }
        }
    }
}

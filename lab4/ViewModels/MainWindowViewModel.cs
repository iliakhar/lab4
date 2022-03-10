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
        string text;
        string tbWatermark;
        bool isOperButtonEnable;
        public MainWindowViewModel()
        {
            text = "";
            IsOperButtonEnable = false;
            TextBoxWatermark = "Write something";
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
                    while (text.IndexOf(charOper) != -1)
                    {
                        borderOfNum[0] = 0;
                        borderOfNum[1] = text.Length - 1;
                        int operPos = text.LastIndexOf(charOper);

                        for (int j = operPos + 1; j < text.Length; j++)
                        {
                            if (allCharOper.IndexOf(text[j]) != -1)
                            {
                                borderOfNum[1] = j - 1;
                                break;
                            }
                        }
                        rn2 = text.Substring(operPos + 1, borderOfNum[1] - operPos);

                        for (int j = operPos - 1; j > 0; j--)
                        {
                            if (allCharOper.IndexOf(text[j]) != -1)
                            {
                                borderOfNum[0] = j + 1;
                                break;
                            }
                        }
                        rn1 = text.Substring(borderOfNum[0], operPos - borderOfNum[0]);
                        TextBoxText = text.Substring(0, borderOfNum[0]) + oper(new RomanNumberExtend(rn1), new RomanNumberExtend(rn2)).ToString() +
                            text.Substring(borderOfNum[1] + 1, text.Length - 1 - borderOfNum[1]);

                    }
                } 
            }
            catch (RomanNumberException)
            {
                TextBoxText = "";
                IsOperButtonEnable = false;
                TextBoxWatermark = "Error";
            }
            return 0;

        }
        private string ClickOperButton(string s)
        {
            TextBoxText += s;
            IsOperButtonEnable = false;
            return text;
        }

        private string ClickButton(string s)
        {
            TextBoxText += s;
            IsOperButtonEnable = true;
            return text;
        }
        public string TextBoxText
        {
            set
            {
                    this.RaiseAndSetIfChanged(ref text, value);
            }
            get
            {
                return text;
            }
        }
        
        public string TextBoxWatermark
        {
            set
            {
                this.RaiseAndSetIfChanged(ref tbWatermark, value);
            }
            get
            {
                return tbWatermark;
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

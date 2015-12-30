using Assisticant.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboCoder
{
    class InstructionSet
    {
        private ObservableList<string> _instructions = new ObservableList<string>();
        private static KeysConverter _converter = new KeysConverter();

        public ImmutableList<string> Instructions
        {
            get { return _instructions.ToImmutableList(); }
        }

        public void SetInstructions(ImmutableList<string> instructions)
        {
            _instructions.Clear();
            _instructions.AddRange(instructions);
        }

        public void AppendKey(KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Enter)
            {
                _instructions.Add("{ENTER}");
            }
            else
            {
                Match<KeyEventArgs, string>.NoResult(args)
                    .Or(InstructionCode)
                    .Or(Symbol)
                    .Or(Letter)
                    .Or(Digit)
                    .Or(NumberPadOperator)
                    .Or(NumberPadDigit)
                    .Or(FunctionKey)
                    .OnMatch(delegate (string code)
                    {
                        if (_instructions.Count > 0 && IsTextInstruction(_instructions[_instructions.Count - 1]))
                        {
                            _instructions[_instructions.Count - 1] = _instructions[_instructions.Count - 1] + code;
                        }
                        else
                        {
                            _instructions.Add(code);
                        }
                    });
            }
        }

        private static bool IsTextInstruction(string instruction)
        {
            return instruction != "{ENTER}";
        }

        private Match<KeyEventArgs, string> Letter(KeyEventArgs args)
        {
            if (args.KeyCode >= Keys.A && args.KeyCode <= Keys.Z)
            {
                var keyChar = (char)args.KeyCode;
                if (!args.Shift)
                {
                    keyChar = char.ToLower(keyChar);
                }
                return Match<KeyEventArgs, string>.Result(args, keyChar.ToString());
            }
            else
            {
                return Match<KeyEventArgs, string>.NoResult(args);
            }
        }

        private Match<KeyEventArgs, string> Digit(KeyEventArgs args)
        {
            if (args.KeyCode >= Keys.D0 && args.KeyCode <= Keys.D9)
            {
                if (args.Shift)
                {
                    switch (args.KeyCode)
                    {
                        case Keys.D0:
                            return Match<KeyEventArgs, string>.Result(args, "{)}");
                        case Keys.D1:
                            return Match<KeyEventArgs, string>.Result(args, "!");
                        case Keys.D2:
                            return Match<KeyEventArgs, string>.Result(args, "@");
                        case Keys.D3:
                            return Match<KeyEventArgs, string>.Result(args, "#");
                        case Keys.D4:
                            return Match<KeyEventArgs, string>.Result(args, "$");
                        case Keys.D5:
                            return Match<KeyEventArgs, string>.Result(args, "{%}");
                        case Keys.D6:
                            return Match<KeyEventArgs, string>.Result(args, "{^}");
                        case Keys.D7:
                            return Match<KeyEventArgs, string>.Result(args, "&");
                        case Keys.D8:
                            return Match<KeyEventArgs, string>.Result(args, "*");
                        case Keys.D9:
                            return Match<KeyEventArgs, string>.Result(args, "{(}");
                    }
                }
                else
                {
                    var keyChar = (char)args.KeyCode;
                    return Match<KeyEventArgs, string>.Result(args, keyChar.ToString());
                }
            }
            return Match<KeyEventArgs, string>.NoResult(args);
        }

        private Match<KeyEventArgs, string> Symbol(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Oemtilde:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "~" : "`");
                case Keys.OemSemicolon:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? ":" : ";");
                case Keys.Oemplus:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "{+}" : "=");
                case Keys.Oemcomma:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "<" : ",");
                case Keys.OemMinus:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "_" : "-");
                case Keys.OemPeriod:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? ">" : ".");
                case Keys.OemOpenBrackets:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "{{}" : "[");
                case Keys.OemPipe:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "|" : "\\");
                case Keys.OemCloseBrackets:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "{}}" : "]");
                case Keys.OemQuotes:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "\"" : "'");
                case Keys.OemQuestion:
                case Keys.OemBackslash:
                    return Match<KeyEventArgs, string>.Result(args, args.Shift ? "?" : "/");
                default:
                    return Match<KeyEventArgs, string>.NoResult(args);
            }
        }

        private Match<KeyEventArgs, string> NumberPadOperator(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Divide:
                    return Match<KeyEventArgs, string>.Result(args, "/");
                case Keys.Multiply:
                    return Match<KeyEventArgs, string>.Result(args, "*");
                case Keys.Subtract:
                    return Match<KeyEventArgs, string>.Result(args, "-");
                case Keys.Add:
                    return Match<KeyEventArgs, string>.Result(args, "{+}");
                case Keys.Decimal:
                    return Match<KeyEventArgs, string>.Result(args, ".");
                default:
                    return Match<KeyEventArgs, string>.NoResult(args);
            }
        }

        private Match<KeyEventArgs, string> NumberPadDigit(KeyEventArgs args)
        {
            if (args.KeyCode >= Keys.NumPad0 && args.KeyCode <= Keys.NumPad9)
            {
                var keyChar = (char)((int)'0' + args.KeyCode - Keys.NumPad0);
                return Match<KeyEventArgs, string>.Result(args, keyChar.ToString());
            }
            return Match<KeyEventArgs, string>.NoResult(args);
        }

        private Match<KeyEventArgs, string> FunctionKey(KeyEventArgs args)
        {
            if (args.KeyCode >= Keys.F1 && args.KeyCode <= Keys.F16)
            {
                var function = args.KeyCode - Keys.F1 + 1;
                return Match<KeyEventArgs, string>.Result(args, string.Format("{{F{0}}}", function));
            }
            return Match<KeyEventArgs, string>.NoResult(args);
        }

        private Match<KeyEventArgs, string> InstructionCode(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Space:
                    return Match<KeyEventArgs, string>.Result(args, " ");
                case Keys.Tab:
                    return Match<KeyEventArgs, string>.Result(args, "{TAB}");
                case Keys.Back:
                    return Match<KeyEventArgs, string>.Result(args, "{BACK}");
                case Keys.CapsLock:
                    return Match<KeyEventArgs, string>.Result(args, "{CAPSLOCK}");
                case Keys.Escape:
                    return Match<KeyEventArgs, string>.Result(args, "{ESC}");
                case Keys.PageUp:
                    return Match<KeyEventArgs, string>.Result(args, "{PGUP}");
                case Keys.PageDown:
                    return Match<KeyEventArgs, string>.Result(args, "{PGDN}");
                case Keys.End:
                    return Match<KeyEventArgs, string>.Result(args, "{END}");
                case Keys.Home:
                    return Match<KeyEventArgs, string>.Result(args, "{HOME}");
                case Keys.Left:
                    return Match<KeyEventArgs, string>.Result(args, "{LEFT}");
                case Keys.Up:
                    return Match<KeyEventArgs, string>.Result(args, "{UP}");
                case Keys.Right:
                    return Match<KeyEventArgs, string>.Result(args, "{RIGHT}");
                case Keys.Down:
                    return Match<KeyEventArgs, string>.Result(args, "{DOWN}");
                case Keys.PrintScreen:
                    return Match<KeyEventArgs, string>.Result(args, "{PRTSC}");
                case Keys.Insert:
                    return Match<KeyEventArgs, string>.Result(args, "{INSERT}");
                case Keys.Delete:
                    return Match<KeyEventArgs, string>.Result(args, "{DELETE}");
                case Keys.NumLock:
                    return Match<KeyEventArgs, string>.Result(args, "{NUMLOCK}");
                case Keys.Scroll:
                    return Match<KeyEventArgs, string>.Result(args, "{SCROLLLOCK}");
                default:
                    return Match<KeyEventArgs, string>.NoResult(args);
            }
        }
    }
}

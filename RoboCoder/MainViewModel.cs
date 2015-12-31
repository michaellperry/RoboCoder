using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboCoder
{
    class MainViewModel
    {
        private readonly InstructionSet _instructionSet;

        public MainViewModel(InstructionSet instructionSet)
        {
            _instructionSet = instructionSet;
        }

        public string Instructions
        {
            get
            {
                return string.Join("\n", _instructionSet.Instructions.ToArray());
            }
            set
            {
                var lines = value.Replace("\r", "").Split('\n');
                _instructionSet.SetInstructions(lines.ToImmutableList());
            }
        }

        public void KeyDown(KeyEventArgs args)
        {
            _instructionSet.AppendKey(args);
        }

        public void KeyUp(KeyEventArgs args)
        {
            _instructionSet.KetUp(args);
        }

        public bool Recording
        {
            get { return _instructionSet.Recording; }
            set
            {
                _instructionSet.Recording = value;
                _instructionSet.Save();
            }
        }

        public void Record()
        {
            _instructionSet.Recording = true;
        }

        public void Stop()
        {
            _instructionSet.Recording = false;
        }

        public void Play()
        {
            _instructionSet.Play();
        }

        public void OpenFile(string fileName)
        {
            _instructionSet.OpenFile(fileName);
        }

        public void Close()
        {
            _instructionSet.Save();
        }

        public string Error
        {
            get { return _instructionSet.Error; }
        }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(_instructionSet.Error); }
        }
    }
}

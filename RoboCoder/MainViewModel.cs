using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assisticant.Fields;

namespace RoboCoder
{
    class MainViewModel
    {
        private readonly Observable<double> _speed = new Observable<double>(1);
        private readonly InstructionSet _instructionSet;
        private readonly Typer _typer;

        public MainViewModel(InstructionSet instructionSet, Typer typer)
        {
            _instructionSet = instructionSet;
            _typer = typer;
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
       
        public double Speed
        {
            get { return _speed.Value; }
            set
            {
                _speed.Value = value;
            }
        }

        public void Record()
        {
            _instructionSet.Recording = true;
        }

        public void Stop()
        {
            _instructionSet.Recording = false;
            _instructionSet.Pause();
        }

        public void Play()
        {
            _typer.SetSpeed(Speed);
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

using Assisticant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCoder
{
    class ViewModelLocator : ViewModelLocatorBase
    {
        private static readonly Typer _typer = new Typer();
        private readonly InstructionSet _instructionSet = new InstructionSet(_typer);

        public object Main
        {
            get { return ViewModel(() => new MainViewModel(_instructionSet, _typer)); }
        }
    }
}

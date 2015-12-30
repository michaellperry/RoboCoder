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
        private InstructionSet _instructionSet = new InstructionSet();

        public object Main
        {
            get { return ViewModel(() => new MainViewModel(_instructionSet)); }
        }
    }
}

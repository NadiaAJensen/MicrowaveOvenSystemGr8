using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenLibrary.Interfaces;

namespace MicrowaveOvenLibrary.Boundary
{
    public class SoundBuzzer : ISoundbuzzer
    {
        private IOutput myOutput;

        public SoundBuzzer(IOutput output)
        {
            myOutput = output;
        }
        public void Buzz3Times()
        {
            myOutput.OutputLine("Bzzz, Bzzz, Bzzz");
        }

        
    }
}

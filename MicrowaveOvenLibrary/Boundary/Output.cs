using MicrowaveOvenLibrary.Interfaces;

namespace MicrowaveOvenLibrary.Boundary
{
    public class Output : IOutput
    {
        public void OutputLine(string line)
        {
            System.Console.WriteLine(line);
        }
        
    }
}
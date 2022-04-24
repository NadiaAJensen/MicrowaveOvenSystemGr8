using System;
using MicrowaveOvenLibrary.Interfaces;

namespace MicrowaveOvenLibrary.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;
        public int Maxpower { get; set; }

        public PowerTube(IOutput output, int maxpower)
        {
            myOutput = output;
            Maxpower = maxpower;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || Maxpower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and "+ Maxpower+" (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}
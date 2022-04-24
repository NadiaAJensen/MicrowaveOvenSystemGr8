using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOvenLibrary.Interfaces
{
    public interface IPowerTube
    {
       public int Maxpower { get; set; }
        void TurnOn(int power);

        void TurnOff();
    }
}

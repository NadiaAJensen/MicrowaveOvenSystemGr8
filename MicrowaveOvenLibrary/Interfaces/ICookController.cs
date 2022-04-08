using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOvenLibrary.Interfaces
{
    public interface ICookController
    {
        void StartCooking(int power, int time);
        void Stop();
    }
}

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

        void AddOnTime(object sender, EventArgs e, int addedTime);
        void SubtractTime(object sender, EventArgs e, int subtractedTime);
    }
}

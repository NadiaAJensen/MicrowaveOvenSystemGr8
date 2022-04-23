using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicrowaveOvenLibrary.Interfaces
{
    public interface ITimer
    {
        int TimeRemaining { get; }
        event EventHandler Expired;
        event EventHandler TimerTick;
        event EventHandler AddTime;
        event EventHandler SubtractTime;

        void Start(int time);
        void Stop();

        int AddOnTime();
        int SubtractOnTime();
    }
}

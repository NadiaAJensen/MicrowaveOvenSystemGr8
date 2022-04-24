﻿using System;
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

        void Start(int time);
        void Stop();

        int AddOnTime(); //New method
        int SubtractOnTime(); //New method
    }
}

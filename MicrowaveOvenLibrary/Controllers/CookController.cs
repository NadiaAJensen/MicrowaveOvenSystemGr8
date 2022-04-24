using System;
using System.Collections.Generic;
using MicrowaveOvenLibrary.Interfaces;

namespace MicrowaveOvenLibrary.Controllers
{
    public class CookController : ICookController
    {
        // Since there is a 2-way association, this cannot be set until the UI object has been created
        // It also demonstrates property dependency injection
        public IUserInterface UI { set; private get; }

        private bool isCooking = false;

        private IDisplay myDisplay;
        private IPowerTube myPowerTube;
        private ITimer myTimer;
        private int maxpower;

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            IUserInterface ui) : this(timer, display, powerTube)
        {
           UI = ui;
           maxpower = myPowerTube.Maxpower;
        }

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube)
        {
            myTimer = timer;
            myDisplay = display;
            myPowerTube = powerTube;

            timer.Expired += new EventHandler(OnTimerExpired);
            timer.TimerTick += new EventHandler(OnTimerTick);
        }

        public void StartCooking(int power, int time)
        {
            myPowerTube.TurnOn(power);
            myTimer.Start(time);
            isCooking = true;
        }

        public void Stop()
        {
            isCooking = false;
            myPowerTube.TurnOff();
            myTimer.Stop();
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (isCooking)
            {
                isCooking = false;
                myPowerTube.TurnOff();
                UI.CookingIsDone();
            }
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            if (isCooking)
            {
                int remaining = myTimer.TimeRemaining;
                myDisplay.ShowTime(remaining / 60, remaining % 60);
            }
        }

        //New method made by Nadia
        public void AddOnTime(object sender, EventArgs e)
        {
            if (isCooking)
            {
                myTimer.AddOnTime();
                int remaining = myTimer.TimeRemaining;
                myDisplay.ShowTime(remaining / 60, remaining % 60);
            }
        }

        //New method made by Nadia
        public void SubtractTime(object sender, EventArgs e)
        {
            if (isCooking)
            {
                myTimer.SubtractOnTime();
                int remaining = myTimer.TimeRemaining;
                myDisplay.ShowTime(remaining / 60, remaining % 60);
            }
        }
    }
}
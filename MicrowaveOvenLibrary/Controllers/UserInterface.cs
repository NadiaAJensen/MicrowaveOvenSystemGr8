using System;
using System.Runtime.Serialization;
using MicrowaveOvenLibrary.Interfaces;

namespace MicrowaveOvenLibrary.Controllers
{
    public class UserInterface : IUserInterface
    {
        private enum States
        {
            READY, SETPOWER, SETTIME, COOKING, DOOROPEN
        }

        private States myState = States.READY;

        private ICookController myCooker;
        private ILight myLight;
        private IDisplay myDisplay;

        private int powerLevel = 50;
        private int minutes = 1;
        private int seconds = 0;

        public UserInterface(
            IButton powerButton,
            IButton timeButton,
            IButton secondsButton,
            IButton startCancelButton,
            IDoor door,
            IDisplay display,
            ILight light,
            ICookController cooker)
        {
            powerButton.Pressed += new EventHandler(OnPowerPressed);
            timeButton.Pressed += new EventHandler(OnTimePressed);
            secondsButton.Pressed += new EventHandler(OnSecondsTimePressed);
            startCancelButton.Pressed += new EventHandler(OnStartCancelPressed);

            door.Closed += new EventHandler(OnDoorClosed);
            door.Opened += new EventHandler(OnDoorOpened);

            myCooker = cooker;
            myLight = light;
            myDisplay = display;
        }

        private void ResetValues()
        {
            powerLevel = 50;
            minutes = 0;
            seconds = 0;
        }

        public void OnPowerPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myDisplay.ShowPower(powerLevel);
                    myState = States.SETPOWER;
                    break;
                case States.SETPOWER:
                    powerLevel = (powerLevel >= 700 ? 50 : powerLevel+50);
                    myDisplay.ShowPower(powerLevel);
                    break;
            }
        }

        public void OnTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    myDisplay.ShowTime(minutes, seconds);
                    myState = States.SETTIME;
                    break;
                case States.SETTIME:
                    minutes += 1;
                    myDisplay.ShowTime(minutes, seconds);
                    break;
            }
        }

        public void OnSecondsTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    myDisplay.ShowTime(minutes, seconds);
                    myState = States.SETTIME;
                    break;
                case States.SETTIME:
                    seconds += 1;
                    if (seconds >= 60)
                    {
                        minutes += 1;
                        seconds = 0;
                    }
                    myDisplay.ShowTime(minutes, seconds);
                    break;
            }
        }

        public void OnStartCancelPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    ResetValues();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
                case States.SETTIME:
                    myLight.TurnOn();
                    myCooker.StartCooking(powerLevel, minutes*60+seconds);
                    myState = States.COOKING;
                    break;
                case States.COOKING:
                    ResetValues();
                    myCooker.Stop();
                    myLight.TurnOff();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
            }
        }

        public void OnDoorOpened(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myLight.TurnOn();
                    myState = States.DOOROPEN;
                    break;
                case States.SETPOWER:
                    ResetValues();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.SETTIME:
                    ResetValues();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.COOKING:
                    myCooker.Stop();
                    myDisplay.Clear();
                    ResetValues();
                    myState = States.DOOROPEN;
                    break;
            }
        }

        public void OnDoorClosed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.DOOROPEN:
                    myLight.TurnOff();
                    myState = States.READY;
                    break;
            }
        }

        public void CookingIsDone()
        {
            switch (myState)
            {
                case States.COOKING:
                    ResetValues();
                    myDisplay.Clear();
                    myLight.TurnOff();
                    // Beep 3 times
                    myState = States.READY;
                    break;
            }
        }
    }
}
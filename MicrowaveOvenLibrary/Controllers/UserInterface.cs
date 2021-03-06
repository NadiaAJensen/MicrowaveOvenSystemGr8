using System;
using System.Runtime.Serialization;

using MicrowaveOvenLibrary.Boundary;

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
        private IPowerTube _powerTube;


        //  SB
        private ISoundbuzzer mySoundBuzzer;


        private int powerLevel = 50;
        private int minutes = 1;
        private int seconds = 0;

        public UserInterface(
            IButton powerButton,
            IButton timeButton,
            IButton secondsButton,
            IButton startCancelButton,
            IButton addTimeButton, //New button by Nadia
            IButton subtractTimeButton, //New button made by Nadia
            IDoor door,
            IDisplay display,
            ILight light,
            ICookController cooker,IPowerTube powertube, ISoundbuzzer soundBuzzer)

        {
            powerButton.Pressed += new EventHandler(OnPowerPressed);
            timeButton.Pressed += new EventHandler(OnTimePressed);
            secondsButton.Pressed += new EventHandler(OnSecondsTimePressed);
            startCancelButton.Pressed += new EventHandler(OnStartCancelPressed);
            addTimeButton.Pressed += new EventHandler(AddTimePressed); //New button
            subtractTimeButton.Pressed += new EventHandler(SubtractTimePressed); //New button

            door.Closed += new EventHandler(OnDoorClosed);
            door.Opened += new EventHandler(OnDoorOpened);

            myCooker = cooker;
            myLight = light;
            myDisplay = display;
            _powerTube = powertube;
            mySoundBuzzer = soundBuzzer;//
        }

        //New method made by Nadia
        private void AddTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.COOKING:
                    myCooker.AddOnTime(sender, e);
                    break;
            }
        }

        //New method made by Nadia
        private void SubtractTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.COOKING:
                    myCooker.SubtractTime(sender, e);
                    break;
            }
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
                    powerLevel = (powerLevel >= _powerTube.Maxpower ? 50 : powerLevel+50);
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
                    mySoundBuzzer.Buzz3Times(); //

                    // Beep 3 times
                    myState = States.READY;
                    break;
            }
        }
    }
}
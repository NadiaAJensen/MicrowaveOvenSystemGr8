using System;
using MicrowaveOvenLibrary.Boundary;
using MicrowaveOvenLibrary.Controllers;

namespace MicrowaveOvenSystemGr8
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button addButton = new Button(); //New button
            Button subtractButton = new Button(); //New button
            Button minutesButton = new Button();
            Button secondsButton = new Button();
            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output,700);

            Light light = new Light(output);

            Timer timer = new Timer();


            SoundBuzzer soundBuzzer = new SoundBuzzer(output); //

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, minutesButton, secondsButton, startCancelButton, addButton, subtractButton,door, display, light, cooker, powerTube,soundBuzzer);//

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence 1,5min

            powerButton.Press();

            minutesButton.Press();
            for (int i = 0; i < 30; i++)
            {
                secondsButton.Press();
            }

            startCancelButton.Press();

            // The simple sequence should now run //Here Nadia made some changes
            bool cont = true;
            System.Console.WriteLine("When you press [C], the program will stop");
            System.Console.WriteLine("Add on 1 minute, press [A] or subtract 1 minute, press [S]");
            // Wait for input
            while (cont)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                    case 'A':
                        addButton.Press();
                        break;
                    case 's':
                    case 'S':
                        subtractButton.Press();
                        break;
                    case 'c':
                    case 'C':
                        cont = false;
                        break;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MicrowaveOvenLibrary.Boundary;
using MicrowaveOvenLibrary.Interfaces;
using NSubstitute;

namespace MicrowaveOvenSystemTest_unit
{
    [TestFixture]
    public class SoundBuzzerTest
    {
        private SoundBuzzer uut;
        private IOutput output;

        [SetUp]

        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new SoundBuzzer(output);
        }

        [Test]
        public void Buzz3Times_CorrectOutput()
        {
            uut.Buzz3Times();
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Equals("Bzzz, Bzzz, Bzzz")));
        }




    }
}

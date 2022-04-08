using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOvenLibrary.Interfaces
{
    public interface IDoor
    {
        event EventHandler Opened;
        event EventHandler Closed;

        void Open();
        void Close();
    }
}

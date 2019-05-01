using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Runtime
{
    public class DiagnosticLog : ILog
    {
        public void Write(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}

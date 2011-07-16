using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.ComponentModel;

namespace Imml
{
    public class ElementNotFoundException : Exception
    {
        public IImmlContext Container { get; private set; }

        public ElementNotFoundException(string message, IImmlContext container)
            : base(message)
        {
            this.Container = container;
        }        
    }
}

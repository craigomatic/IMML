using System;
using System.Collections.Generic;
using System.Text;

namespace Imml
{
    public interface IElementFactory
    {
        ImmlElement Create(string elementName, IImmlElement parentElement);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Imml
{
    public static class ElementFactory
    {
        public static ImmlElement Create(string elementName)
        {
            var type = _FindType(elementName, Assembly.GetExecutingAssembly());

            if (type == null)
            {
                return null;
            }

            var element = Activator.CreateInstance(type);

            if (!(element is ImmlElement))
            {
                return null;
            }

            return element as ImmlElement;
        }

        private static Type _FindType(string typeName, Assembly assembly)
        {
            Type[] assemblyTypes = assembly.GetTypes();

            foreach (Type type in assemblyTypes)
            {
                if (type.Name == typeName)
                    return type;
            }

            return null;
        }
    }
}

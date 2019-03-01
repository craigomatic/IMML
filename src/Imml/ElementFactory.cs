using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Imml
{
    /// <summary>
    /// Default implementation of <see cref="IElementFactory"/> that instantiates elements by matching their name to types within the executing assembly
    /// </summary>
    public class ElementFactory : IElementFactory
    {
        public static ElementFactory Default = new ElementFactory();

        public virtual Assembly ResolveAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        public virtual ImmlElement Create(string elementName, IImmlElement parentElement)
        {
            var rootAssembly = this.ResolveAssembly();

            var type = _FindType(elementName, rootAssembly);

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

        private Type _FindType(string typeName, Assembly assembly)
        {
            var assemblyTypes = assembly.GetTypes();

            foreach (var type in assemblyTypes)
            {
                if (type.Name == typeName)
                    return type;
            }

            return null;
        }
    }
}

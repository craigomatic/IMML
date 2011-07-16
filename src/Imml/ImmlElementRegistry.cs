using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml
{
    public class ImmlElementRegistry : IImmlElementRegistry
    {
        private Dictionary<string, int> _NameToSystemID;

        private Dictionary<int, string> _SystemIDToName;

        /// <summary>
        /// Gets the elements.
        /// </summary>
        public IDictionary<int, ImmlElement> Elements { get; private set; }

        public ImmlElementRegistry()
        {
            _NameToSystemID = new Dictionary<string, int>();
            _SystemIDToName = new Dictionary<int, string>();
            
            this.Elements = new Dictionary<int, ImmlElement>();
        }

        public void Add(ImmlElement element)
        {
            if (this.Elements.ContainsKey(element.ID))
            {
                return;
            }

            var count = 1;
            var elementName = element.Name;

            while (_NameToSystemID.ContainsKey(elementName))
            {
                elementName = element.Name + count;
                count++;
            }    

            this.Elements.Add(element.ID, element);
            _NameToSystemID.Add(elementName, element.ID);
            _SystemIDToName.Add(element.ID, elementName);

            //update the element name
            element.Name = elementName;
            element._ElementRegistry = this;
        }

        public void Remove(ImmlElement element)
        {
            //as the name may have changed, it's a better idea to use the stored name here
            string name;

            if (!_SystemIDToName.TryGetValue(element.ID, out name))
            {
                return;
            }

            _NameToSystemID.Remove(name);
            _SystemIDToName.Remove(element.ID);
            this.Elements.Remove(element.ID);
        }

        public bool TryGetElementByName<T>(string name, out T element) where T : ImmlElement
        {
            int id;

            if (!_NameToSystemID.TryGetValue(name, out id))
            {
                element = null;
                return false;   
            }

            return this.TryGetElementById(id, out element);
        }

        public bool TryGetElementById<T>(int id, out T element) where T : ImmlElement
        {
            ImmlElement outElement = null;

            if (this.Elements.TryGetValue(id, out outElement))
            {
                element = (T)outElement;
                return true;
            }

            element = null;
            return false;
        }


        public bool ContainsName(string name)
        {
            return _NameToSystemID.ContainsKey(name);
        }

        public bool ContainsID(int id)
        {
            return this.Elements.ContainsKey(id);
        }
    }
}

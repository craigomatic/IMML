using System;
using System.Collections.Generic;
using Imml.ComponentModel;

namespace Imml
{
    public interface IImmlElement
    {
        void Add(ImmlElement element);

        void Clear();
        
        IImmlContext Container { get; }
        
        bool ContainsID(int id);
        
        bool ContainsName(string name);
        
        string Context { get; set; }
        
        IList<ImmlElement> Elements { get; }
        
        T GetElementByID<T>(int id) where T : ImmlElement;

        T GetElementByName<T>(string name) where T : ImmlElement;
        
        bool HasChildren { get; }
        
        int ID { get; }
        
        string Name { get; set; }
        
        ImmlElement Parent { get; }
        
        void Remove(ImmlElement element);
        
        bool TryGetElementByID<T>(int id, out T element) where T : ImmlElement;
        
        bool TryGetElementByName<T>(string name, out T element) where T : ImmlElement;
    }
}

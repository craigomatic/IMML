﻿//-----------------------------------------------------------------------
//VastPark is a lightweight extensible virtual world platform 
//and this file is a program released under the GPL.
//Copyright (C) 2009 VastPark
//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.ComponentModel;

namespace Imml
{
    /// <summary>
    /// Base definition of an IMML element. All IMML elements should inherit from this base class.
    /// </summary>
    public class ImmlElement : BindableObject, IImmlElement
    {
        /// <summary>
        /// Gets or sets the behaviours.
        /// </summary>
        /// <value>
        /// The behaviours.
        /// </value>
        public IList<string> Behaviours { get; set; }

        /// <summary>
        /// The data context for this element used during binding evaluations
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has children.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has children; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildren
        {
            get { return this.Elements.Count > 0; }
        }

        /// <summary>
        /// The unique identifier for this element
        /// </summary>
        public int ID { get; private set; }

        private string _Name;

        /// <summary>
        /// Gets or sets the name for this element
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value)
                {
                    return;
                }                

                var oldName = _Name;
                _Name = value;

                _ElementRegistry.Remove(this);
                _ElementRegistry.Add(this);

                base.RaisePropertyChanged("Name", oldName, _Name);
            }
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        public IImmlContext Container
        {
            get { return ImmlHelper.FindParentContainer(this); }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public virtual ImmlElement Parent { get; protected set; }
        
        /// <summary>
        /// Gets the list of elements which are children of this element
        /// </summary>
        public IList<ImmlElement> Elements { get; private set; }

        internal IImmlElementRegistry _ElementRegistry;

        public ImmlElement()
        {
            this.Behaviours = new List<string>();
            this.Elements = new List<ImmlElement>();
            this.ID = ImmlHelper.NextID();

            _ElementRegistry = new ImmlElementRegistry();
            
            this.Name = this.GetType().Name;
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void Add(ImmlElement element)
        {
            element.Parent = this;
    
            _ElementRegistry.Add(element);        

            //at this point, a relationship has been created between the added element and this element
            //all names must be unique before this method exits
            var containerElement = (ImmlElement)ImmlHelper.FindParentContainer(this);

            if (containerElement == null)
            {
                //this is the container element
                containerElement = this;
            }

            var children = element.Elements.AsRecursiveEnumerable();

            foreach (var child in children)
            {
                _ElementRegistry.Add(child);
            }

            this.Elements.Add(element);    
        }

        /// <summary>
        /// Removes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void Remove(ImmlElement element)
        {
            this.Elements.Remove(element);
            element.Parent = null;

            _ElementRegistry.Remove(element);

            var children = element.Elements.AsRecursiveEnumerable();

            foreach (var child in children)
            {
                _ElementRegistry.Remove(child);
            } 

            //TODO: restore the element's original registry
        }

        /// <summary>
        /// Clears this instance of all elements.
        /// </summary>
        public virtual void Clear()
        {
            foreach (var child in this.Elements)
            {
                child.Parent = null;

                _ElementRegistry.Remove(child);
            }

            this.Elements.Clear();
        }

        /// <summary>
        /// Tries to get the element by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public virtual bool TryGetElementByName<T>(string name, out T element) where T : ImmlElement
        {
            return _ElementRegistry.TryGetElementByName(name, out element);
        }

        public virtual ImmlElement GetElementByName(string name)
        {
            ImmlElement outElement = null;

            if (this.TryGetElementByName(name, out outElement))
            {
                return outElement;
            }

            throw new ElementNotFoundException(string.Format("Element with name {0} could not be found", name), this.Container);
        }

        /// <summary>
        /// Tries to get the element by ID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public virtual bool TryGetElementByID<T>(int id, out T element) where T : ImmlElement
        {
            return _ElementRegistry.TryGetElementById(id, out element);
        }

        /// <summary>
        /// Looks for the element with the given ID in the current collection and any child elements
        /// </summary>
        /// <param name="id">VastPark ID of the desired element</param>
        /// <returns></returns>
        /// <exception cref="ElementNotFoundException">If no element matching the Id is found</exception>
        public virtual ImmlElement GetElementByID(int id)
        {
            ImmlElement outElement = null;

            if (this.TryGetElementByID(id, out outElement))
            {
                return outElement;
            }

            throw new ElementNotFoundException(string.Format("Element with id {0} could not be found", id), this.Container);
        }

        /// <summary>
        /// Returns true if the collection contains a child element with the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool ContainsName(string name)
        {
            return _ElementRegistry.ContainsName(name);
        }

        /// <summary>
        /// Returns true if the collection contains a child element with the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool ContainsID(int id)
        {
            return _ElementRegistry.ContainsID(id);
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", this.GetType().Name, this.Name);
        }
    }
}
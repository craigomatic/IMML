﻿using Imml.ComponentModel;
using Imml.IO;
using Imml.Runtime.Services;
using Imml.Scene;
using Imml.Scene.Container;
using Imml.Scene.Controls;
using Imml.Scene.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imml.Runtime
{
    public class ImmersiveEnvironment<T> : IDisposable
    {
        public ICacheService Cache { get; private set; }

        public ImmlDocument Document { get; private set; }

        public IImmlSerialiser Serialiser { get; private set; }

        public IResourceAcquisitionService ResourceAcquisitionService { get; private set; }

        public ILog Log { get; private set; }

        public T ParentNode { get; private set; }

        /// <summary>
        /// Gets the active camera element
        /// </summary>
        public Camera Camera { get; private set; }

        public ImmersiveEnvironment(IImmlSerialiser serialiser, IResourceAcquisitionService resourceAcquisitionService, ILog log = null)
        {
            this.Serialiser = serialiser;
            this.ResourceAcquisitionService = resourceAcquisitionService;

            this.Log = log == null ? 
                new DiagnosticLog() : this.Log = log;
        }

        private bool _IsCreated;

        public async Task CreateAsync(Stream sceneData)
        {
            _IsCreated = false;

            this.Document = this.Serialiser.Read<ImmlDocument>(sceneData);

            this.Log.Write($"Loading '{this.Document.Name}'");

            //look for includes and add those elements to the collection before load occurs below. Elements that have been included in this way are not isolated from the main this.Document context and name collisions, etc are possible
            var allIncludes = this.Document.Elements.AsRecursiveEnumerable().OfType<Include>().ToArray();

            foreach (var include in allIncludes)
            {
                //TODO: support nested includes
                try
                {
                    var bytes = await this.ResourceAcquisitionService.AcquireResource(include);
                    var includedDocument = this.Serialiser.Read<ImmlDocument>(new MemoryStream(bytes));

                    foreach (var element in includedDocument.Elements)
                    {
                        this.Document.Add(element);
                    }
                }
                catch
                {
                    this.Log.Write($"Unable to load Include from '{include.Source}'");
                }
            }

            var allRuntimeElements = this.Document.Elements.AsRecursiveEnumerable().OfType<IRuntimeElement<T>>();

            foreach (var item in allRuntimeElements)
            {
                try
                {
                    await item.AcquireResourcesAsync();
                }
                catch (Exception e)
                {
                    this.Log.Write($"Unable to acquire resources for {(item as ImmlElement).Name}");
                }
            }

            _IsCreated = true;
        }

        public void Run(T parentNode)
        {
            if (!_IsCreated)
            {
                throw new Exception("Environment must first be created via a call to CreateAsync.");
            }

            this.ParentNode = parentNode;

            var allRuntimeElements = this.Document.Elements.AsRecursiveEnumerable().OfType<IRuntimeElement<T>>();

            //first pass, load all elements
            foreach (var item in allRuntimeElements)
            {
                parentNode = this.ParentNode;

                if (item.Parent != this.Document &&
                    item.Parent is IRuntimeElement<T>)
                {                    
                    parentNode = (item.Parent as IRuntimeElement<T>).Node;
                }

                item.Load(parentNode);
            }

            //second pass, apply layout
            foreach (var item in allRuntimeElements)
            {
                item.ApplyLayout();
            }

            this.Camera = this.Document.GetCamera();
        }

        public void Dispose()
        {
            var runtimeElements = this.Document.Elements.OfType<IRuntimeElement<T>>();

            foreach (var element in runtimeElements)
            {
                element.Dispose();
            }
        }

        public void HandleGesture(EventType eventType)
        {
            //TODO: implement            
        }
    }
}

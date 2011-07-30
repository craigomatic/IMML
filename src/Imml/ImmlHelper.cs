using System;
using System.Collections.Generic;
using System.Text;
using Imml.Scene;
using Imml.Scene.Controls;
using Imml.Scene.Layout;
using Imml.ComponentModel;

namespace Imml
{
    public static class ImmlHelper
    {
        /// <summary>
        /// Finds the parent container for the given element. Returns null if no parent is specified.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IImmlContext FindParentContainer(ImmlElement element)
        {
            if (element.Parent == null && element is IImmlContext)
                return element as IImmlContext;
            else if (element.Parent == null)
                return null;

            return _FindParent(element);
        }

        private static IImmlContext _FindParent(ImmlElement element)
        {
            if (element.Parent == null)
                return null;

            if (element.Parent is IImmlContext)
                return element.Parent as IImmlContext;

            return _FindParent(element.Parent);
        }

        /// <summary>
        /// Returns true if the specified element is nested within a dock
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsDocked(ImmlElement element)
        {
            if (element is Video || element is Web)
                return (element.Parent != null && element.Parent.Parent != null && element.Parent.Parent.Parent != null && (element.Parent.Parent.Parent is Dock));

            return element.Parent is Dock;
        }

        private static int _ID;
        private static object _IdLock = new object();

        /// <summary>
        /// Returns a unique id for the element
        /// </summary>
        /// <returns></returns>
        internal static int NextID()
        {
            lock (_IdLock)
            {
                return ++_ID;
            }
        }

        public static ElementType GetType(ImmlElement element)
        {
            if (element is Camera)
                return ElementType.Camera;

            if (element is Dock)
                return ElementType.Dock;

            if (element is Effect)
                return ElementType.Effect;

            if (element is Grid)
                return ElementType.Grid;

            if (element is Light)
                return ElementType.Light;

            if (element is Model)
                return ElementType.Model;

            if (element is Plugin)
                return ElementType.Plugin;

            if (element is Anchor)
                return ElementType.Anchor;

            if (element is Primitive)
                return ElementType.Primitive;

            if (element is Script)
                return ElementType.Script;

            if (element is Shader)
                return ElementType.Shader;

            if (element is Background)
                return ElementType.Background;

            if (element is Sound)
                return ElementType.Sound;

            if (element is Stack)
                return ElementType.Stack;

            if (element is Text)
                return ElementType.Text;

            if (element is Trigger)
                return ElementType.Trigger;

            if (element is Video)
                return ElementType.Video;

            if (element is Web)
                return ElementType.Web;

            if (element is Widget)
                return ElementType.Widget;

            return ElementType.All;
        }
    }
}

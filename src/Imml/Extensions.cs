﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Imml.Scene;
using Imml.Scene.Controls;
using Imml.ComponentModel;

namespace Imml
{
    public static class Extensions
    {
        /// <summary>
        /// Creates an enumerable collection of all the elements available within the full hierarchy of the given collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static IEnumerable<ImmlElement> AsRecursiveEnumerable(this IList<ImmlElement> collection)
        {
            var stack = new Stack<IEnumerable<ImmlElement>>();
            stack.Push(collection);

            while (stack.Count > 0)
            {
                var items = stack.Pop();

                foreach (var item in items)
                {
                    yield return item;

                    var children = item;

                    if (children != null)
                    {
                        stack.Push(children.Elements);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the material group with the given Id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static MaterialGroup GetMaterialGroup<T>(this T element, int id) where T : IMaterialHostElement
        {
            return element.Elements.OfType<MaterialGroup>().Where(e => e.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets the material element from the material group.
        /// </summary>
        /// <param name="materialGroup">The material group.</param>
        /// <returns></returns>
        public static Material GetMaterial(this MaterialGroup materialGroup)
        {
            return materialGroup.Elements.OfType<Material>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the texture element from the material group.
        /// </summary>
        /// <param name="materialGroup">The material group.</param>
        /// <returns></returns>
        public static Texture GetTexture(this MaterialGroup materialGroup)
        {
            return materialGroup.Elements.OfType<Texture>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the web element from the material group.
        /// </summary>
        /// <param name="materialGroup">The material group.</param>
        /// <returns></returns>
        public static Web GetWeb(this MaterialGroup materialGroup)
        {
            return materialGroup.Elements.OfType<Web>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the video element from the material group.
        /// </summary>
        /// <param name="materialGroup">The material group.</param>
        /// <returns></returns>
        public static Video GetVideo(this MaterialGroup materialGroup)
        {
            return materialGroup.Elements.OfType<Video>().FirstOrDefault();
        }
    }
}

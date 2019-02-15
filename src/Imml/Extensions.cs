using System;
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
        /// <param name="element">The element.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static MaterialGroup GetMaterialGroup(this IMaterialHostElement element, int id)
        {
            return element.Elements.OfType<MaterialGroup>().Where(e => e.Id == id).FirstOrDefault();
        }

        public static IEnumerable<MaterialGroup> GetMaterialGroups(this IMaterialHostElement element)
        {
            return element.Elements.OfType<MaterialGroup>();
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

        /// <summary>
        /// Gets the physics element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static Physics GetPhysics(this IPhysicsHostElement element)
        {
            return element.Elements.OfType<Physics>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the physics interactions.
        /// </summary>
        /// <param name="physics">The physics.</param>
        /// <returns></returns>
        public static IList<Interaction> GetInteractions(this Physics physics)
        {
            return physics.Elements.OfType<Interaction>().ToList();
        }

        /// <summary>
        /// Gets the defines.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static IList<Define> GetDefines(this IImmlElement element)
        {
            return element.Elements.OfType<Define>().ToList();
        }

        /// <summary>
        /// Gets the network definition.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static Network GetNetwork(this INetworkHostElement element)
        {
            return element.Elements.OfType<Network>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the network filters.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static IList<Filter> GetFilters(this Network element)
        {
            return element.Elements.OfType<Filter>().ToList();
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static IList<Parameter> GetParameters(this IParameterHost element)
        {
            return element.Elements.OfType<Parameter>().ToList();
        }

        /// <summary>
        /// Gets the triggers.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static IList<Trigger> GetTriggers(this IImmlElement element)
        {
            return element.Elements.OfType<Trigger>().ToList();
        }

        /// <summary>
        /// Gets the emitters.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns></returns>
        public static IList<Emitter> GetEmitters(this Effect effect)
        {
            return effect.Elements.OfType<Emitter>().ToList();
        }

        /// <summary>
        /// Gets the particle changes.
        /// </summary>
        /// <param name="emitter">The emitter.</param>
        /// <returns></returns>
        public static IList<ParticleChange> GetParticleChanges(this Emitter emitter)
        {
            return emitter.Elements.OfType<ParticleChange>().ToList();
        }
    }
}

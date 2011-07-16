using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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
    }
}

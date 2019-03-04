using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Numerics;
using Imml.Numerics.Geometry;

namespace Imml.ComponentModel
{
    /// <summary>
    /// An element that has 3-dimensions
    /// </summary>
    public interface ICubicElement : IVisibleElement
    {
        /// <summary>
        /// The local-space bounding box
        /// </summary>
        BoundingBox BoundingBox { get; }

        /// <summary>
        /// The world-space bounding box
        /// </summary>
        BoundingBox WorldBoundingBox { get; }


        /// <summary>
        /// Gets the size of the element in world coordinate space
        /// </summary>
        Vector3 WorldSize { get; }

        /// <summary>
        /// Gets or sets the size of the element in local coordinate space
        /// </summary>
        Vector3 Size { get; set; }
    }
}

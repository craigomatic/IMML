using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Numerics;
using Imml.Numerics.Geometry;
using Imml.Numerics;

namespace Imml.ComponentModel
{
    /// <summary>
    /// An element that has 3-dimensions
    /// </summary>
    public interface ICubicElement : IVisibleElement
    {
        /// <summary>
        /// Gets the world scale.
        /// </summary>
        Vector3 WorldScale { get; }

        /// <summary>
        /// The local-space bounding box
        /// </summary>
        BoundingBox BoundingBox { get; }

        /// <summary>
        /// The world-space bounding box
        /// </summary>
        BoundingBox WorldBoundingBox { get; }
    }
}

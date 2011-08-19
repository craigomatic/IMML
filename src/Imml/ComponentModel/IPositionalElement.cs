using System;
using Imml.Numerics;

namespace Imml.ComponentModel
{
    /// <summary>
    /// An element that has a position in 3d space
    /// </summary>
    public interface IPositionalElement : IImmlElement
    {
        /// <summary>
        /// The pivot position this element should rotate about
        /// </summary>
        Vector3 Pivot { get; set; }

        /// <summary>
        /// The rotation of this element relative to its parent container
        /// </summary>
        Vector3 Rotation { get; set; }

        /// <summary>
        /// Rotation of the element relative to the world
        /// </summary>
        Vector3 WorldRotation { get; set; }

        /// <summary>
        /// The position of this element relative to its parent container
        /// </summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// Position of the element relative to the world
        /// </summary>
        Vector3 WorldPosition { get; set; }

        /// <summary>
        /// Gets the world scale
        /// </summary>
        Vector3 WorldScale { get; }

        /// <summary>
        /// Gets the absolute matrix
        /// </summary>
        Matrix4 Matrix { get; }
    }
}

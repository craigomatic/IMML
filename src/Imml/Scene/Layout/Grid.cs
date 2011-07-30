using System;
using System.Collections.Generic;
using System.Text;
using Imml.ComponentModel;
using Imml.Numerics;

namespace Imml.Scene.Layout
{
    /// <summary>
    ///  Defines uniformly sized cells for placement of elements. 
    /// </summary>
    /// <remarks>An undefined grid defaults to a size of 1,1,1 with 1 row, column and layer. Child elements added to the grid are limited in maximum size to the size of the cell they occupy. New elements are added to the next available cell in a column, row, layer order.</remarks>
    public class Grid : VisibleElement
    {
        #region Properties
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        /// <remarks>The number of horizontal segments to divide the grid into</remarks>
        public virtual int Rows { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        /// <remarks>The number of vertical segments to divide the grid into</remarks>
        public virtual int Columns { get; set; }

        /// <summary>
        /// Gets or sets the layers.
        /// </summary>
        /// <value>
        /// The layers.
        /// </value>
        /// <remarks>The number of depth segments to divide the grid into</remarks>
        public virtual int Layers { get; set; }

        protected Vector3 _Size;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public virtual Vector3 Size
        {
            get { return _Size; }
            set
            {
                if (_Size.X == value.X && _Size.Y == value.Y && _Size.Z == value.Z)
                    return;

                Vector3 oldValue = _Size;
                _Size = value;
                base.RaisePropertyChanged("Size", oldValue, _Size);
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid()
        {
            this.Rows = 1; 
            this.Columns = 1;
            this.Layers = 1;
            this.Size = new Vector3(1, 1, 1);
        }
    }
}

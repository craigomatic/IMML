﻿using Imml.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    /// <summary>
    /// Directs the document to include a resource from the specified source address.
    /// </summary>
    public class Include : ImmlElement, ISourcedElement
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual string Source { get; set; }
    }
}

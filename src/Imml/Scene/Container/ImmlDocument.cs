using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Numerics;
using Imml.Drawing;
using Imml.ComponentModel;

namespace Imml.Scene.Container
{
    /// <summary>
    /// The primary document container. Provides support for compositing a fully immersive scene, including the visual representation, logic interaction and simulation modifiers.
    /// </summary>
    public class ImmlDocument : ImmlElement, IImmlContext
    {
        #region Properties 

        private Uri _HostUri;

        /// <summary>
        /// Gets or sets the host URI.
        /// </summary>
        /// <value>
        /// The host URI.
        /// </value>
        public Uri HostUri
        {
            get { return _HostUri; }
            set
            {
                if (_HostUri == value)
                    return;

                _HostUri = value;
                base.RaisePropertyChanged("HostUri");
            }
        }

        private string _Camera;

        /// <summary>
        /// Gets or sets the camera.
        /// </summary>
        /// <value>
        /// The camera.
        /// </value>
        public string Camera
        {
            get { return _Camera; }
            set
            {
                if (_Camera == value)
                    return;

                string oldValue = _Camera;
                _Camera = value;

                base.RaisePropertyChanged("Camera", oldValue, _Camera);
            }
        }        

        private string _Author;

        /// <summary>
        /// Gets the author.
        /// </summary>
        public string Author
        {
            get { return _Author; }
            set
            {
                if (_Author == value)
                    return;

                _Author = value;
                base.RaisePropertyChanged("Author");
            }
        }

        private string _Description;

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description == value)
                    return;

                _Description = value;
                base.RaisePropertyChanged("Description");
            }
        }


        /// <summary>
        /// Gets the tags.
        /// </summary>
        public IList<string> Tags { get; set; }

        private string _Background;

        /// <summary>
        /// The name of the selected background
        /// </summary>
        public string Background
        {
            get { return _Background; }
            set
            {
                if (_Background == value)
                    return;

                string oldValue = _Background;
                _Background = value;
                base.RaisePropertyChanged("Background", oldValue, _Background);
            }
        }

        private float _PhysicsSpeed;

        /// <summary>
        /// Gets or sets the physics speed.
        /// </summary>
        /// <value>
        /// The physics speed.
        /// </value>
        public float PhysicsSpeed
        {
            get { return _PhysicsSpeed; }
            set
            {
                if (_PhysicsSpeed == value)
                    return;

                _PhysicsSpeed = value;
                base.RaisePropertyChanged("PhysicsSpeed");
            }
        }

        private float _SoundSpeed;

        /// <summary>
        /// Gets or sets the sound speed.
        /// </summary>
        /// <value>
        /// The sound speed.
        /// </value>
        public float SoundSpeed
        {
            get { return _SoundSpeed; }
            set
            {
                if (_SoundSpeed == value)
                    return;

                _SoundSpeed = value;
                base.RaisePropertyChanged("SoundSpeed");
            }
        }

        private float _AnimationSpeed;

        /// <summary>
        /// Gets or sets the animation speed.
        /// </summary>
        /// <value>
        /// The animation speed.
        /// </value>
        public float AnimationSpeed
        {
            get { return _AnimationSpeed; }
            set
            {
                if (_AnimationSpeed == value)
                    return;

                _AnimationSpeed = value;
                base.RaisePropertyChanged("AnimationSpeed");
            }
        }

        private Vector3 _Gravity;

        /// <summary>
        /// Gets or sets the gravity.
        /// </summary>
        /// <value>
        /// The gravity.
        /// </value>
        public Vector3 Gravity
        {
            get { return _Gravity; }
            set
            {
                if (_Gravity == value)
                    return;

                _Gravity = value;
                base.RaisePropertyChanged("Gravity");
            }
        }

        private Color3 _GlobalIllumination;

        /// <summary>
        /// Gets or sets the global illumination.
        /// </summary>
        /// <value>
        /// The global illumination.
        /// </value>
        public Color3 GlobalIllumination
        {
            get { return _GlobalIllumination; }
            set
            {
                if (value == null)
                    value = new Color3(0.4f, 0.4f, 0.4f);

                _GlobalIllumination = value;
                base.RaisePropertyChanged("GlobalIllumination");
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmlDocument"/> class.
        /// </summary>
        public ImmlDocument()
        {
            this.Behaviours = new List<string>();
            this.Tags = new List<string>();
            this.GlobalIllumination = new Color3("#4c4c4c");
            this.Gravity = new Vector3(0, -9.8f, 0);
            this.AnimationSpeed = 1;
            this.PhysicsSpeed = 1;
            this.SoundSpeed = 1;
        }
    }
}

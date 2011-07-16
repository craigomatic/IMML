using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Drawing;

namespace Imml.Scene
{
    public class Material : ImmlElement
    {
        #region Properties

        private Color3 _Ambient;

        /// <summary>
        /// Gets or sets the ambient.
        /// </summary>
        /// <value>
        /// The ambient.
        /// </value>
        public Color3 Ambient
        {
            get { return _Ambient; }
            set
            {
                if (value == null)
                    value = Material.Default.Ambient;

                if (_Ambient == value)
                {
                    return;
                }

                var oldValue = _Ambient;

                _Ambient = value;
                base.RaisePropertyChanged("Ambient", oldValue, value, "Material.Ambient");
            }
        }

        private Color3 _Diffuse;

        /// <summary>
        /// Gets or sets the diffuse.
        /// </summary>
        /// <value>
        /// The diffuse.
        /// </value>
        public Color3 Diffuse
        {
            get { return _Diffuse; }
            set
            {
                if (value == null)
                    value = Material.Default.Diffuse;

                if (_Diffuse == value)
                {
                    return;
                }

                var oldValue = _Diffuse;

                _Diffuse = value;
                base.RaisePropertyChanged("Diffuse", oldValue, value, "Material.Diffuse");
            }
        }

        private Color3 _Emissive;

        /// <summary>
        /// Gets or sets the emissive.
        /// </summary>
        /// <value>
        /// The emissive.
        /// </value>
        public Color3 Emissive
        {
            get { return _Emissive; }
            set
            {
                if (value == null)
                    value = Material.Default.Emissive;

                if (_Emissive == value)
                {
                    return;
                }

                var oldValue = _Emissive;

                _Emissive = value;
                base.RaisePropertyChanged("Emissive", oldValue, value, "Material.Emissive");
            }
        }

        private Color3 _Specular;

        /// <summary>
        /// Gets or sets the specular.
        /// </summary>
        /// <value>
        /// The specular.
        /// </value>
        public Color3 Specular
        {
            get { return _Specular; }
            set
            {
                if (value == null)
                    value = Material.Default.Specular;

                if (_Specular == value)
                {
                    return;
                }

                var oldValue = _Specular;

                _Specular = value;
                base.RaisePropertyChanged("Specular", oldValue, value, "Material.Specular");
            }
        }

        private float _Power;

        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>
        /// The power.
        /// </value>
        public float Power
        {
            get { return _Power; }
            set
            {
                if (_Power != value)
                {
                    float oldValue = _Power;
                    _Power = value;
                    base.RaisePropertyChanged("Power", oldValue, _Power, "Material.Power");
                }
            }
        }

        private float _Opacity;

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        /// <remarks>Opacity is a value between 0 and 1 which represents how opaque the entity will appear. 0 is fully transparent and 1 is fully opaque.</remarks>
        public float Opacity
        {
            get { return _Opacity; }
            set
            {
                if (_Opacity != value)
                {
                    float oldValue = _Opacity;
                    _Opacity = value;
                    base.RaisePropertyChanged("Opacity", oldValue, _Opacity, "Material.Opacity");
                }
            }
        }

        private float _AlphaThreshold;


        /// <summary>
        /// Gets or sets the alpha threshold.
        /// </summary>
        /// <value>
        /// The alpha threshold.
        /// </value>
        /// <remarks>the alpha threshold is a value between 0 and 1 which represents the alpha threshold for this material</remarks>
        public float AlphaThreshold
        {
            get { return _AlphaThreshold; }
            set
            {
                if (_AlphaThreshold != value)
                {
                    float oldValue = _AlphaThreshold;
                    _AlphaThreshold = value;
                    base.RaisePropertyChanged("AlphaThreshold", oldValue, _AlphaThreshold, "Material.AlphaThreshold");
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets the default material.
        /// </summary>
        public static Material Default
        {
            get
            {
                return new Material(new Color3(0.3f, 0.3f, 0.3f), new Color3(0.8f, 0.8f, 0.8f), new Color3(0, 0, 0), new Color3(0, 0, 0), 1, 1);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Material"/> class.
        /// </summary>
        public Material()
        {
            this.AlphaThreshold = 0.5f;
            this.Ambient = new Color3(0.3f, 0.3f, 0.3f);
            this.Diffuse = new Color3(0.8f, 0.8f, 0.8f);
            this.Emissive = new Color3(0, 0, 0);
            this.Specular = new Color3(0, 0, 0);
            this.Opacity = 1;
            this.Power = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Material"/> class.
        /// </summary>
        /// <param name="ambient">The ambient.</param>
        /// <param name="diffuse">The diffuse.</param>
        /// <param name="emissive">The emissive.</param>
        /// <param name="specular">The specular.</param>
        /// <param name="power">The power.</param>
        /// <param name="opacity">The opacity.</param>
        public Material(Color3 ambient, Color3 diffuse, Color3 emissive, Color3 specular, float power, float opacity)
        {
            this.Ambient = ambient;
            this.Diffuse = diffuse;
            this.Emissive = emissive;
            this.Specular = specular;
            this.Power = power;
            this.Opacity = opacity;
        }
    }
}

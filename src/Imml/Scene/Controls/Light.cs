using System;
using System.Collections.Generic;
using System.Text;
using Imml.Drawing;
using Imml.ComponentModel;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// A source of illumination with three different modes: point, spot and directional.
    /// </summary>
    public class Light : PositionalElement
    {
        #region Properties

        protected bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Light"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled != value)
                {
                    _Enabled = value;
                    base.RaisePropertyChanged("Enabled", !_Enabled, _Enabled);
                }
            }
        }

        protected bool _CastShadows;

        /// <summary>
        /// Gets or sets a value indicating whether the light should cast shadows.
        /// </summary>
        /// <value>
        ///   <c>true</c> if shadows should be cast; otherwise, <c>false</c>.
        /// </value>
        public virtual bool CastShadows
        {
            get { return _CastShadows; }
            set
            {
                if (_CastShadows == value)
                    return;

                _CastShadows = value;
                base.RaisePropertyChanged("CastShadows", !_CastShadows, _CastShadows);
            }
        }

        protected float _InnerCone;

        /// <summary>
        /// Gets or sets the inner cone.
        /// </summary>
        /// <value>
        /// The inner cone.
        /// </value>
        /// <remarks>The angle in degrees of the inner cone. This value must be in the range from 0 through the value specified by OuterCone. Only valid for lights of type Spot.</remarks>
        public virtual float InnerCone
        {
            get { return _InnerCone; }
            set
            {
                if (_InnerCone != value)
                {
                    object oldValue = _InnerCone;
                    _InnerCone = value;
                    base.RaisePropertyChanged("InnerCone", oldValue, _InnerCone);
                }
            }
        }

        protected float _OuterCone;

        /// <summary>
        /// Gets or sets the outer cone.
        /// </summary>
        /// <value>
        /// The outer cone.
        /// </value>
        /// <remarks>The angle in degrees of the outer cone. Points outside this cone are not lit. This value must be between 0 and pi. Only valid for lights of type Spot.</remarks>
        public virtual float OuterCone
        {
            get { return _OuterCone; }
            set
            {
                if (_OuterCone != value)
                {
                    object oldValue = _OuterCone;
                    _OuterCone = value;
                    base.RaisePropertyChanged("OuterCone", oldValue, _OuterCone);
                }
            }
        }

        protected float _ConstantAttenuation;

        /// <summary>
        /// Gets or sets the constant attenuation.
        /// </summary>
        /// <value>
        /// The constant attenuation.
        /// </value>
        /// <remarks>Constant attenuation value specifying how the light intensity changes over distance. Valid values between 0 and infinity. Does not affect lights of type Directional.</remarks>
        public virtual float ConstantAttenuation
        {
            get { return _ConstantAttenuation; }
            set
            {
                if (_ConstantAttenuation != value)
                {
                    object oldValue = _ConstantAttenuation;
                    _ConstantAttenuation = value;
                    base.RaisePropertyChanged("ConstantAttenuation", oldValue, _ConstantAttenuation);
                }
            }
        }

        protected float _LinearAttenuation;

        /// <summary>
        /// Gets or sets the linear attenuation.
        /// </summary>
        /// <value>
        /// The linear attenuation.
        /// </value>
        /// <remarks>Linear attenuation value specifying how the light intensity changes over distance. Valid values between 0 and infinity. Does not affect lights of type Directional.</remarks>
        public virtual float LinearAttenuation
        {
            get { return _LinearAttenuation; }
            set
            {
                if (_LinearAttenuation != value)
                {
                    object oldValue = _LinearAttenuation;
                    _LinearAttenuation = value;
                    base.RaisePropertyChanged("LinearAttenuation", oldValue, _LinearAttenuation);
                }
            }
        }

        protected float _QuadraticAttenuation;

        /// <summary>
        /// Gets or sets the quadratic attenuation.
        /// </summary>
        /// <value>
        /// The quadratic attenuation.
        /// </value>
        /// <remarks>Quadratic attenuation value specifying how the light intensity changes over distance. Valid values between 0 and infinity. Does not affect lights of type Directional.</remarks>
        public virtual float QuadraticAttenuation
        {
            get { return _QuadraticAttenuation; }
            set
            {
                if (_QuadraticAttenuation != value)
                {
                    object oldValue = _QuadraticAttenuation;
                    _QuadraticAttenuation = value;
                    base.RaisePropertyChanged("QuadraticAttenuation", oldValue, _QuadraticAttenuation);
                }
            }
        }

        protected float _Range;

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        /// <remarks>Distance beyond which the light has no effect. Does not affect lights of type Directional.</remarks>
        public virtual float Range
        {
            get { return _Range; }
            set
            {
                if (_Range != value)
                {
                    object oldValue = _Range;
                    _Range = value;
                    base.RaisePropertyChanged("Range", oldValue, _Range);
                }
            }
        }

        protected float _Falloff;

        /// <summary>
        /// Gets or sets the falloff.
        /// </summary>
        /// <value>
        /// The falloff.
        /// </value>
        /// <remarks>Decrease in illumination between a spotlight's InnerCone and the outer edge of the OuterCone. Only valid for lights of type Spot.</remarks>
        public virtual float Falloff
        {
            get { return _Falloff; }
            set
            {
                if (_Falloff != value)
                {
                    object oldValue = _Falloff;
                    _Falloff = value;
                    base.RaisePropertyChanged("Falloff", oldValue, _Falloff);
                }
            }
        }

        protected LightType _Type;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public virtual LightType Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    object oldType = _Type;
                    _Type = value;
                    base.RaisePropertyChanged("Type", oldType, _Type);
                }
            }
        }

        protected Color3 _Diffuse;

        /// <summary>
        /// Gets or sets the diffuse colour.
        /// </summary>
        /// <value>
        /// The diffuse.
        /// </value>
        public virtual Color3 Diffuse
        {
            get { return _Diffuse; }
            set
            {
                if (value == null)
                    value = new Color3("#FFFFFF");

                if (_Diffuse == value)
                    return;

                object oldDiffuse = _Diffuse;
                _Diffuse = value;
                
                base.RaisePropertyChanged("Diffuse", oldDiffuse, _Diffuse);
            }
        }

        protected Color3 _Specular;

        /// <summary>
        /// Gets or sets the specular colour.
        /// </summary>
        /// <value>
        /// The specular.
        /// </value>
        public virtual Color3 Specular
        {
            get { return _Specular; }
            set
            {
                if (value == null)
                    value = new Color3("#FFFFFF");

                if (_Specular == value)
                    return;

                object oldSpecular = _Specular;
                _Specular = value;

                base.RaisePropertyChanged("Specular", oldSpecular, _Specular);
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// </summary>
        public Light()
        {
            _CastShadows = true;
            _Enabled = true;
            _Range = 20;
            _Type = LightType.Point;
        }
    }
}

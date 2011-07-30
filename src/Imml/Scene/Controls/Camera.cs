using System;
using System.Collections.Generic;
using System.Text;
using Imml.ComponentModel;
using Imml.Numerics;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Defines a view into the scene. Supports chasing positional elements.
    /// </summary>
    public class Camera : PositionalElement
    {
        #region Properties

        protected bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Camera"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>If a valid ChaseTarget has been specified, enables the camera chase functionality</remarks>
        public virtual bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled == value)
                    return;

                _Enabled = value;

                base.RaisePropertyChanged("Enabled", !_Enabled, _Enabled);
            }
        }

        protected float _FOV;

        /// <summary>
        /// Gets or sets the FOV.
        /// </summary>
        /// <value>
        /// The FOV.
        /// </value>
        public virtual float FOV
        {
            get { return _FOV; }
            set
            {
                if (_FOV == value)
                    return;

                float oldValue = _FOV;
                _FOV = value;
                base.RaisePropertyChanged("FOV", oldValue, _FOV);
            }
        }

        protected string _ChaseTarget;

        /// <summary>
        /// Gets or sets the chase target.
        /// </summary>
        /// <value>
        /// The chase target.
        /// </value>
        /// <remarks>The name of the element in the current document context to chase.</remarks>
        public virtual string ChaseTarget
        {
            get { return _ChaseTarget; }
            set
            {
                if (_ChaseTarget == value)
                    return;

                string oldValue = _ChaseTarget;
                _ChaseTarget = value;
                base.RaisePropertyChanged("ChaseTarget", oldValue, _ChaseTarget);
            }
        }

        protected Vector3 _ChasePositionOffset;

        /// <summary>
        /// Gets or sets the chase position offset.
        /// </summary>
        /// <value>
        /// The chase position offset.
        /// </value>
        /// <remarks>Desired camera position in the chased element's coordinate system.</remarks>
        public virtual Vector3 ChasePositionOffset
        {
            get { return _ChasePositionOffset; }
            set
            {
                if (_ChasePositionOffset == value)
                    return;

                Vector3 oldValue = _ChasePositionOffset;
                _ChasePositionOffset = value;
                base.RaisePropertyChanged("ChasePositionOffset", oldValue, _ChasePositionOffset);
            }
        }

        protected Vector3 _ChaseLookAtOffset;

        /// <summary>
        /// Gets or sets the chase look at offset.
        /// </summary>
        /// <value>
        /// The chase look at offset.
        /// </value>
        /// <remarks>The point in the chased element's coordinate system to look at.</remarks>
        public virtual Vector3 ChaseLookAtOffset
        {
            get { return _ChaseLookAtOffset; }
            set
            {
                if (_ChaseLookAtOffset == value)
                    return;

                Vector3 oldValue = _ChaseLookAtOffset;
                _ChaseLookAtOffset = value;
                base.RaisePropertyChanged("ChaseLookAtOffset", oldValue, _ChaseLookAtOffset);
            }
        }

        protected float _ChaseDamping;

        /// <summary>
        /// Gets or sets the chase damping.
        /// </summary>
        /// <value>
        /// The chase damping.
        /// </value>
        /// <remarks>Physics coefficient which approximates internal friction of the spring.</remarks>
        public virtual float ChaseDamping
        {
            get { return _ChaseDamping; }
            set
            {
                if (_ChaseDamping == value)
                    return;

                float oldValue = _ChaseDamping;
                _ChaseDamping = value;
                base.RaisePropertyChanged("ChaseDamping", oldValue, _ChaseDamping);
            }
        }

        protected float _ChaseStiffness;

        /// <summary>
        /// Gets or sets the chase stiffness.
        /// </summary>
        /// <value>
        /// The chase stiffness.
        /// </value>
        /// <remarks>Physics coefficient which controls the influence of the camera's position over the spring force. The stiffer the spring, the closer it will stay to the chased object.</remarks>
        public virtual float ChaseStiffness
        {
            get { return _ChaseStiffness; }
            set
            {
                if (_ChaseStiffness == value)
                    return;

                float oldValue = _ChaseStiffness;
                _ChaseStiffness = value;
                base.RaisePropertyChanged("ChaseStiffness", oldValue, _ChaseStiffness);
            }
        }

        protected float _ChaseMass;

        /// <summary>
        /// Gets or sets the chase mass.
        /// </summary>
        /// <value>
        /// The chase mass.
        /// </value>
        /// <remarks>The amount of mass to simulate the camera has when chasing.</remarks>
        public virtual float ChaseMass
        {
            get { return _ChaseMass; }
            set
            {
                if (_ChaseMass == value)
                    return;

                float oldValue = _ChaseMass;
                _ChaseMass = value;
                base.RaisePropertyChanged("ChaseMass", oldValue, _ChaseMass);
            }
        }

        protected ProjectionType _Projection;

        /// <summary>
        /// Gets or sets the projection.
        /// </summary>
        /// <value>
        /// The projection.
        /// </value>
        public virtual ProjectionType Projection
        {
            get { return _Projection; }
            set
            {
                if (_Projection == value)
                    return;

                ProjectionType oldValue = _Projection;
                _Projection = value;
                base.RaisePropertyChanged("Projection", oldValue, _Projection);
            }
        }

        protected float _NearPlane;

        /// <summary>
        /// Gets or sets the near plane.
        /// </summary>
        /// <value>
        /// The near plane.
        /// </value>
        /// <remarks>The near clipping distance from camera. Anything within this distance of the camera will not be rendered.</remarks>
        public virtual float NearPlane
        {
            get
            {
                return _NearPlane;
            }

            set
            {
                if (_NearPlane == value)
                    return;

                float oldValue = _NearPlane;
                _NearPlane = value;
                base.RaisePropertyChanged("NearPlane", oldValue, _NearPlane);
            }
        }

        protected float _FarPlane;

        /// <summary>
        /// Gets or sets the far plane.
        /// </summary>
        /// <value>
        /// The far plane.
        /// </value>
        /// <remarks>The far clipping distance from camera. Anything beyond this distance from the camera will not be rendered.</remarks>
        public virtual float FarPlane
        {
            get
            {
                return _FarPlane;
            }

            set
            {
                if (_FarPlane == value)
                    return;

                float oldValue = _FarPlane;
                _FarPlane = value;
                base.RaisePropertyChanged("FarPlane", oldValue, _FarPlane);
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera()
        {
            this.FOV = 60;
            this.Projection = ProjectionType.Perspective;
            this.NearPlane = 0.01f;
            this.FarPlane = 1000f;
        }
    }
}

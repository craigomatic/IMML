using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml
{
    public enum ProjectionType
    {
        Isometric,
        Perspective
    }

    public enum LightingMode
    {
        Bumpmap,
        Managed,
        None,
        Normal
    }

    public enum ConditionSource
    {
        EventData,
        Element
    }

    public enum ConditionType
    {
        Allow,
        Ignore
    }

    public enum KeyFrameType
    {
        Position,
        Rotation,
        Size
    }

    public enum BoundingType
    {
        Box,
        ConvexHull,
        Cylinder,
        Sphere
    }

    public enum InterpolationType
    {
        Linear,
        Bezier,
        Spline
    }

    public enum HorizontalAlignment
    {
        Left,
        Centre,
        Right,
        Stretch
    }

    public enum VerticalAlignment
    {
        Top,
        Centre,
        Bottom,
        Stretch
    }

    public enum TextureType
    {
        Ambient,
        Diffuse,
        Emissive,
        Specular,
        AmbientOcclusion,
        Normal
    }

    public enum PrimitiveType
    {
        Box,
        Cone,
        Cylinder,
        Plane,
        Sphere
    }

    public enum PrimitiveComplexity
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh
    }

    public enum ElementType
    {
        All,
        Anchor,
        Background,
        Camera,
        Dock,
        Effect,
        Grid,
        Light,
        Model,
        Plugin,        
        Primitive,
        Script,
        Shader,
        Sound,
        Stack,
        Text,
        Timeline,
        Trigger,
        Video,
        Web,
        Widget
    }

    public enum EventType
    {
        Collided,          
        DragEnter,
        DragDrop,
        DragLeave,
        KeyDown,
        KeyPress,
        KeyUp,
        Loaded,
        MouseClick,
        MouseDown,
        MouseEnter,
        MouseHover,
        MouseLeave,
        MouseMove,
        MouseUp,
        NetworkCustomMessage,
        NetworkConnectionEstablished,
        NetworkConnectionDropped,
        NetworkConnectionRejected,
        ProximityHover,
        ProximityEnter,
        ProximityLeave,
        Unloaded,
        AnimationFinished,
        SoundFinished
    }

    public enum RenderMode
    {
        Fill,
        Line,
        Point
    }

    public enum LightType
    {
        Directional,
        Point,
        Spot
    }

    public enum SpeakerType
    {
        Stereo,
        Surround,
        Quad,
        Prologic,
        FivePointOne,
        SevenPointOne
    }

    public enum AnchorType
    {
        Geometric,
        NonGeometric
    }

    public enum TextAlignment
    {
        /// <summary>
        /// Aligns the text to the left margin
        /// </summary>
        Left,

        /// <summary>
        /// Aligns the text to the centre
        /// </summary>
        Centre,

        /// <summary>
        /// Aligns the text to the right margin
        /// </summary>
        Right,

        /// <summary>
        /// Aligns the text to both the left and the right margins, adding extra space between words as necessary
        /// </summary>
        Justify
    }

    public enum ParticleChangeType
    {
        Movement,
        Rotation,
        Drag,
        Size,
        Red,
        Green,
        Blue,
        Opacity
    }
}

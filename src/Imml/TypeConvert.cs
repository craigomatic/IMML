//-----------------------------------------------------------------------
//VastPark is a lightweight extensible virtual world platform 
//and this file is a program released under the GPL.
//Copyright (C) 2009 VastPark
//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Scene;
using System.Globalization;
using Imml.Numerics;
using Imml.Drawing;

namespace Imml
{
    /// <summary>
    /// Converts a number of known types from a one representation into another
    /// </summary>
    public static class TypeConvert
    {
        public static string Parse(object toConvert)
        {
            if (toConvert is List<string>)
                return _ProcessListString(toConvert);
            else if (toConvert is Vector3)
                return _ProcessVectorString(toConvert);
            else if (toConvert is DateTime)
                return ((DateTime)toConvert).ToString("s");

            return Convert.ToString(toConvert);
        }

        /// <summary>
        /// Converts the object to an optimised byte representation
        /// </summary>
        /// <param name="toConvert"></param>
        /// <returns></returns>
        public static byte[] ToBytes(object toConvert)
        {
            if (toConvert is Vector3)
                return TypeConvert.Vector3ToBytes((Vector3)toConvert);
            else if (toConvert is bool)
                return TypeConvert.BoolToTypes((bool)toConvert);
            else
                return Encoding.UTF8.GetBytes(TypeConvert.Parse(toConvert));
        }

        /// <summary>
        /// Returns an object given the byte array and destination type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public static object Parse(byte[] value, Type destinationType)
        {
            if (destinationType == typeof(Vector3))
                return TypeConvert.BytesToVector3(value);
            else if (destinationType == typeof(bool))
                return TypeConvert.BytesToBool(value);
            else
                return TypeConvert.Parse(Encoding.UTF8.GetString(value, 0, value.Length), destinationType);

        }

        private static bool BytesToBool(byte[] value)
        {
            return BitConverter.ToBoolean(value, 0);
        }

        private static byte[] BoolToTypes(bool value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] Vector3ToBytes(Vector3 vector)
        {
            byte[] x = BitConverter.GetBytes(vector.X);
            byte[] y = BitConverter.GetBytes(vector.Y);
            byte[] z = BitConverter.GetBytes(vector.Z);

            return _MergeByteArrays(_MergeByteArrays(x, y), z);
        }

        private static byte[] _MergeByteArrays(byte[] array1, byte[] array2)
        {
            int length = array1.Length + array2.Length;
            byte[] sum = new byte[length];
            array1.CopyTo(sum, 0);
            array2.CopyTo(sum, array1.Length);
            return sum;
        }

        /// <summary>
        /// Takes 12 bytes and converts it into a Vector3 struct.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3 BytesToVector3(byte[] value)
        {
            if (value.Length != 12)
                throw new Exception("Incorrect byte array size: " + value.Length + " passed into the Vector3 converter");

            return new Vector3(BitConverter.ToSingle(value, 0), BitConverter.ToSingle(value, 4), BitConverter.ToSingle(value, 8));
        }

        private static string _ProcessVectorString(object toConvert)
        {
            Vector3 v = (Vector3)toConvert;
            return v.ToString();
        }

        private static string _ProcessListString(object toConvert)
        {
            List<string> list = (List<string>)toConvert;

            string toReturn = string.Empty;

            for (int i = 0; i < list.Count; i++)
            {
                toReturn += list[i];

                if (i != list.Count - 1)
                    toReturn += ",";
            }

            return toReturn;
        }


        /// <summary>
        /// Parse the given value into the destination type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Parse(string value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return value;

            if (string.IsNullOrEmpty(value))
                return null; //no convert

            //wasnt a string so process the type to find the appropriate typeconverter
            if (destinationType == typeof(IList<string>))
                return _ProcessListString(value);
            else if (destinationType == typeof(Vector3))
                return _ProcessVectorString(value);
            else if (destinationType == typeof(bool))
                return Convert.ToBoolean(value);
            else if (destinationType == typeof(RenderMode))
                return (RenderMode)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(LightType))
                return (LightType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(AnchorType))
                return (AnchorType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(BoundingType))
                return (BoundingType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(EventType))
                return (EventType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(PrimitiveType))
                return (PrimitiveType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(HorizontalAlignment))
                return (HorizontalAlignment)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(VerticalAlignment))
                return (VerticalAlignment)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(PrimitiveComplexity))
                return (PrimitiveComplexity)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(TextureType))
                return (TextureType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(KeyFrameType))
                return (KeyFrameType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(ConditionType))
                return (ConditionType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(ConditionSource))
                return (ConditionSource)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(float))
                return Convert.ToSingle(value);
            else if (destinationType == typeof(double))
                return Convert.ToDouble(value);
            else if (destinationType == typeof(int))
                return Convert.ToInt32(value);
            else if (destinationType == typeof(Color3))
                return _ProcessRGBString(value);
            else if (destinationType == typeof(TimeSpan))
                return TimeSpan.Parse(value);
            else if (destinationType == typeof(ProjectionType))
                return (ProjectionType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(TextAlignment))
                return (TextAlignment)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(ParticleChangeType))
                return (ParticleChangeType)Enum.Parse(destinationType, value, true);
            else if (destinationType == typeof(Uri))
            {
                if (string.IsNullOrEmpty(value))
                    return null;
                else
                    try
                    {
                        return new Uri(value);
                    }
                    catch { return null; }
            }
            else if (destinationType == typeof(DateTime))
            {
                //different cultures will have different time formats. 
                try
                {
                    //First try to parse as the current culture
                    return Convert.ToDateTime(value);
                }
                catch
                {
                    //parse as en-us (the WS uses en-us in the date format returned)
                    return DateTime.Parse(value, new CultureInfo("en-us"));
                }
            }
            System.Diagnostics.Debug.Assert(false, "Need to update the TypeConvert class for type: " + destinationType.Name);
            throw new TypeLoadException("Failed to convert the string: " + value + " to type: " + destinationType.Name);
        }

        private static object _ProcessRGBString(string value)
        {
            if (value.StartsWith("#"))
                return new Color3(value);

            string[] split = value.Split(',');

            if (split.Length != 3)
                return null;

            return new Color3(Convert.ToSingle(split[0]),
                Convert.ToSingle(split[1]),
                Convert.ToSingle(split[2]));
        }

        private static object _ProcessVectorString(string value)
        {
            string[] split = value.Split(',');

            if (split.Length != 3)
                return null;

            return new Vector3(Convert.ToSingle(split[0]),
                Convert.ToSingle(split[1]),
                Convert.ToSingle(split[2]));
        }

        private static object _ProcessListString(string value)
        {
            List<string> toReturn = new List<string>();
            string[] split = value.Split(',');

            foreach (string s in split)
            {
                if (!string.IsNullOrEmpty(s))
                    toReturn.Add(s.Trim());
            }

            return toReturn;
        }
    }
}

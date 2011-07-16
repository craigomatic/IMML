using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Imml.Test
{
    /// <summary>
    /// Provides methods for working with embedded resources
    /// </summary>
    public static class EmbeddedResourceHelper
    {
        /// <summary>
        /// Gets a byte array representing the embedded resource
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string resourcePath, Assembly assembly)
        {
            var ms = EmbeddedResourceHelper.GetMemoryStream(resourcePath, assembly);

            if (ms == null)
                return null;

            return ms.ToArray();
        }

        /// <summary>
        /// Gets a MemoryStream representing the embedded resource
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static MemoryStream GetMemoryStream(string resourcePath, Assembly assembly)
        {
            var stream = assembly.GetManifestResourceStream(resourcePath);

            if (stream == null)
                return null;

            try
            {
                MemoryStream ms = new MemoryStream();
                byte[] buffer = new byte[1024];
                int bytesRead = 0;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    ms.Write(buffer, 0, bytesRead);
                }

                return ms;
            }
            catch
            {
                return null;
            }
        }
    }
}

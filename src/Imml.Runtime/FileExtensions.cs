using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Runtime
{
    public static class FileExtensions
    {
        public static string FileExtension(this string fileUri)
        {
            return fileUri.Substring(fileUri.LastIndexOf('.'), fileUri.Length - fileUri.LastIndexOf('.'));
        }
    }
}

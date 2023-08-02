using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiXSLT.Data
{
    public static class XmlHelper
    {
        public static Stream GetEmbeddedResourceStream(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}

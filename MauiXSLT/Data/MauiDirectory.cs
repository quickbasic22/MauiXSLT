using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiXSLT.Data
{
    public static class MauiDirectory
    {
        public static string WriteToFileSystem(string filename)
        {
            var assembly = Application.Current.GetType().Assembly;
            var resourceName1 = "MauiXSLT.Resources.Raw.";
            resourceName1 = string.Concat(resourceName1, filename);
            var AppDataFileName = Path.Combine(FileSystem.AppDataDirectory, filename);
            try
            { 
                using (var stream = assembly.GetManifestResourceStream(AppDataFileName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            File.WriteAllText(AppDataFileName, reader.ReadToEnd());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return AppDataFileName;
        }

        public static string GetAppDataDirFileName(string filename)
        {
            return Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}

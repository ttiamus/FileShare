using System;
using FileShare.Common.Configuration;

namespace FileShare.Directory
{
    public class DirectoryConfiguration : IConfiguration
    {
        public string FileSaveLocation
        {
            get
            {
                //Use reflection to get the permanant path of the executing dll
                var path = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;

                //trim off the last \bin found in the above path. This should get you the root application folder
                path = path.Substring(0, path.LastIndexOf(@"\bin", StringComparison.InvariantCultureIgnoreCase));

                path = $@"{path}\App_Data\uploads";
                return path;
            }
        }

    }
}
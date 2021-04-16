using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Shared.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        ///  Indicates whether the Assembly has been compiled in "Release" mode.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool IsReleaseMode(this Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(DebuggableAttribute), true);
            if (attributes == null || attributes.Length == 0)
            {
                return true;
            }

            DebuggableAttribute debug = (DebuggableAttribute)attributes[0];
            return (debug.DebuggingFlags & DebuggableAttribute.DebuggingModes.Default) == DebuggableAttribute.DebuggingModes.None;
        }


        /// <summary>
        /// Get assembly file size in bytes
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static long GetAssemblyFileSize(this Assembly assembly)
        {
            long fileSize = 0;
                if (assembly != null && !assembly.IsDynamic)
                {
                    FileStream[] fileStreams = assembly.GetFiles();

                    if (fileStreams?.Length > 0)
                    {
                        fileSize = fileStreams[0]?.Length ?? 0;
                    }
                }
   
            return fileSize;
        }

        /// <summary>
        /// Get build date on the assembly by reading file header
        /// </summary>
        public static DateTime GetBuildDate(this Assembly assembly)
        {
            DateTime buildDate = new DateTime(1970, 1, 1, 0, 0, 0);

                if (assembly != null && !assembly.IsDynamic)
                {
                    string location = assembly.Location;
                    if (!location.IsNullOrEmpty())
                    {

                        byte[] b = new byte[2048];
                        if (File.Exists(location))
                        {
                            using (FileStream stream = new FileStream(location, FileMode.Open, FileAccess.Read))
                            {
                                stream.Read(b, 0, 2048);
                                stream.Close();
                            }

                            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(b, BitConverter.ToInt32(b, 60) + 8));
                            buildDate = dateTime.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dateTime).Hours);
                        }
                    }
                }
            return buildDate;
        }

    }
}

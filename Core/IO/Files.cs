using System.Collections.Generic;
using System.IO;

namespace Core.IO
{
    /// <summary>
    /// Utility class used to control files
    /// </summary>
    public sealed class Files
    {

        public static List<FileInfo> FilesOf(DirectoryInfo fileName)
        {
            return null;
        }

        public static List<DirectoryInfo> DirecotriesOf(DirectoryInfo directory)
        {
            directory.GetFileSystemInfos();
            var dds = new List<DirectoryInfo>();
            dds.AddRange(directory.GetDirectories());
            foreach (var d in directory.GetDirectories())
            {
                dds.AddRange(DirecotriesOf(d));
            }
            return dds;
        }

    }

}
namespace Library.FileHelpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The io stream.
    /// </summary>
    public class IOStream
    {
        #region File

        /// <summary>
        /// The save stream to file.
        /// </summary>
        /// <param name="fileFullPath">
        /// The file full path.
        /// </param>
        /// <param name="stream">
        /// The stream.
        /// </param>
        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0)
            {
                return;
            }

            // Create a FileStream object to write a stream to a file
            using (var fileStream = File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                var bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        /// <summary>
        /// The save byte array to file.
        /// </summary>
        /// <param name="fileFullPath">
        /// The file full path.
        /// </param>
        /// <param name="byteArray">
        /// The _byte array.
        /// </param>
        public static void SaveByteArrayToFile(string fileFullPath, byte[] byteArray)
        {
            try
            {
                // Open file for reading
                var fileStream = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                fileStream.Write(byteArray, 0, byteArray.Length);

                // close file stream
                fileStream.Close();
            }
            catch (Exception ex)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
        }
        #endregion

        #region Folder

        /// <summary>
        /// The show directories.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The list folder name
        /// </returns>
        public static List<string> ShowDirectories(string path)
        {
            var dir = new DirectoryInfo(path);
            
            return dir.GetDirectories().Select(info => info.Name).ToList();
        }

        /// <summary>
        /// The get file in folder.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="extension">
        /// The extension.
        /// </param>
        /// <param name="fullName">
        /// The full name.
        /// </param>
        /// <returns>
        /// The list file name
        /// </returns>
        public static List<string> GetFileInFolder(string path, string extension, bool fullName = false)
        {
            var dir = new DirectoryInfo(path);

            return dir.GetFiles("*." + extension).Select(info => fullName ? info.Name : info.Name.Split('.')[0]).ToList();
        }

        /// <summary>
        /// The create folder.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public static void CreateFolder(string path)
        {
            var folders = path.Replace("\\", "/").Replace("//", "/").Split('/');
            if (!Directory.Exists(folders[0]))
            {
                return; // neu ko ton tai o dia ay thi thoat luon
            }

            for (var i = 1; i < folders.Length; i++)
            {
                if (string.IsNullOrEmpty(folders[i]))
                {
                    break;
                }

                var folderPath = string.Empty;
                for (var j = 0; j < i; j++)
                {
                    folderPath += folders[j] + "\\";
                }

                folderPath += folders[i];
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
        }

        /// <summary>
        /// The get folder size.
        /// </summary>
        /// <param name="path">
        /// The folder path.
        /// </param>
        /// <returns>
        /// folder size
        /// </returns>
        public static long GetFolderSize(string path)
        {
            var di = new DirectoryInfo(path);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        /// <summary>
        /// The copy folder.
        /// </summary>
        /// <param name="sourceDirName">
        /// The source dir name.
        /// </param>
        /// <param name="destDirName">
        /// The dest dir name.
        /// </param>
        /// <param name="copySubDirs">
        /// The copy sub dirs.
        /// </param>
        /// <exception cref="DirectoryNotFoundException">
        /// </exception>
        public static void CopyFolder(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    CopyFolder(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        /// <summary>
        /// Delete a directory, include all files and directory in it.
        /// </summary>
        /// <param name="DirectoryPath">The path of directory</param>
        public static void RemoveDirectory(string DirectoryPath, bool Recursive)
        {
            if (Directory.Exists(DirectoryPath))
                Directory.Delete(DirectoryPath, Recursive);
        }
        #endregion
    }
}

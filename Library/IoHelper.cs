namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Cung cấp các hàm xử lý thường dùng cho file, folder.
    /// </summary>
    public class IoHelper
    {
        /// <summary>
        /// Tạo thư mục (kể cả thư mục con) cho đường dẫn truyền vào.
        /// </summary>
        /// <param name="path">
        /// The path to create.
        /// </param>
        public static void CreateIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// The get base 64 from file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetBase64FromFile(string fileName)
        {
            string base64FileString;
            using (var file = File.Open(fileName, FileMode.Open))
            {
                var buffers = new byte[file.Length];
                file.Read(buffers, 0, (int)file.Length);
                base64FileString = Convert.ToBase64String(buffers);
            }

            return base64FileString;
        }

        /// <summary>
        /// The get base 64 from file.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetBase64FromStream(Stream stream)
        {
            // Set the position of stream to beginning
            stream.Seek(0, SeekOrigin.Begin);

            var buffers = new byte[stream.Length];
            stream.Read(buffers, 0, (int)stream.Length);
            return Convert.ToBase64String(buffers);
        }

        /// <summary>
        /// The write base 64 string to file.
        /// </summary>
        /// <param name="base64EncodedContent">
        /// The base 64 encoded content.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public static void WriteBase64StringToFile(string base64EncodedContent, string fileName)
        {
            using (var file = File.Create(fileName))
            {
                var buffers = Convert.FromBase64String(base64EncodedContent);
                file.Write(buffers, 0, buffers.Length);
            }
        }

        /// <summary>
        /// The get bytes.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// The get string.
        /// </summary>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// Copy all file and folder from source directory to destination directory.
        /// http://msdn.microsoft.com/en-us/library/bb762914.aspx
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
        /// Source directory does not exist or could not be found.
        /// </exception>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = false)
        {
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            CreateIfNotExists(destDirName);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, true);
                }
            }
        }

        /// <summary>
        /// Move all file and folder from source directory to destination directory.
        /// </summary>
        /// <param name="sourceDirName">
        /// The source dir name.
        /// </param>
        /// <param name="destDirName">
        /// The dest dir name.
        /// </param>
        /// <param name="moveSubDirs">
        /// The move sub dirs.
        /// </param>
        /// <param name="ignoreOnMoveFileError">
        /// Ignore error when move file.
        /// </param>
        /// <param name="preserveSubDirectory">
        /// The preserve Sub Directory.
        /// </param>
        /// <exception cref="DirectoryNotFoundException">
        /// Source directory does not exist or could not be found.
        /// </exception>
        public static void DirectoryMove(
            string sourceDirName, string destDirName, bool moveSubDirs = false, bool ignoreOnMoveFileError = false, bool preserveSubDirectory = true)
        {
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            if (moveSubDirs)
            {
                foreach (var subdir in dirs)
                {
                    var newPath = preserveSubDirectory
                        ? Path.Combine(destDirName, subdir.Name)
                        : destDirName;
                    DirectoryMove(
                        subdir.FullName, newPath, true, ignoreOnMoveFileError, preserveSubDirectory);
                }
            }

            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var newFileName = Path.Combine(destDirName, file.Name);
                try
                {
                    file.MoveTo(newFileName);
                }
                catch (Exception)
                {
                    if (!ignoreOnMoveFileError)
                    {
                        throw;
                    }
                }
            }

            if (sourceDirName != destDirName)
            {
                Directory.Delete(sourceDirName);
            }
        }

        /// <summary>
        /// The move file.
        /// </summary>
        /// <param name="sourceFile">
        ///     The source file.
        /// </param>
        /// <param name="destionationFolder">
        ///     The destionation Folder.
        /// </param>
        /// <param name="defaultExt">
        ///     Default file extension if file missing extension.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string MoveFile(string sourceFile, string destionationFolder, string defaultExt = null)
        {
            if (sourceFile == null)
            {
                throw new ArgumentNullException("sourceFile");
            }

            if (destionationFolder == null)
            {
                throw new ArgumentNullException("destionationFolder");
            }

            var fileName = Path.GetFileNameWithoutExtension(sourceFile);
            Debug.Assert(fileName != null, "fileName != null");

            var fileExtension = Path.GetExtension(sourceFile);
            if (string.IsNullOrEmpty(fileExtension) && defaultExt != null)
            {
                fileExtension = defaultExt;
            }
            else
            {
                // Remove dot character before file extension
                if (fileExtension != null && fileExtension.StartsWith("."))
                {
                    fileExtension = fileExtension.Substring(1);
                }
            }

            var fileNameFormat = !string.IsNullOrEmpty(fileExtension) ? "{0}.{1}" : "{0}";
            var fileNameIndexFormat = !string.IsNullOrEmpty(fileExtension) ? "{0}_{1}.{2}" : "{0}_{1}";
            
            CreateIfNotExists(destionationFolder);
            var destFileName = Path.Combine(destionationFolder, string.Format(fileNameFormat, fileName, fileExtension));
            var fileIndex = 1;
            while (File.Exists(destFileName))
            {
                destFileName = Path.Combine(destionationFolder, string.Format(fileNameIndexFormat, fileName, fileIndex, fileExtension));
                fileIndex++;
            }

            File.Move(sourceFile, destFileName);
            return destFileName;
        }

        /// <summary>
        /// The get trailing path. Get path after root path exclude file name.
        /// Ex: Path: C:/ROOT/AHPM/File.edi RootPath: C:/ROOT
        /// Result: AHPM/
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="rootPath">
        /// The root path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Require parameter.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Path does not valid.
        /// </exception>
        public static string GetTrailingPath(string path, string rootPath)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (rootPath == null)
            {
                throw new ArgumentNullException("rootPath");
            }

            path = Path.GetFullPath(path);
            rootPath = Path.GetFullPath(rootPath);

            if (!path.StartsWith(rootPath))
            {
                throw new InvalidOperationException(
                    string.Format("Path '{0}' does not begin with '{1}'.", path, rootPath));
            }

            if (path.Length < rootPath.Length + 1)
            {
                return string.Empty;
            }

            return path.Substring(rootPath.Length + 1);
        }

        /// <summary>
        /// The move file.
        /// </summary>
        /// <param name="destionationFolder">
        /// The destionation folder.
        /// </param>
        /// <param name="defaultExt">
        /// The default ext.
        /// </param>
        /// <param name="sourceFiles">
        /// The source files.
        /// </param>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        public static string[] MoveManyFile(string destionationFolder, string defaultExt, params string[] sourceFiles)
        {
            var movedFiles = new List<string>();
            foreach (var sourceFile in sourceFiles)
            {
                movedFiles.Add(MoveFile(sourceFile, destionationFolder, defaultExt));
            }

            return movedFiles.ToArray();
        }

        /// <summary>
        /// The remove file extension folder.
        /// </summary>
        /// <param name="inputFolder">
        /// The input folder.
        /// </param>
        public static void RemoveFileExtensionFolder(string inputFolder)
        {
            foreach (var file in Directory.GetFiles(inputFolder))
            {
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(file);
                Debug.Assert(fileNameWithoutExt != null, "fileNameWithoutExt != null");
                File.Move(file, Path.Combine(inputFolder, fileNameWithoutExt));
            }
        }
    }
}

#region license

//Copyright 2009 Zack Owens

//Licensed under the Microsoft Public License (Ms-PL) (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://clubstarterkit.codeplex.com/license

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.IO
{
    internal static class FileExt
    {
        /// <summary>
        /// Reads the files and generates output
        /// </summary>
        /// <param name="files"></param>
        /// <returns>Array of output</returns>
        internal static string[] ToStringArray(this FileInfo[] files)
        {
            var ar = new string[files.Length];

            for (int i = 0; i < ar.Length; i++)
                ar[i] = ReadAll(new[] {files[i]});

            return ar;
        }

        #region GetFiles

        /// <summary>
        /// Gets files in a directory 
        /// with given extention
        /// </summary>
        /// <param name="directory">Path of directory</param>
        /// <param name="ext">File extention search pattern</param>
        /// <returns>List of files</returns>
        internal static FileInfo[] GetFiles(string directory, string ext)
        {
            var files = new List<FileInfo>(new DirectoryInfo(directory).GetFiles("*." + ext));

            return files.Where(x => !x.Extension.Contains("db")).ToArray();
        }

        /// <summary>
        /// Gets files in a directory
        /// with any file extention
        /// </summary>
        /// <param name="directory">Path of Directory</param>
        /// <returns>List of files</returns>
        internal static FileInfo[] GetFiles(string directory)
        {
            return GetFiles(directory, "*");
        }

        #endregion

        #region Read

        /// <summary>
        /// Reads the values of the files into a single string
        /// </summary>
        /// <param name="files">list of Files</param>
        /// <returns>concatenated values</returns>
        internal static string ReadAll(FileInfo[] files)
        {
            var builder = new StringBuilder();

            foreach (FileInfo file in files)
                using (var reader = new StreamReader(file.OpenRead()))
                    builder.Append(reader.ReadToEnd() + Environment.NewLine);

            return builder.ToString();
        }

        #endregion
    }
}
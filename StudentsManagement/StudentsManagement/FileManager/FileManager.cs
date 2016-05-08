using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentsManagement.FileManager
{
    public class FileManager : IFileManager
    {
        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }

        public void Move(string fromPath, string toPath)
        {
            File.Move(fromPath, toPath);
        }
    }
}
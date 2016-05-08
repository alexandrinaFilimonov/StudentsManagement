using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.FileManager
{
    public interface IFileManager
    {
        void Delete(string filePath);
        void Move(string fromPath, string toPath);
    }
}

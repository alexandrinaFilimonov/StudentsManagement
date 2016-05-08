using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentsManagement.Reader
{
    public class Writer : IWriter
    {
        private readonly StreamWriter writer;

        public Writer(string filePath, bool append)
        {
            if(append){
                writer = new StreamWriter(filePath, append);
            }
            else
            {
                writer = new StreamWriter(File.OpenWrite(filePath));
            }
        }

        public void WriteLine(string line)
        {
            writer.WriteLine(line);
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}
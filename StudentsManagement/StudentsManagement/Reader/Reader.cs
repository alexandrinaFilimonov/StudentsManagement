using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentsManagement.Reader
{
    public class Reader : IReader
    {
        private readonly StreamReader reader;

        public Reader(string filePath)
        {
            reader = new StreamReader(File.OpenRead(filePath));
        }

        public bool EndOfStream
        {
            get
            {
                return reader.EndOfStream;
            }
        }

        public string ReadLine()
        {
            return reader.ReadLine();
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
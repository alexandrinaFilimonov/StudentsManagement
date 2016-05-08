using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.Reader
{
    public class IOFactory : IIOFactory
    {
        public IReader CreateReader(string filePath)
        {
            return new Reader(filePath);
        }

        public IWriter CreateWriter(string filePath, bool append = false)
        {
            return new Writer(filePath, append);
        }
    }
}
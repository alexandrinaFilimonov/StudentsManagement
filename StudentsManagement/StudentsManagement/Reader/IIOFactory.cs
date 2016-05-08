using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.Reader
{
    public interface IIOFactory
    {
        IReader CreateReader(string filePath);

        IWriter CreateWriter(string filePath, bool append = false);
    }
}

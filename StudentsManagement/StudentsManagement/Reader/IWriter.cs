using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.Reader
{
    public interface IWriter : IDisposable
    {
        void WriteLine(string line);
    }
}

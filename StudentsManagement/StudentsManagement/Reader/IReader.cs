using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.Reader
{
    public interface IReader : IDisposable
    {
        bool EndOfStream { get; }

        string ReadLine();
    }
}

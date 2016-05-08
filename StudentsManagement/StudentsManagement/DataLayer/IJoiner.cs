using System.Collections.Generic;
using StudentsManagement.Models;

namespace StudentsManagement.DataLayer
{
    public interface IJoiner<TModelMap,TModelTo>
    {
        List<StudentSubjectJoin> Join(int fromForeignKey);
    }
}

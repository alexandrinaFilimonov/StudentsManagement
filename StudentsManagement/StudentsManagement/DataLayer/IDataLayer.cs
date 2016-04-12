using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.DataLayer
{
    public interface IDataLayer<TModel>
    {
        IEnumerable<TModel> GetAll();

        TModel Get(int Id);

        void Update(TModel model);

        void Delete(int Id);
    }
}

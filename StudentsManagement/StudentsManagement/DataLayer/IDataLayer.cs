using System.Collections.Generic;
using System.Diagnostics.Contracts;
using StudentsManagement.CodeContracts;

namespace StudentsManagement.DataLayer
{
    [ContractClass(typeof(ContractDataLayer<>))]
    public interface IDataLayer<TModel>
    {
        IEnumerable<TModel> GetAll();

        IEnumerable<TModel> GetByFieldValue(int columnIndex, string value);

        TModel Get(int id);

        void Add(TModel model);

        void Update(int id, TModel model);

        void Delete(int id);

        void Import(string fileName);
    }
}

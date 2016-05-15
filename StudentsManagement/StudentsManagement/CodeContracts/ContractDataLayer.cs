using System.Collections.Generic;
using System.Diagnostics.Contracts;
using StudentsManagement.DataLayer;

namespace StudentsManagement.CodeContracts
{
    [ContractClassFor(typeof(IDataLayer<>))]
    public abstract class ContractDataLayer<T> : IDataLayer<T> 
    {
        public IEnumerable<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetByFieldValue(int columnIndex, string value)
        {
            throw new System.NotImplementedException();
        }

        public T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(T model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, T model)
        {
            Contract.Requires(id != 0, "Id cannot be zero");
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Import(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
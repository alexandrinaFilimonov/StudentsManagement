using System.Collections.Generic;

namespace StudentsManagement.DataLayer
{
    public interface IDataLayer<TModel>
    {
        IEnumerable<TModel> GetAll();

        IEnumerable<TModel> GetByFieldValue(int columnIndex, string value);

        TModel Get(int id);

        void Add(TModel model);

        void Update(int id, TModel model);

        void Delete(int id);
    }
}

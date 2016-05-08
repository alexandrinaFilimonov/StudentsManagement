using StudentsManagement.DataLayer;
using StudentsManagement.FileManager;
using StudentsManagement.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.Tests.DataLayer
{
    class FakeDataService : DataLayer<FakeModel>
    {
        public FakeDataService(IIOFactory factory, IFileManager fileManager) : base(factory, fileManager) { }

        public override string FilePath
        {
            get { return "test.txt"; }
        }

        public override void Import(string fileName)
        {
            throw new NotImplementedException();
        }

        protected override FakeModel CreateEntity(string[] fields)
        {
            return new FakeModel
            {
                Id = int.Parse(fields[0]),
                FirstProperty = fields[1],
                SecondProperty = int.Parse(fields[2])
            };
        }

        protected override string EntityToCsv(int newItemId, FakeModel model)
        {
            return string.Format("{0},{1},{2}", newItemId, model.FirstProperty, model.SecondProperty);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentsManagement.DataLayer
{
    public abstract class DataLayer<TModel> : IDataLayer<TModel>
    {
        public abstract string FilePath { get; }

        public abstract void Import(string fileName);

        //private readonly string tempFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\temp.csv");
        private readonly string tempFilePath = "D:\\master an 2\\css\\c\\temp.csv";
        public IEnumerable<TModel> GetAll()
        {
            var entities = new List<TModel>();
            using (var reader = new StreamReader(File.OpenRead(FilePath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        var fields = line.Split(',');
                        var entity = CreateEntity(fields);
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }

        public IEnumerable<TModel> GetByFieldValue(int fieldIndex, string fieldValue)
        {
            var entities = new List<TModel>();
            using (var reader = new StreamReader(File.OpenRead(FilePath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        var fields = line.Split(',');
                        if (fields[fieldIndex] == fieldValue)
                        {
                            var entity = CreateEntity(fields);
                            entities.Add(entity);
                        }
                    }
                }
            }
            return entities;
        }

        protected abstract TModel CreateEntity(string[] fields);

        public TModel Get(int id)
        {
            using (var reader = new StreamReader(File.OpenRead(FilePath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        var fields = line.Split(',');
                        if (id == int.Parse(fields[0]))
                        {
                            return CreateEntity(fields);
                        }
                    }
                }
            }
            return default(TModel);
        }

        public void Add(TModel model)
        {
            var newItemId = GetNewItemId();

            var newLine = EntityToCsv(newItemId, model);
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine(newLine);
            }
        }

        protected abstract string EntityToCsv(int newItemId, TModel model);

        public void Update(int id, TModel model)
        {
            using (var reader = new StreamReader(File.OpenRead(FilePath)))
            {
                using (var writer = new StreamWriter(File.OpenWrite(tempFilePath)))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            var values = line.Split(',');
                            if (int.Parse(values[0]) == id)
                            {
                                line = EntityToCsv(id, model);
                            }
                            writer.WriteLine(line);
                        }
                    }
                }
            }
            ApplyChanges();
        }

        public void Delete(int id)
        {
            using (var reader = new StreamReader(File.OpenRead(FilePath)))
            {
                using (var writer = new StreamWriter(File.OpenWrite(tempFilePath)))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            var values = line.Split(',');
                            if (int.Parse(values[0]) != id)
                            {
                                writer.WriteLine(line);
                            }
                        }
                    }
                }
            }
            ApplyChanges();
        }
        protected int GetNewItemId()
        {
            var ids = GetExistingValuesOfAProperty(propertyIndex: 0);
            var maxId = ids.Select(int.Parse).OrderByDescending(x => x).First();
            return maxId + 1;
        }

        protected List<string> GetExistingValuesOfAProperty(int propertyIndex)
        {
            var lines = File.ReadAllLines(FilePath).ToList();
            var items = lines.Select(x => new List<string>(x.Split(',')));
            var existingIds = items.Select(x => x[propertyIndex]).ToList();
            return existingIds;
        }

        private void ApplyChanges()
        {
            File.Delete(FilePath);
            File.Move(tempFilePath, FilePath);
            File.Delete(tempFilePath);
        }
    }
}
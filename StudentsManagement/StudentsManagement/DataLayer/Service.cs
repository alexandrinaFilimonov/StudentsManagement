using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentsManagement.DataLayer
{
    public abstract class Service<TModel> : IDataLayer<TModel>
    {
        public IEnumerable<TModel> GetAll()
        {
            var entities = new List<TModel>();
            using (var reader = new StreamReader(File.OpenRead(FilePath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
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
                    if (line != null)
                    {
                        var fields = line.Split(',');
                        if(fields[fieldIndex] == fieldValue){
                            var entity = CreateEntity(fields);
                            entities.Add(entity);
                        }
                    }
                }
            }
            return entities;
        }

        protected abstract TModel CreateEntity(string[] fields);

        protected abstract string FilePath { get; }
        
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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        private int GetNewItemId()
        {
            int lastId = 1;
            using (var reader = new StreamReader(File.OpenRead(FilePath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        lastId++;
                    }
                }
                lastId++;
            }
            return lastId;
        }
    }
}
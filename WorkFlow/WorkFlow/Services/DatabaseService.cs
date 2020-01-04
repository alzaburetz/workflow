using LiteDB;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Services
{
    public class DatabaseService : IDataBase
    {
        private readonly string DatabasePath;
        private readonly LiteDatabase database;
        public DatabaseService()
        {
            DatabasePath = Path
            .Combine(System
            .Environment
            .GetFolderPath(System
                .Environment
                .SpecialFolder
                .Personal), @"database.db");

            database = new LiteDatabase(DatabasePath);
           
        }

        public Task DeleteItem<T>(string collection, Query query)
        {
            return Task.Run(() => database.GetCollection<T>(collection).Delete(query));
        }

        public Task<T> GetItem<T>(string collection, Query query)
        {
            return Task.Run(() =>
            {
                var item = database.GetCollection<T>(collection).FindOne(query);
                return item;
            });
        }

        public Task<IEnumerable<T>> GetItemsByQuery<T>(string collection, Query query)
        {
            return Task.Run(() =>
            {
                var item = database.GetCollection<T>(collection).Find(query);
                return item;
            });
        }

        public Task<bool> UpdateItem<T>(string collection, T item, Query query = null)
        {
            return Task.Run(() =>
            {
                return database.GetCollection<T>(collection).Update(item);
            });
        }

        public Task WriteItem<T>(string collection, T item)
        {
            return Task.Run(() => database.GetCollection<T>(collection).Insert(item));
        }

        public Task<int> GetRecordCount<T>(string collection)
        {
            return Task.Run(() =>
            {
                return database.GetCollection<T>(collection).Count();
            });
        }
    }
}

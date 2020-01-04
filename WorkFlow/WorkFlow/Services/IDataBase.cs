using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using LiteDB;

namespace WorkFlow.Services
{
    public interface IDataBase
    {
        Task WriteItem<T>(string collection, T item);
        Task<T> GetItem<T>(string collection, Query query);
        Task<IEnumerable<T>> GetItemsByQuery<T>(string collection, Query query);
        Task DeleteItem<T>(string collection, Query query);
        Task<bool> UpdateItem<T>(string collection, T item, Query query);
        Task<int> GetRecordCount<T>(string colletion);
    }
}

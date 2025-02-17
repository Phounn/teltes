using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Microsoft.AspNetCore.Components.Forms;

namespace Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T body, string collection, string id);

    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private AccessBucket accessBucket;
        public Repository(IBucketProvider bucket)
        {
            accessBucket = new AccessBucket(bucket);
        }
        public async Task<T> Create(T body, string collection, string id)
        {
            try
            {
                var database = await accessBucket.GetCollectionAsync(collection);
                var result = await database.InsertAsync<T>(id, body);
                return body;
            }
            catch (Exception ex) { return null; }
        }
    }

    public class AccessBucket
    {
        public IBucketProvider bucketProvider;
        public AccessBucket(IBucketProvider bucketProvider)
        {
            this.bucketProvider = bucketProvider;
        }
        public async Task<IBucket> _GetBucketAsync()
        {
            var bucket = await bucketProvider.GetBucketAsync("TestTelbiz");
            return bucket;
        }
        public async Task<IScope> GetScopeAsync()
        {
            var scope = await _GetBucketAsync();
            var _scope = scope.Scope("Permission");
            return _scope;
        }
        public async Task<ICouchbaseCollection> GetCollectionAsync(string collectionName)
        {
            var collection = await GetScopeAsync();
            var _collection = collection.Collection(collectionName);
            return _collection;
        }

    }

}

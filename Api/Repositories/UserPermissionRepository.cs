using Couchbase.Extensions.DependencyInjection;
using Share.Models;

namespace Api.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        Task<UserPermission> GetById(string id);
        Task<UserPermission> UpdateUserPermission(string id, UserPermission body);
        Task DeleteUserPermission(string id);
    }
    public class UserPermissionRepository : Repository<UserPermission>, IUserPermissionRepository
    {
        private readonly IBucketProvider bucketProvider;
        private readonly AccessBucket accessBucket;
        public UserPermissionRepository(IBucketProvider bucketProvider, AccessBucket accessBucket) : base(bucketProvider)
        {
            this.bucketProvider = bucketProvider;
            this.accessBucket = accessBucket;
        }
        public async Task<UserPermission> GetById(string id)
        {
            try
            {
                var database = await accessBucket.GetScopeAsync();
                var query = await database.QueryAsync<UserPermission>($"SELECT t.* FROM TestTelbiz.Permission.PermissionAll t WHERE t.phone IS NOT MISSING");
                var content = await query.ToListAsync();
                var s = content.Where(x => x.Phone == id).FirstOrDefault();
                return s;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<UserPermission> UpdateUserPermission(string id, UserPermission body)
        {
            try
            {
                var database = await accessBucket.GetScopeAsync();
                var collection = await accessBucket.GetCollectionAsync("PermissionAll");
                var query = await database.QueryAsync<UserPermission>($"SELECT t.* FROM TestTelbiz.Permission.PermissionAll t WHERE t.phone IS NOT MISSING");
                var content = await query.ToListAsync();
                var s = content.Where(x => x.Phone == id).FirstOrDefault();
                s.Name = body.Name;
                s.Description = body.Description;
                s.Permissions = body.Permissions;
                await collection.UpsertAsync(id, s);
                return s;
            }
            catch (Exception ex) { return null; }
        }
        public async Task DeleteUserPermission(string id)
        {
            try
            {
                var database = await accessBucket.GetScopeAsync();
                var collection = await accessBucket.GetCollectionAsync("PermissionAll");
                //var content = await database.QueryAsync<UserPermission>($"DELETE FROM TestTelbiz.Permission.PermissionAll t WHERE t.phone = {id}");
                await collection.RemoveAsync(id);
            }
            catch (Exception ex) { }   
        }

    }
}

using Couchbase.Extensions.DependencyInjection;
using Share.Models;
using System.Linq;

namespace Api.Repositories
{
    public interface IGroupPermissionRepository : IRepository<GroupPermission>
    {
        Task<GroupPermission> UpdateGroupPermission(string id, GroupPermission body);
        Task<GroupPermission> GetById(string id);
        Task DeleteGroupPermission(string id);

    }
    public class GroupPermissionRepository : Repository<GroupPermission>, IGroupPermissionRepository
    {
        private readonly AccessBucket accessBucket;
        private readonly IBucketProvider bucketProvider;
        public GroupPermissionRepository(IBucketProvider bucketProvider, AccessBucket accessBucket) : base(bucketProvider)
        {
            this.bucketProvider = bucketProvider;
            this.accessBucket = accessBucket;
        }

        public async Task<GroupPermission> GetById(string id)
        {
            try
            {
                var database = await accessBucket.GetScopeAsync();
                
                var query= await database.QueryAsync<GroupPermission>($"SELECT t.* FROM TestTelbiz.Permission.PermissionAll t WHERE t.id IS NOT MISSING");
                var content = await query.ToListAsync();
                var s = content.Where(x => x.Id == id).FirstOrDefault();
                return s;
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<GroupPermission> UpdateGroupPermission(string id, GroupPermission body)
        {
            try
            {
                var database = await accessBucket.GetScopeAsync();
                var collection = await accessBucket.GetCollectionAsync("PermissionAll");
                var query = await database.QueryAsync<GroupPermission>($"SELECT t.* FROM TestTelbiz.Permission.PermissionAll t WHERE t.id IS NOT MISSING");
                var content = await query.ToListAsync();
                var s = content.Where(x => x.Id == id).FirstOrDefault();
                s.Name = body.Name;
                s.Members = body.Members;
                s.Description = body.Description;
                s.Permissions = body.Permissions;
                await collection.UpsertAsync(id, s);
                return s;
            }
            catch (Exception ex) { return null; }
        }
        public async Task DeleteGroupPermission(string id)
        {
            try
            {
                var collection = await accessBucket.GetCollectionAsync("PermissionAll");
                await collection.RemoveAsync(id);
            }

            catch (Exception ex) { }
        }
    }
}

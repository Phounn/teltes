using Couchbase.Extensions.DependencyInjection;
using Share.Models;

namespace Api.Repositories
{
    public interface IPermissionManagementRepository : IRepository<PermissionManagement>
    {
        Task<List<PermissionManagement>> GetAll(string collection);
    }
    public class PermissionManagementRepository : Repository<PermissionManagement>, IPermissionManagementRepository
    {
        private readonly IBucketProvider bucketProvider;
        private readonly AccessBucket accessBucket;
        public PermissionManagementRepository(IBucketProvider bucketProvider, AccessBucket accessBucket) : base(bucketProvider)
        {
            this.bucketProvider = bucketProvider;
            this.accessBucket = accessBucket;
        }
        public async Task<List<PermissionManagement>> GetAll(string collection)
        {
            try
            {
                var database = await accessBucket.GetScopeAsync();
                var query_user = await database.QueryAsync<UserPermission>($"SELECT t.* FROM TestTelbiz.Permission.{collection} t WHERE t.phone IS NOT MISSING");
                var query_group = await database.QueryAsync<GroupPermission>($"SELECT t.* FROM TestTelbiz.Permission.{collection} t WHERE t.id IS NOT MISSING");

                //var data_user = await query_user.ToListAsync().;
                var data_user = await query_user.ToListAsync();
                var data_group = await query_group.ToListAsync();

                List<PermissionManagement> data = new();
                data.Add(new PermissionManagement { Users = data_user, Groups = data_group });  
                // Pairing each user with each group
                return data;
            }
            catch { return null; }
        }
    }
}

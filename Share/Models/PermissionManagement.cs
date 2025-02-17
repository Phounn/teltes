using System.ComponentModel.DataAnnotations;

namespace Share.Models
{
    public class PermissionManagement
    {
        public List<UserPermission> Users { get; set; } = new();
        public List<GroupPermission> Groups { get; set; } = new();
        public List<string> GetUserPermissions(string phone, string permission)
        {
            var user = Users.FirstOrDefault(x => x.Phone == phone);
            if (user == null)
                return new();
            var allUserPermissions = Groups.Where(g => g.Members.Contains(phone)).SelectMany(g => g.Permissions).Concat(user.Permissions).DistinctBy(x => x).ToList();
            return allUserPermissions;
        }
    }
    public class UserPermission
    {
        [Key]
        public string? Phone { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string> Permissions { get; set; } = new();
    }
    public class GroupPermission    
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public List<string> Members { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }
}

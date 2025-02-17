
namespace TelbizTemplate_Test.Components.Models
{
    public class UserModel

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CompanyModel Company { get; set; }
    }



}

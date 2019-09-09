using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
        public RoleModel()
        {
            Users = new List<UserModel>();
        }
    }
}

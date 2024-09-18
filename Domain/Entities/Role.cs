using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public ICollection<UserAccount> UserAccounts { get; set; }
    }
}

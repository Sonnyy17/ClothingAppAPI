using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserAccount
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string RoleID { get; set; }

        public Role Role { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Clothes> Clothes { get; set; }
    }
}

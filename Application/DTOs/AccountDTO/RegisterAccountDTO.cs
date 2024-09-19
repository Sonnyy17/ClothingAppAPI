using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AccountDTO
{
    public class RegisterAccountDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPasword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoASPNETCore_Autenticacao_Autorizacao_Via_Token_Bearer_JWT.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

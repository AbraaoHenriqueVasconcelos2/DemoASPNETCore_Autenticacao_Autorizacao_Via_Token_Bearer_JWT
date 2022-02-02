using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoASPNETCore_Autenticacao_Autorizacao_Via_Token_Bearer_JWT.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using DemoASPNETCore_Autenticacao_Autorizacao_Via_Token_Bearer_JWT.Services;
using DemoASPNETCore_Autenticacao_Autorizacao_Via_Token_Bearer_JWT.Repositories;


namespace DemoASPNETCore_Autenticacao_Autorizacao_Via_Token_Bearer_JWT.Controllers
{

    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";





    }

}

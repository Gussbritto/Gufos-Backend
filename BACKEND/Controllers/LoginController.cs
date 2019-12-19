using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BACKEND.Domains;
using BACKEND.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BACKEND.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        // Chamamos nosso contexto da base de dados 
        GufosContext _context = new GufosContext ();

        // Definimos uma variável para percorrer nossos metodos com as configurações obtidas no appsetings.json
        private IConfiguration _config;

        // Definimos método construtor para poder acessar estas configs ^
        public LoginController (IConfiguration config) {
            _config = config;
        }

        // Chamamos nosso método para validar o usuário na aplicação 
        public Usuario ValidaUsuario (LoginViewModel Login) {
            var usuario = _context.Usuario.FirstOrDefault (
                u => u.Email == Login.Email && u.Senha == Login.Senha);

            return usuario;
        }

        // Geramos o Token

        private string GerarToken (Usuario userInfo) {

            // Definimos a criptografia do nosso Token
            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config["JWT:Key"]));

            var credentials = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims( dados da sessão )
            var claims = new [] {
                new Claim (JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim (JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ()),
            };

            // Configuramos nosso Token e seu tempo de vida 
            var token = new JwtSecurityToken (
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires : DateTime.Now.AddMinutes (120),
                signingCredentials : credentials
            );

            return new JwtSecurityTokenHandler ().WriteToken (token);

        }

        // Usamos essa anotação ignorar a autenticação nesse método 
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login ([FromBody] LoginViewModel login) {

            IActionResult response = Unauthorized ();
            var user = ValidaUsuario (login);

            if (user != null) {
                var tokenString = GerarToken (user);
                response = Ok (new { token = tokenString });
            }

            return response;
        }
    }
}
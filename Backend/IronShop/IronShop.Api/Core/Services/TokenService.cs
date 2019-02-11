using IronShop.Api.Core.Common;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IronShop.Api.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IronToken Generate(User user)
        {
            //Create token
            //Claims son propiedades conocidas que se envian al cliente para 
            //que las use y este las devuelve en los request

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtCustomClaims.Role, user.Role)
            };

            //Secret key que se utiliza para encriptar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            /*
             * En el Token incluimos Issuer & Audience porque luego cuando nos llegue
             * un token el method [Authorize] verificara que sean los autorizados (declarados en el appsettings)
             * Ya que cuando configuramos el Authentication Schema especificamos que se valida Issuer&Audience.
             * Asique si los creamos con Issuer = "A" & Audience = "B" los token tienen que venir con esa data sino se rechaza!
             */

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
                );

            return new IronToken(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using Microsoft.EntityFrameworkCore;
using WebTuyenSinh_Application.Modell;

namespace WebTuyenSinh_Application.Repository
{
    public class ValidateTokenService : IValidateTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly HeThongTuyenSinhDB _context;
        public ValidateTokenService(IConfiguration configuration, HeThongTuyenSinhDB context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<bool> ValidateToken(string Token, List<long> idPermisstions)
        {
            try {
                IdentityModelEventSource.ShowPII = true;

                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();

                validationParameters.ValidateLifetime = true;

                validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
                validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(Token, validationParameters, out validatedToken);
                var id = principal.Claims.SingleOrDefault(x => x.Type == "idRole");

                var role_permistion = await _context.Role_Permisstions.Where(x => x.idPermisstion!=null && (idPermisstions.Contains(x.idPermisstion.Value)) && x.idRole == long.Parse(id.Value)).FirstOrDefaultAsync();
                if(role_permistion!=null)
                    return true;
            } catch { return false; }
            return false;
        
        }

        public async Task<ResetPassword> ValidateToken(string Token)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;

                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();

                validationParameters.ValidateLifetime = true;

                validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
                validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(Token, validationParameters, out validatedToken);
                var id = principal.Claims.SingleOrDefault(x => x.Type == "ID");
                var Users = await _context.Users.FirstOrDefaultAsync(x=>x.id.ToString()==id.Value);
                return new ResetPassword()
                {
                    Password = "",
                    Email = Users.Email,
                    ConfirmPassword = "",
                    UseName = Users.UserName,
                    
                };
            }
            catch { return null; }
         
        }
    }
}

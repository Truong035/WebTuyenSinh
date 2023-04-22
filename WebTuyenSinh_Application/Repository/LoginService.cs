using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.System;
using WebTuyenSinh_Application.ViewApi;
namespace WebTuyenSinh_Application.Repository
{
    public class LoginService : ILoginService
    {
        private readonly HeThongTuyenSinhDB _context;
        private readonly IConfiguration _config;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        public LoginService(HeThongTuyenSinhDB context, IConfiguration configuration, UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = configuration;
        }

        public async Task<ApiResult> CheckToken(string Token, string Email)
        {
            try
            {
                var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Email != null && x.Email.Trim().ToUpper().Equals(Email.ToUpper()));
                if (account == null)
                {
                    return new ApiResult() { Success = false, Message = "Email incorrest" };
                }
                if (account?.NormalizedEmail.Equals(Token) == true)
                {
                    return new ApiResult() { Success = true, Message = "Success" };
                }
                else
                {
                    return new ApiResult() { Success = false, Message = "" };
                }
            }
            catch { return new ApiResult() { Success = false, Message = "Server errol" }; }

        }

        public async Task<ApiResult> ForgetPassWord(string Email)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.Email != null && x.Email.Trim().ToUpper().Equals(Email.ToUpper()));
            if (account == null)
            {
                return new ApiResult() { Success = false, Message = "Email not accept" };
            }
            else
            {

                ///  var user = await _userManager.FindByNameAsync(account.UserName);
                var claims = new[] {
                     new Claim("ID",account.Id),
                new Claim(ClaimTypes.Email,account.Email),
                 new Claim(ClaimTypes.Role, "SinhVien"),
                new Claim(ClaimTypes.Name, account.UserName)
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds);
                string strtoken = new JwtSecurityTokenHandler().WriteToken(token);
                account.NormalizedEmail = strtoken;
                await _context.SaveChangesAsync();
                return new ApiResult() { Success = true, Message = "Login success", Data = strtoken };
            }
        }

        public async Task<ApiResult> Login(LoginRequest request)
        {
            try
            {
                var account = _context.Accounts.Single(x => x.Email != null && x.Email.Trim().ToUpper().Equals(request.Email.ToUpper()));
                if (account == null)
                {
                    return new ApiResult() { Success = false, Message = "Email incorrest" };
                }
                else
                {
                    account = _context.Accounts.SingleOrDefault(x => x.Email.Trim().ToUpper().Equals(request.Email.Trim().ToUpper()) && x.PasswordHash.Trim().Equals(new MD5().GetMD5(request.Password.Trim()))); ;
                    if (account == null)
                    {
                        return new ApiResult() { Success = false, Message = "Password incorrest" };

                    }
                    // var user = await _userManager.FindByNameAsync(account.UserName);

                    var claims = new[] {
                     new Claim("ID",account.Id),
                new Claim(ClaimTypes.Email,account.Email),
                 new Claim(ClaimTypes.Role, "SinhVien"),
                new Claim(ClaimTypes.Name, account.UserName)
            };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Issuer"],
                        claims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: creds);

                    return new ApiResult() { Success = true, Message = "Login success", Data = (new JwtSecurityTokenHandler().WriteToken(token)) };
                }
            }
            catch
            {
                return new ApiResult() { Success = false, Message = "Server Internal", Data = null };
            }
        }

        public async Task<ApiResult> LoginApi(string Email)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.Email != null && x.Email.Trim().ToUpper().Equals(Email.ToUpper()));
            if (account == null)
            {
                return new ApiResult() { Success = false, Message = "Email incorrest" };
            }
            else
            {

                //var user = await _userManager.FindByNameAsync(account.UserName);
                var claims = new[] {
                     new Claim("ID",account.Id),
                new Claim(ClaimTypes.Email,account.Email),
                 new Claim(ClaimTypes.Role, "SinhVien"),
                new Claim(ClaimTypes.Name, account.UserName)
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds);

                return new ApiResult() { Success = true, Message = "Login success", Data = (new JwtSecurityTokenHandler().WriteToken(token)) };
            }
        }

        public async Task<ApiResult> LoginUse(LoginRequest request)
        {
            try
            {
                var Users = await _context.Users.ToListAsync();
                var account = Users.FirstOrDefault(x => x.Email.Trim() != null && x.Email.Trim().ToUpper().Equals(request.Email.ToUpper()));
                if (account == null)
                {
                    return new ApiResult() { Success = false, Message = "Email incorrest" };
                }
                else
                {
                    //account = Users.FirstOrDefault(x => x.Email.Trim().ToUpper().Equals(request.Email.Trim().ToUpper()) && x.Pass.Trim().Equals(new MD5().GetMD5(request.Password.Trim())));
                    //if (account == null)
                    //{
                    //    return new ApiResult() { Success = false, Message = "Password incorrest" };
                    //}
                    var claims = new[] {
                    new Claim("ID",account.id.ToString()),
                new Claim(ClaimTypes.Email,account.Email),
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim("idRole",account.idRole.ToString())
            };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Issuer"],
                        claims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: creds);
                    return new ApiResult() { Success = true, Message = "success", Data = (new JwtSecurityTokenHandler().WriteToken(token)) };

                }
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Server Internal" + e.Message, Data = null };
            }
        }
        public async Task<ApiResult> Register(RegisterRequest request)
        {
            try
            {
                var user = _context.Accounts.SingleOrDefault(x => x.Email != null && x.Email.Trim().ToUpper().Equals(request.Email.Trim().ToUpper()));
                if (user != null)
                {
                    return new ApiResult() { Success = false, Message = "Email already exists" };
                }
                else
                {
                    Account account = new Account();
                    account.Email = request.Email;
                    account.UserName = request.UseName;
                    account.PasswordHash = new MD5().GetMD5(request.Password.Trim());
                    account.Telephone = request.Telephone;
                    account.Adress = request.Adress;
                    account.BrithDay = request.BrithDay;
                    account.Position = "SinhVien";
                    await _context.Accounts.AddAsync(account);
                    await _context.SaveChangesAsync();
                    var claims = new[] {
                    new Claim("ID",account.Id),
                new Claim(ClaimTypes.Email,account.Email),
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role,"SinhVien")
            };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Issuer"],
                        claims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: creds);
                    return new ApiResult() { Success = true, Message = "D success", Data = (new JwtSecurityTokenHandler().WriteToken(token)) };

                }
            }
            catch { return new ApiResult() { Success = false, Message = "Server Internal", Data = null }; }

            // return new ApiResult() { Success = true, Message = "Login success" };
        }

        public async Task<ApiResult> ResetPassWord(ResetPassword reset)
        {
            try
            {
                var account = _context.Accounts.SingleOrDefault(x => x.Email != null && x.Email.Trim().ToUpper().Equals(reset.Email.ToUpper()));
                if (account == null)
                {
                    return new ApiResult() { Success = false, Message = "Email không tồn tại" };
                }
                var user = await _userManager.FindByNameAsync(account.UserName);
                var claims = new[] {
                     new Claim("ID",account.Id),
                new Claim(ClaimTypes.Email,account.Email),
                 new Claim(ClaimTypes.Role, "SinhVien"),
                new Claim(ClaimTypes.Name, account.UserName)
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds);
                account.NormalizedEmail = "";
                account.PasswordHash = new MD5().GetMD5(reset.Password.Trim());
                await _context.SaveChangesAsync();
                return new ApiResult() { Success = true, Message = "Reset success", Data = (new JwtSecurityTokenHandler().WriteToken(token)) };
            }
            catch
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server", Data = null };
            }



        }
        public async Task<ApiResult> ResetPassWordUse(ResetPassword reset)
        {
            try
            {
                var account = _context.Users.SingleOrDefault(x => x.Email != null && x.Email.Trim().ToUpper().Equals(reset.Email.ToUpper()));
                if (account == null)
                {
                    return new ApiResult() { Success = false, Message = "Email không tồn tại" };
                }

                account.Pass = new MD5().GetMD5(reset.Password.Trim());
                await _context.SaveChangesAsync();
                return new ApiResult() { Success = true, Message = "Reset success", Data = null };
            }
            catch
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server", Data = null };
            }


        }
    }
}

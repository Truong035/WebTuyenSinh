using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinh_Application.Interface
{
 public interface  ILoginService
    {
        public Task<ApiResult> Login(LoginRequest request);
        public Task<ApiResult> Register(RegisterRequest request);
        public Task<ApiResult> LoginApi(string Email);
        public Task<ApiResult> ForgetPassWord(string Email);
        public Task<ApiResult> ResetPassWord(ResetPassword reset);
        public Task<ApiResult> ResetPassWordUse(ResetPassword reset);
        public Task<ApiResult> LoginUse(LoginRequest request);
        public Task<ApiResult> CheckToken(string Token,string Email);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TuyenSinhAPI.Modell;
using TuyenSinhAPI.ViewApi;

namespace WebTuyenSinh.Interface
{
 public interface  ILoginService
    {
        public Task<ApiResult> Login(LoginRequest request);
        public Task<ApiResult> Register(RegisterRequest request);

    }
}

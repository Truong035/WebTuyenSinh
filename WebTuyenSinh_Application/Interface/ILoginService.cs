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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTuyenSinh_Application.Interface
{
    public interface IValidateTokenService
    {
        Task<bool> ValidateToken(string Token, List<long> idPermisstion);
    }
}

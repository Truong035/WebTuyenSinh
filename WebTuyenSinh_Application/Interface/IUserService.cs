using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinh_Application.Interface
{
   public interface IUserService
    {
        public Task<UserView> GetAll(int? status);
        Task<ApiResult> Create(User use);
        Task<ApiResult> RoleCreate(Role use);
        Task<RoleView> GetAllRole();
        Task<ApiResult> GetRoleByid(long id);

        Task<ApiResult> DeleteRole(long id);
        Task<ApiResult> DeleteUse(long id);
    }
}

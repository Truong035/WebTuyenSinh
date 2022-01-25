using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.System;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinh_Application.Repository
{
   public  class UserService : IUserService
    {
        private HeThongTuyenSinhDB _context;

        public UserService(HeThongTuyenSinhDB context)
        {
            _context = context;
        }

        public async  Task<ApiResult> Create(User use)
        {
         

            if (use.id > 0)
            {
                var UserUpdate = await _context.Users.Where(x => x.Email != null && x.Email.Equals(use.Email) && x.id!=use.id).FirstOrDefaultAsync();
                if (UserUpdate != null)
                {
                    return new ApiResult() { Message = "Email này đã tồn tại", Success = false };
                }
                UserUpdate = await _context.Users.Where(x => x.id == use.id).FirstOrDefaultAsync();

                if (UserUpdate == null)
                {
                    return new ApiResult() { Message = "Không Tìm Thấy", Success = false };
                }
                try {
                    UserUpdate.idRole = use.idRole;
                    UserUpdate.UserName = use.UserName;
                    UserUpdate.Email = use.Email;
                 await _context.SaveChangesAsync();
                    return new ApiResult() { Message = "Cập nhật thành công", Success = true, Data = use.id};
                }
                catch
                {
                    return new ApiResult() { Message = "Lỗi Server", Success = false };
                }


            }
            var Users = await _context.Users.Where(x => x.Email != null && x.Email.Equals(use.Email)).FirstOrDefaultAsync();
            if (Users != null)
            {
                return new ApiResult() { Message = "Email này đã tồn tại", Success = false };
            }
            try {
                Users = new User()
                {
                    UserName = use.UserName,
                    Email = use.Email,
                    CreateDate = DateTime.Now,
                    idRole = use.idRole,
                    Pass= new MD5().GetMD5("1"),
                };
                _context.Users.Add(Users);
              var id=  await _context.SaveChangesAsync();
                return new ApiResult() { Message = "Thêm mới thành công", Success = true ,Data=id};
            } catch
            {
                return new ApiResult() { Message = "Lỗi Server" , Success = false };
            }
         
           

        }

        public async Task<UserView> GetAll(int? status)
        {
            UserView view = new UserView();
         var Users= await _context.Users.ToListAsync();
            var Roles = await _context.Roles.ToListAsync();
            view.Role = Roles;
            view.User = (from u in Users
                        join R in Roles on u.idRole equals R.id
                        select new User()
                        {
                            UserName =u.UserName,
                            Email=u.Email,
                            id=u.id,
                            idRole=u.idRole,
                            Role=R,
                        }
                      ).ToList();

            return view;
        }

        public async Task<RoleView> GetAllRole()
        {
            RoleView view = new RoleView();
            view.Permisstion = await _context.Permisstions.ToListAsync();
            view.Role = await _context.Roles.ToListAsync();
            return view;


        }

        public async Task<ApiResult> GetRoleByid(long id)
        {
            var Role = await _context.Roles.Where(x=>x.id==id).FirstOrDefaultAsync();
            if (Role == null)
            {
                return new ApiResult() { Data = null, Message = "Không tìm thấy", Success = false };
            }
            Role.Role_Permisstion = await _context.Role_Permisstions.Where(x => x.idRole == id).ToListAsync();
            return new ApiResult() { Data = Role, Message = "Success", Success = true };
        }

        public async Task<ApiResult> RoleCreate(Role Role)
        {
            try {

                if (Role.id > 0)
                {
                    var RoleUpdate = await _context.Roles.Where(x => x.id == Role.id).FirstOrDefaultAsync();
                    if (RoleUpdate == null)
                    {
                        return new ApiResult() { Data = null, Message = "Không tìm thấy", Success = false };
                    }
                    RoleUpdate.Name = Role.Name;
                    RoleUpdate.Description = Role.Description;
                    await _context.SaveChangesAsync();

                    var Role_Permisstion = await _context.Role_Permisstions.Where(x => x.idRole == Role.id).ToListAsync();
                     _context.Role_Permisstions.RemoveRange(Role_Permisstion);
                    await _context.SaveChangesAsync();
                    foreach (var item in Role.Role_Permisstion)
                    {
                        Role_Permisstion role_Permisstion = new Role_Permisstion()
                        {
                            idPermisstion = item.idPermisstion,
                            idRole = (long)Role.id
                        };
                        _context.Role_Permisstions.Add(role_Permisstion);
                        await _context.SaveChangesAsync();
                    }
                    return new ApiResult()
                    {
                        Data = Role.id,
                        Success = true,
                        Message = "Cập nhật thành công"
                    };
                }

                Role role = new Role()
                {
                    Name = Role.Name,
                    Description = Role.Description
                };
                _context.Roles.Add(role);
                var id = await _context.SaveChangesAsync();
                foreach (var item in Role.Role_Permisstion)
                {
                    Role_Permisstion role_Permisstion = new Role_Permisstion()
                    {
                        idPermisstion = item.idPermisstion,
                        idRole = (long)id
                    };
                    _context.Role_Permisstions.Add(role_Permisstion);
                    await _context.SaveChangesAsync();
                }
                    return new ApiResult()
                    {
                        Data = id,
                        Success = true,
                        Message = "Thêm thành công"
                    };
                } catch (Exception e){
                return new ApiResult()
                {
                    Data = null,
                    Success = false,
                    Message = "Lỗi Server"
                };
            }
        
        } 
    }
}

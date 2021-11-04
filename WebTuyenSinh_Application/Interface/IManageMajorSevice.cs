using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinh_Application.Interface
{
 public interface IManageMajorSevice
    {
        public Task<List<Major>> GetAll();
        public Task<ApiResult> CreateList(List<Major> listMajor);
    }
}

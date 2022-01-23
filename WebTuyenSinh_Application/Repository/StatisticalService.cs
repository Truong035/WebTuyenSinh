using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using Microsoft.EntityFrameworkCore;
namespace WebTuyenSinh_Application.Repository
{
    public class StatisticalService : IStatisticalService
    {
        private HeThongTuyenSinhDB _context;

        public StatisticalService(HeThongTuyenSinhDB context)
        {
            _context = context;
        }

        public async Task<StatisticalHome> StatisticalHome()
        {
            var Profile = await _context.ProfileStudents.ToListAsync();
            return null;
        }
    }
}

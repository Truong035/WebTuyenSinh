using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Modell;

namespace WebTuyenSinh_Application.Interface
{
   public interface IStatisticalService
    {
        public Task<StatisticalHome> StatisticalHome(long? idAdmisstion);
        public Task<Statistical> Statistical(DateTime? fromDate,DateTime? toDate,int? Type, string idMajor, long? idAdmisstion, bool? Statust);

    }
}

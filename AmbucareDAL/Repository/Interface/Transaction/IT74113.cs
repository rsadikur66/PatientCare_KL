using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Transaction
{
   public interface IT74113:IDisposable
    {
       IEnumerable GetPatInfo(string uId);
       IEnumerable GetDocInfoByReq(int T_AMBU_REG_ID);
       IEnumerable GetDocDept(int T_EMP_ID);
       IEnumerable DiagonosisList();
       IEnumerable GetFrequency();
       IEnumerable GetDuration(int Frequency_Code);
        IEnumerable GetAdviceList(string T_LANG2_NAME);
       DataTable GetMedicineByTrade();
       DataTable GetGen();
       DataTable GetStrengthByGen(string T_GEN_CODE);
       DataTable GetRouteByGen(string T_GEN_CODE);
       DataTable GetFormByGen(string T_GEN_CODE);
        IEnumerable GetPrscrptnByDoc(int T_DOC_ID, int T_PAT_ID);
        string Insert(T74039 t74039, List<T74040> t74040List, List<T74078> t74078List);
        IEnumerable GetDiagonosisByPres(int Pres);
        DataTable GetMedListByPres(int Pres);
        void Save();


    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Setup
{
   public interface IT74146
   {
       DataTable getGridData();

       string insertStatus(string T_MSG_CODE, string T_LANG1_MSG, string T_LANG2_MSG,
           string T_ENTRY_USER, string T_FORM_CODE, string T_ACTV_STTS);

       string updateStatus(string T_MSG_ID, string T_MSG_CODE, string T_LANG1_MSG, string T_LANG2_MSG,
           string T_UPD_USER, string T_FORM_CODE, string T_ACTV_STTS);
       DataTable getFormCode();
   }
}

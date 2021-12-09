using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Report
{
   public interface IR74027Report
   {
       DataTable GetStoresData();
       DataTable GetSuppliersData();
       DataTable GetGridAllData(int storeid);
   }
}

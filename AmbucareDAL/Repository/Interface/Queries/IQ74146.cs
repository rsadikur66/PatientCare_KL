using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Queries
{
    public interface IQ74146
    {
        DataTable GetAmbulanceList( string zone,string role);
        DataTable GetStockItem(int ambuId);
        DataTable GetUsedMedicineByTeam(string userid);
        DataTable GetUsedMedicineByRequest(int requestId);
        DataTable GetStorId(int ambuId);
    }
}

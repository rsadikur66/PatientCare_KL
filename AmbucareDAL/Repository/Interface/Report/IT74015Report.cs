using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.Interface.Report
{
    public interface IT74015Report
    {
        //Report T74015Report interface
        DataTable GetEmployeeTypeIdAndEmployeeIdByAmbulanceIdReport();
    }
}

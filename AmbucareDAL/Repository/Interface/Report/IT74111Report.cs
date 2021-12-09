using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Report
{
  public interface IT74111Report
    {
        DataTable GetData(int id);
    }
}

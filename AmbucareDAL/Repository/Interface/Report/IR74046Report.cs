﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbucareDAL.Repository.Interface.Report
{
   public interface IR74046Report
   {
       DataTable GetPrescription(int T_REQUEST_ID);
       DataTable GetBill(int T_REQUEST_ID);
    }
}

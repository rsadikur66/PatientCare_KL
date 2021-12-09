using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Repository.DAL.Query
{
    public class Q74143DAL:CommonDAL
    {
        public DataTable getAllGridData(string lang)
        {
            DataTable query = Query($"SELECT k.*, DECODE(k.t_login_status,NULL,'Logged Out','Logged In') t_login_status_desc, (CASE WHEN (k.t_disch_status IS NULL AND K.T_REQUEST_ID IS NULL) THEN 'Request Not Created' WHEN (k.t_disch_status IS NULL AND K.T_REQUEST_ID IS NOT NULL) THEN 'In Ambulance' WHEN (k.t_disch_status IS NOT NULL AND K.T_REQUEST_ID IS NOT NULL) THEN 'Discharged' END) t_disch_status_desc FROM ( SELECT DISTINCT t57.t_user_id, t_login_status, ( SELECT t_disch_status FROM t74041 WHERE t74041.t_request_id = ( SELECT MAX(t_request_id) FROM t74041 t WHERE t.t_user_id = t57.t_user_id ) ) t_disch_status, ( SELECT t_request_id FROM t74041 WHERE t74041.t_request_id = ( SELECT MAX(t_request_id) FROM t74041 t WHERE t.t_user_id = t57.t_user_id ) ) t_request_id, t04.t_first_lang{lang}_name || ' ' || t04.t_father_lang{lang}_name || ' ' || t04.t_gfather_lang{lang}_name t_operator_name FROM t74057 t57 INNER JOIN t74004 t04 ON t04.t_emp_id = t57.t_emp_id WHERE t57.t_role_code = 24 AND t04.t_emp_typ_id = 21 ) k");
            return query;
        }
    }
}
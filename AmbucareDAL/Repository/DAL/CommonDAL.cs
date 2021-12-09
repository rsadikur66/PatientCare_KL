using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Linq;
using Oracle.ManagedDataAccess.Client;

namespace AmbucareDAL.Repository.DAL
{
    public class CommonDAL
    {
        private OracleTransaction _oracleTransaction;
        readonly OracleConnection _oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["OrclCon"].ConnectionString);
        
       


        public void BeginTransaction()
        {
            if (_oracleConnection.State != ConnectionState.Open)
                _oracleConnection.Open();
            _oracleTransaction = _oracleConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _oracleTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            _oracleTransaction.Rollback();
        }
        public DataTable Query(string query)
        {
            var oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["OrclCon"].ConnectionString);
            var oracleCommand = new OracleCommand
            {
                Connection = oracleConnection,
                CommandText = query
            };
            var oracleDataAdapter = new OracleDataAdapter(oracleCommand);
            var dataTable = new DataTable();

            oracleDataAdapter.Fill(dataTable);
            return dataTable;
        }
        public bool Command(string command)
        {
            if (_oracleConnection.State != ConnectionState.Open)
                _oracleConnection.Open();
            var oracleCommand = new OracleCommand
            {
                Connection = _oracleConnection,
                CommandText = command
            };
            return oracleCommand.ExecuteNonQuery() > 0;
        }
        public DataTable ExecuteSelectProcedure(String procedureName)
        {
            var oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["OrclCon"].ConnectionString);
            var oracleCommand = new OracleCommand
            {
                Connection = oracleConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedureName
            };
            oracleCommand.Parameters.Add("P_RECORDSET", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            var oracleDataAdapter = new OracleDataAdapter(oracleCommand);
            var dataTable = new DataTable();
            oracleDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public Boolean ExecuteIProcedure(String procedureName, OracleParameter[] parameters)
        {
            var oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["OrclCon"].ConnectionString);
            var context = System.Web.HttpContext.Current;
            var procedureOutput = new List<string>();
            var oracleCommand = new OracleCommand
            {
                Connection = oracleConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedureName
            };
            oracleCommand.Parameters.AddRange(parameters);
            // OracleConnection.
            //var outputParameters = new[]
            //{
            // new OracleParameter("ProcedureResult", OracleDbType.NVarchar2, 100, null, ParameterDirection.Output),
            //};
            //oracleCommand.Parameters.AddRange(outputParameters);
            oracleCommand.ExecuteNonQuery();
            // procedureOutput.AddRange(from op in 1 select op.Value.ToString());
            return Convert.ToBoolean(Convert.ToInt16(0));
        }

        public DataTable ExecuteSelectProcedure(String procedureName, OracleParameter[] parameters)
        {
            var oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["OrclCon"].ConnectionString);
            var oracleCommand = new OracleCommand
            {
                Connection = oracleConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedureName
            };
            oracleCommand.Parameters.AddRange(parameters);
            oracleCommand.Parameters.Add("P_RECORDSET", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            var oracleDataAdapter = new OracleDataAdapter(oracleCommand);
            var dataTable = new DataTable();
            oracleDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public Boolean ExecuteProcedure(String procedureName, OracleParameter[] parameters)
        {
            var context = System.Web.HttpContext.Current;
            var procedureOutput = new List<string>();
            var oracleCommand = new OracleCommand
            {
                Connection = _oracleConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedureName
            };
            oracleCommand.Parameters.AddRange(parameters);

            var outputParameters = new[]
            {
                new OracleParameter("ProcedureResult", OracleDbType.NVarchar2, 100, null, ParameterDirection.Output)
            };
            oracleCommand.Parameters.AddRange(outputParameters);
            _oracleConnection.Open();
            oracleCommand.ExecuteNonQuery();
            //_oracleConnection.Dispose();
            _oracleConnection.Close();
            procedureOutput.AddRange(from op in outputParameters select op.Value.ToString());
            return Convert.ToBoolean(Convert.ToInt16(procedureOutput[0]));
        }

        public Boolean ExecuteProcedureForGrid(String procedureName, OracleParameter[] parameters)
        {
            var procedureOutput = new List<string>();
            var oracleCommand = new OracleCommand
            {
                Connection = _oracleConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedureName
            };
            oracleCommand.Parameters.AddRange(parameters);

            var outputParameters = new[]
            {
                new OracleParameter("ProcedureResult", OracleDbType.NVarchar2, 100, null, ParameterDirection.Output)
            };
            oracleCommand.Parameters.AddRange(outputParameters);
            _oracleConnection.Open();
            oracleCommand.ExecuteNonQuery();
            _oracleConnection.Close();
            procedureOutput.AddRange(from op in outputParameters select op.Value.ToString());
            return Convert.ToBoolean(Convert.ToInt16(procedureOutput[0]));
        }

        public DataTable ExecuteSelectWithoutParam(String procedureName)
        {
            var oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["OrclCon"].ConnectionString);
            var oracleCommand = new OracleCommand
            {
                Connection = oracleConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedureName
            };
            oracleCommand.Parameters.Add("P_RECORDSET", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            var oracleDataAdapter = new OracleDataAdapter(oracleCommand);
            var dataTable = new DataTable();
            oracleDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public String GetIPAddress()
        {
            var context = System.Web.HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress)) return context.Request.ServerVariables["REMOTE_ADDR"];
            var addresses = ipAddress.Split(',');
            return addresses.Length != 0 ? addresses[0] : context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public DateTime dateTime()
        {
            return DateTime.Parse(Query($"select LOCALTIMESTAMP time from dual").Rows[0]["time"].ToString());
        }
        public string Time()
        {
            return Query($"select to_char( LOCALTIMESTAMP ,'HH24:MI') time from dual").Rows[0]["time"].ToString();
        }
        public void LogError(String T_ERROR_MESSAGE)
        {
            var context = System.Web.HttpContext.Current;
            var oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["OrclCon"].ConnectionString);
            var oracleCommand = new OracleCommand
            {
                Connection = oracleConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "T11111_INSERT"
            };
            oracleConnection.Open();
            oracleCommand.ExecuteNonQuery();
            oracleCommand.Dispose();
            oracleConnection.Close();
            oracleConnection.Dispose();
        }

        OracleCommand comm = new OracleCommand();

        OracleDataAdapter adp = new OracleDataAdapter();

        public OracleConnection OracleConnection => _oracleConnection;

        public string CheckLoginLogoutType(string rolecode)
        {
            return Query($"SELECT * from t74100 where T_ROLE_CODE ='{rolecode}' and T_TYPE = 18").Rows[0]["T_EVENT_ID"].ToString();
        }
        public string CheckLogoutType(string rolecode)
        {
            return Query($"SELECT * from t74100 where T_ROLE_CODE ='{rolecode}' and T_TYPE = 19").Rows[0]["T_EVENT_ID"].ToString();
        }
        public DataTable GetAllUserMsg(string LANGUAGE, string T_FORM_CODE)
        {
            return Query($"SELECT T_FORM_CODE,T_MSG_CODE, T_MSG_CODE ||' : '||T_LANG{LANGUAGE}_MSG MSG FROM T74099 WHERE T_FORM_CODE IN('Common','{T_FORM_CODE}') ORDER BY T_MSG_CODE");
        }
        public bool chkVerified(int tRequestId)
        {
            bool msg = false;
            DataTable dt = Query($"select * from t74041 where T_REQUEST_ID={tRequestId} and T_VERIFY_DISCHARGE is null");
            if (dt.Rows.Count > 0)
            {
                msg = true;
            }
            return msg;
        }

        public string GetSingleMsg(string lang, string msgCode)
        {
            string msg = msgCode;
            DataTable dt =Query($"SELECT T_MSG_CODE ||' : '||T_LANG{lang}_MSG T_MSG from t74099 where T_MSG_CODE ='{msgCode}' ");
            if (dt.Rows.Count>0)
            {
                msg = dt.Rows[0]["T_MSG"].ToString();
            }
            return msg;
        }
        public string updateForm(string user, string form)
        {
            string str = "OK";
            try
            {
                Command(Query($"SELECT T_FORM FROM T74124 WHERE T_ENTRY_USER='{user}'").Rows.Count > 0 ? $"UPDATE T74124 SET T_FORM='{form}' WHERE T_ENTRY_USER='{user}'" : $"INSERT INTO T74124 (T_FORM,T_ENTRY_USER) VALUES('{form}','{user}')");
            }
            catch (Exception e)
            {
                str = e.Message;
            }
            return str;

        }
        public string setServerErrorLog(string controller, string action, string user, string message)
        {
            string str = "OK";
            try
            { DataTable dt = Query($"SELECT T_FORM FROM T74124 WHERE T_ENTRY_USER='{user}'");
                string form =dt.Rows.Count>0?dt.Rows[0]["T_FORM"].ToString():"";
                string T_ID = Query($"SELECT NVL(MAX(T_ID),0)+1 T_ID FROM T74123").Rows[0]["T_ID"].ToString();
                Command($"INSERT INTO T74123(T_ID,T_CONTROLLER,T_ACTION,T_FORM,T_MESSAGE,T_TYPE,T_ENTRY_USER,T_ENTRY_DATE) VALUES ({T_ID},'{controller}','{action}','{form}','{message}','s','{user}',SYSTIMESTAMP)");

            }
            catch (Exception e)
            {

                str = e.Message;
            }
            return str;
          
        }
        public string setClientErrorLog(string controller, string action, string message, string user, string source)
        {
            string str = "OK";
            try
            { DataTable dt = Query($"SELECT T_FORM FROM T74124 WHERE T_ENTRY_USER='{user}'");
                string form =dt.Rows.Count>0?dt.Rows[0]["T_FORM"].ToString():"";
                string T_ID = Query($"SELECT NVL(MAX(T_ID),0)+1 T_ID FROM T74123").Rows[0]["T_ID"].ToString();
                Command($"INSERT INTO T74123(T_ID,T_CONTROLLER,T_ACTION,T_FORM,T_MESSAGE,T_TYPE,T_ENTRY_USER,T_ENTRY_DATE,T_SOURCE) VALUES ({T_ID},'{controller}','{action}','{form}','{message}','c','{user}',SYSTIMESTAMP,'{source}')");

            }
            catch (Exception e)
            {

                str = e.Message;
            }
            return str;
          
        }
    }
}
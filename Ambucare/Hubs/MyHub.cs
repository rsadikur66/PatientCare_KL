using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Ambucare.Models;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;
using System.Net;
using System.Web.Security;
using System.Web;
//---------------------------Dll----------------------------------
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using AmbucareDAL.Repository.DAL.Transaction;
using Newtonsoft.Json;
//-------------------------Repository-------------------------

using AmbucareDAL.Repository.Interface.Transaction;


namespace Ambucare.Hubs
{
    [HubName("myChat")]
    public class MyHub : Hub
    {
        public void say(string message)
        {
            Clients.All.hello();
            Trace.WriteLine(message);
        }
        static readonly HashSet<string> Rooms = new HashSet<string>();
        static List<user> loggedInUsers = new List<user>();
        //----------------------------------------------------------
        private readonly T74131DAL _t74131DAL = new T74131DAL();
        public string Login(string name, string UserName, string UserId)

        {
            
            //_t74131DAL.setDoc(UserId);
            DataTable dtUser = _t74131DAL.getUser(UserId);
            //string role = HttpContext.Current.Session["T_ROLE_CODE"].ToString();
            string role = dtUser.Rows[0]["T_ROLE_CODE"].ToString();
            string Name = "";
            string Id = "";
            string type = "";
            List<HospWithDoc> HospWithDocList = new List<HospWithDoc>();
            if (role =="81")
            {   //-----------------When Doc Login --------------------------------------------
            //    if (UserId.Length >4)
            //{
                //DataTable dt = GetDocInfo(name);
                //Name = dt.Rows[0]["doctor"].ToString();
                //Name = HttpContext.Current.Session["T_USER_NAME"].ToString();
                Name = UserName;
                //Id= dt.Rows[0]["T_EMP_CODE"].ToString();
                Id= dtUser.Rows[0]["T_EMP_ID"].ToString();
                type = "p";
                //DataTable dtList = GetAmbList(dt.Rows[0]["T_SITE_CODE"].ToString());




                //---------------------------------oRIGINAL-------------------------------------//
                //--DataTable dtList = GetAmbList(dtUser.Rows[0]["T_SITE_CODE"].ToString());----//
                //------------------------------------------------------------------------------//


                //---------------------------------TEMP-------------------------------------//
                DataTable dtList = GetAmbList(dtUser.Rows[0]["T_SITE_CODE"].ToString(),UserId);
                //------------------------------------------------------------------------------//







                DataTable newTable = dtList.Clone();
                var itemGroups = dtList.AsEnumerable().GroupBy(row => row.Field<Int64>("T_AMBU_REG_ID"));
                foreach (var group in itemGroups)
                {
                    DataRow first = group.First();
                   // if (group.Count() == 1)
                        //newTable.ImportRow(first);
                    //else
                    //{
                        DataRow clone = newTable.Rows.Add((object[])first.ItemArray.Clone());
                        var SingleHospWithDoc = new HospWithDoc();

                        SingleHospWithDoc.HOSPITAL_NAME = clone.ItemArray[2].ToString();
                        SingleHospWithDoc.T_SITE_CODE = clone.ItemArray[1].ToString();
                        SingleHospWithDoc.T_U_ID = clone.ItemArray[9].ToString();
                        SingleHospWithDoc.docInfo = new List<docInfo>();
                        for (int i = 0; i < dtList.Rows.Count; i++)
                        {
                            docInfo singleDoc = new docInfo();
                            if (SingleHospWithDoc.T_SITE_CODE == dtList.Rows[i]["T_AMBU_REG_ID"].ToString())
                            {
                                singleDoc.TYPE = "Pat";
                                singleDoc.DOCTOR = dtList.Rows[i]["PAT_NAME"].ToString();
                                singleDoc.HDM_DOC_CODE = dtList.Rows[i]["T_PAT_ID"].ToString();
                                singleDoc.HDM_SPCLTY_CODE = dtList.Rows[i]["GENDER"].ToString();
                                singleDoc.HDM_SPCLTY_DSCRPTN = dtList.Rows[i]["AGE_GENDER"].ToString();
                                singleDoc.T_STATUS = dtList.Rows[i]["T_DISCH_STATUS"].ToString();
                                singleDoc.T_LOG_STTS = dtList.Rows[i]["T_LOGIN_STATUS"].ToString();
                                singleDoc.T_U_ID = dtList.Rows[i]["T_USER_ID"].ToString();
                                singleDoc.T_USER_ID = dtList.Rows[i]["T_USER_ID"].ToString();
                                singleDoc.T_REQUEST_ID = Int32.Parse(dtList.Rows[i]["T_REQUEST_ID"].ToString());
                                SingleHospWithDoc.docInfo.Add(singleDoc);

                            }

                        }

                        HospWithDocList.Add(SingleHospWithDoc);
                    //}


                }



            }
            else
            {   //-----------------When Amb Login --------------------------------------------
                Name = UserName;
                Id = UserId;
                type = "d";
                DataTable dt = GetSite(Id);

                DataTable newTable = dt.Clone();
                
                
                var itemGroups = dt.AsEnumerable().GroupBy(row => row.Field<string>("T_SITE_CODE"));
                foreach (var group in itemGroups)
                {
                    DataRow first = group.First();
                   // if (group.Count() == 1)
                      //  newTable.ImportRow(first);
                    //else
                    //{
                        DataRow clone = newTable.Rows.Add((object[])first.ItemArray.Clone());
                        var SingleHospWithDoc = new HospWithDoc();
                        
                        SingleHospWithDoc.HOSPITAL_NAME = clone.ItemArray[8].ToString();
                        SingleHospWithDoc.T_SITE_CODE = clone.ItemArray[7].ToString();
                        SingleHospWithDoc.T_U_ID = clone.ItemArray[2].ToString();
                        SingleHospWithDoc.docInfo=new List<docInfo>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            docInfo singleDoc = new docInfo();
                            if (SingleHospWithDoc.T_SITE_CODE==dt.Rows[i]["T_SITE_CODE"].ToString())
                            {
                                singleDoc.TYPE = "Doc";
                                singleDoc.DOCTOR = dt.Rows[i]["DOCTOR"].ToString();
                                singleDoc.HDM_DOC_CODE = dt.Rows[i]["HDM_DOC_CODE"].ToString();
                                singleDoc.HDM_SPCLTY_CODE = dt.Rows[i]["HDM_SPCLTY_CODE"].ToString();
                                singleDoc.HDM_SPCLTY_DSCRPTN = dt.Rows[i]["HDM_SPCLTY_DSCRPTN"].ToString();
                                singleDoc.T_EMP_CODE = dt.Rows[i]["T_EMP_CODE"].ToString();
                                singleDoc.T_EMP_CODE = dt.Rows[i]["T_EMP_CODE"].ToString();
                                singleDoc.T_U_ID = dt.Rows[i]["T_EMP_CODE"].ToString();
                                singleDoc.T_REQUEST_ID = Int32.Parse(dt.Rows[i]["T_REQUEST_ID"].ToString());
                                singleDoc.T_USER_ID = dt.Rows[i]["T_USER_ID"].ToString();
                                SingleHospWithDoc.docInfo.Add(singleDoc);
                                
                            }
                             
                        }

                        HospWithDocList.Add(SingleHospWithDoc);
                    //}
                    

                }
            }

            var a = HttpContext.Current.User.Identity.Name;
            var user = new user { name = Name, ConnectionId = "0", ContextName = a, age = 20, avator = "", id = Id,U_TYPE = type,sex = "Male", memberType = "Re+gistered", fontColor = "red", status = Status.Online.ToString() , HospWithDoc= HospWithDocList };
            //var user = new user { name = Name, ConnectionId = Context.ConnectionId, ContextName = a, age = 20, avator = "", id = Id, U_TYPE = type, sex = "Male", memberType = "Re+gistered", fontColor = "red", status = Status.Online.ToString(), HospWithDoc = HospWithDocList };

            //----------------------------For test Perpose-------------------------------------
            //var user = new user { name = Name, ConnectionId = Context.ConnectionId, ContextName = a, age = 20, avator = "", id = Int32.Parse(Id), sex = "Male", memberType = "Re+gistered", fontColor = "red", status = Status.Online.ToString() };
            //----------------------------For test Perpose-------------------------------------



            Clients.Caller.rooms(Rooms.ToArray());
            Clients.Caller.setInitial(Context.ConnectionId, Name);
            var oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(loggedInUsers);
            loggedInUsers.Add(user);
            Clients.Caller.getOnlineUsers(sJSON, user);
            //Clients.Caller.getOnlineUsers(user);
            Clients.Others.newOnlineUser(user);
            return Name;
        }
        public void SendPrivateMessage(string toUserId, string message, string name)
        {

            string fromUserId = Context.ConnectionId;
            var toUser = loggedInUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            if (toUser == null)
            {
                toUser = loggedInUsers.FirstOrDefault(x => x.name.Replace("!!", "").Trim() == name.Replace("!!", "").Trim());
                if (toUser != null)
                {
                    Clients.Caller.updateConnectionId(toUserId, toUser.ConnectionId);
                    toUserId = toUser.ConnectionId;
                }
            }
            var fromUser = loggedInUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);
            if (toUser != null && fromUser != null)
            {
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.name, message, fromUserId);
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.name, message, toUserId);
            }
        }
        public void UpdateStatus(string status)
        {
            string userId = Context.ConnectionId;
            loggedInUsers.FirstOrDefault(x => x.ConnectionId == userId).status = status;                     
            Clients.Others.statusChanged(userId, status);

        }
        public void UserTyping(string connectionId, string msg)
        {
            if (connectionId != null)
            {
                var id = Context.ConnectionId;
                Clients.Client(connectionId).isTyping(id, msg);
            }
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = loggedInUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                loggedInUsers.Remove(item); // list = 
                var id = Context.ConnectionId;
                Clients.Others.newOfflineUser(item);
            }
            return base.OnDisconnected(false);
        }
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (loggedInUsers.Count(x => x.ConnectionId == id) == 0)
            {
                loggedInUsers.Add(new user { ConnectionId = id, name = userName });
                Clients.Caller.onConnected(id, userName, loggedInUsers);
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        //----------------------For Doctor Login(AmbList)---------------------------
        public string GetDecryptData(string Data)
        {
            string decData = "";
            string hash = "foxle@rn";
            string dec = Data.Replace('_', '/');
            byte[] data = Convert.FromBase64String(dec);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDEs = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDEs.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    decData = UTF8Encoding.UTF8.GetString(results);
                }

            }

            return decData;
        }
        private DataTable GetDocInfo(string chatData)
        {
            TRequest tr = new TRequest();
            List<TParam> param = new List<TParam>();
            //Param List---------------------------------start
            tr.db = "KSMC";
            tr.sp = "GET_DOC_Info_T02039";
            //string decData = GetDecryptData(chatData);
            string[] codes = chatData.Split(',').Select(a => a.Trim()).ToArray();
            string EMP_CODE = codes[0];
            string ROLE_CODE = codes[1];
            string T_SITE_CODE = codes[2];
            param.Add(new TParam() { Key = "P_EMP_CODE", Value = EMP_CODE });
            param.Add(new TParam() { Key = "P_ROLE_CODE", Value = ROLE_CODE });
            param.Add(new TParam() { Key = "P_SITE_CODE", Value = T_SITE_CODE });
            //Param List---------------------------------end
            tr.dict = param;
            string inputJson = (new JavaScriptSerializer()).Serialize(tr);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(APILink.GetDataParam, inputJson);
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(json);
            DataTable dt = ds.Tables[1];
            return dt;


        }
        //private DataTable GetAmbList(string P_SITE_CODE)
        // {
        //    TRequest tr = new TRequest();
        //    List<TParam> param = new List<TParam>();
        //    //Param List---------------------------------start
        //    tr.db = "PatientCare";
        //    tr.sp = "GET_AMB_LIST_WITH_PAT_INFO";
        //    param.Add(new TParam() { Key = "P_SITE_CODE", Value = P_SITE_CODE });
        //    //Param List---------------------------------end
        //    tr.dict = param;
        //    string inputJson = (new JavaScriptSerializer()).Serialize(tr);
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;
        //    string json = client.UploadString(APILink.GetDataParam, inputJson);
        //    DataSet ds = JsonConvert.DeserializeObject<DataSet>(json);
        //    DataTable dt = ds.Tables[1];
        //    return dt;

        //}
        private DataTable GetAmbList(string P_SITE_CODE, string user)
        {
            TRequest tr = new TRequest();
            List<TParam> param = new List<TParam>();
            //Param List---------------------------------start
            tr.db = "PatientCare";
            tr.sp = "WITH_PAT_INFO_TEST_CON_TEST";
            param.Add(new TParam() { Key = "P_SITE_CODE", Value = P_SITE_CODE });
            param.Add(new TParam() { Key = "user", Value = user });
            //Param List---------------------------------end
            tr.dict = param;
            string inputJson = (new JavaScriptSerializer()).Serialize(tr);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(APILink.GetDataParam, inputJson);
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(json);
            DataTable dt = ds.Tables[1];
            return dt;

        }
        //----------------------For Ambulance Login (DocList)---------------------------
        public string GetSiteCode(string P_USER_ID)
        {
            DataTable dt = _t74131DAL.GetSite(P_USER_ID);
            string list = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list += dt.Rows[i]["T_SITE_CODE"]+",";
            }
            string Code = list.TrimEnd(',');
            return Code;
        }
        //private DataTable GetSite(string P_USER_ID)
        //{
        //    TRequest tr = new TRequest();
        //    List<TParam> param = new List<TParam>();
        //    //Param List---------------------------------start
        //   // string Code = GetSiteCode(P_USER_ID); ;
        //    string Code ="";
        //    tr.db = "PatientCare";
        //    tr.sp = "GET_DOC_LIST_T02039";
        //    param.Add(new TParam() { Key = "P_SITE_CODES", Value = Code });
        //    //Param List---------------------------------end
        //    tr.dict = param;
        //    string inputJson = (new JavaScriptSerializer()).Serialize(tr);
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;
        //    string json = client.UploadString(APILink.GetDataParam, inputJson);
        //    DataSet ds = JsonConvert.DeserializeObject<DataSet>(json);
        //    DataTable dt = ds.Tables[1];
        //    return dt;


        //}
        private DataTable GetSite(string P_USER_ID)
        {
            TRequest tr = new TRequest();
            List<TParam> param = new List<TParam>();
            //Param List---------------------------------start
            // string Code = GetSiteCode(P_USER_ID); ;
            string Code = "";
            tr.db = "PatientCare";
            tr.sp = "GET_DOC_LIST_TEST_CON_TEST1";
            param.Add(new TParam() { Key = "P_SITE_CODES", Value = Code });
            param.Add(new TParam() { Key = "P_USER_ID", Value = P_USER_ID });
            //Param List---------------------------------end
            tr.dict = param;
            string inputJson = (new JavaScriptSerializer()).Serialize(tr);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(APILink.GetDataParam, inputJson);
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(json);
            DataTable dt = ds.Tables[1];
            return dt;


        }
    }
}
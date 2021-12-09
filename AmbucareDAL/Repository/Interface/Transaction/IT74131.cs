using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbucareDAL.Models;
using System.Collections;
using System.Data;

namespace AmbucareDAL.Repository.Interface.Transaction
{
    public interface IT74131 : IDisposable
    {
        string GetSite();
        DataTable GetPatInfo(string P_USER_ID);
        DataTable GetPreviousMedicine(int requId);
        void chatHistory(T74098 t74098);
        DataTable GetChatHistory(int T_REQUEST_ID,string T_SENDER_ID, string T_RECIEVER_ID);
        string setDoc(string uid);

        void Dispose();
        string SaveSuggestedMedicine(int requestID, List<T74040> save40List, string lang);
        string saveRequestHospital(int requestId, string hosId, string lang);

        DataTable reqListForTeam(string user, string zone);
        DataTable reqListForDoc(string user, string zone);
        string acptReqofDoc(string user, string zone);
        string closeChat(string user, int req,string lang);
        string onRcvPatReq(string user, int req, string lang);
        string conWthDoc(string user, string zone);
        int chkConn(string user, string zone);
        DataTable getDocID(string user);

        //-----------------
        string chkReqHos(int requestId, string lang);

        string setDoc_new(string zone,int req, string user,string doc);
        string setNewConv(int req, string doc);
        string emergenceyReq(int req, string text, string user);
        DataTable getPatMsg(int req);
        int chkPat(string req);
        DataTable getPlaceHolder(string label);
    }
}

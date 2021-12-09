using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ambucare.Models;
using AmbucareDAL.Repository.Interface.Transaction;

namespace Ambucare.Controllers.Transaction
{
    public class TransactionController : Controller
    {
        //private readonly IT74116 repository;

        //public TransactionController(IT74116 ObjectIRepository)
        //{
        //    repository = ObjectIRepository;
        //}
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74037")]public ActionResult T74037()//For Issue
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74041")]public ActionResult T74041()//For Patient Transaction  Master
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74046")]public ActionResult T74046()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "test")]public ActionResult test() // Blood Group
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74028")]public ActionResult T74028()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74111")]public ActionResult T74111()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74015")]public ActionResult T74015()
        {
            return View();
        }
        //[AuthorizeUserAccessLevel(UserRole = "T74027")]public ActionResult T74027()
        //{
        //    return View();
        //}
        [AuthorizeUserAccessLevel(UserRole = "T74113")]public ActionResult T74113()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74114")]public ActionResult T74114()//Purchase Requsition
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74115")]public ActionResult T74115()//Purchase List
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74116")]public ActionResult T74116()//Purchase Receive
        {
            // var receiveBy = repository.getpurProduct(41);
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74117")]public ActionResult T74117()//Sale Issue/Requsition
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74118")]public ActionResult T74118()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74119")]public ActionResult T74119()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74120")]public ActionResult T74120()// For Transfer Issu/ Requsition 
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74121")]public ActionResult T74121()// For Transfer Price Setup 
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74122")]public ActionResult T74122()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74123")]public ActionResult T74123() // For Active patient List 
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74124")]public ActionResult T74124()// For Discharging patient 
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74125")]public ActionResult T74125()// For Discharging  
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74127")]public ActionResult T74127()// Maintenance
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74128")]public ActionResult T74128()// Instance Prescription
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74129")]public ActionResult T74129()// Repairing
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74130")]public ActionResult T74130()// Repairing
        {
            return View();
        }
        //[AuthorizeUserAccessLevel(UserRole = "T74131")]
        public ActionResult T74131()
        {
            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "T74132")]public ActionResult T74132()
        {
            return View();
        }
       // [AuthorizeUserAccessLevel(UserRole = "T74134")]
        public ActionResult T74134()
        {
            return View();
        }
        //[AuthorizeUserAccessLevel(UserRole = "T74135")]
        //public ActionResult T74135()
        //{
        //    return View();
        //}
        [AuthorizeUserAccessLevel(UserRole = "T74136")]
        public ActionResult T74136()
        {
            return View();
        }
        public ActionResult T74042()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ambucare.Models;

namespace Ambucare.Controllers.Initialization
{
    public class InitializationController : Controller
    {
        // GET: Initialization
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult T74000() //for Label
        {
            return View();
        }


        public ActionResult T74001() //for nationality
        {
            return View();
        }

        public ActionResult T74048() //for nationality
        {
            return View();
        }

        public ActionResult T02005() //for Religion
        {
            return View();
        }

        public ActionResult T74003() //for UM Id
        {
            return View();
        }

        public ActionResult T74004() //for Employee Information 
        {
            return View();
        }
        public ActionResult T02006() //for Gender
        {
            return View();
        }
        public ActionResult T02007() //for Marital
        {
            return View();
        }
        public ActionResult T74008() //for Product type
        {
            return View();
        }

        public ActionResult T74052() //for zone
        {
            return View();
        }

        public ActionResult T74053() //for Hospital
        {
            return View();
        }
        public ActionResult T74054() //for Hospital
        {
            return View();
        }

        public ActionResult T74010() //for Product
        {
            return View();
        }

        public ActionResult T03001() //for Patient
        {
            return View();
        }


        public ActionResult T74002()
        {
            return View();
        }

        public ActionResult T74005() //Employee Type
        {
            return View();
        }

        public ActionResult T74006() //Employee Designation
        {
            return View();
        }

        public ActionResult T74020() //Education board 
        {
            return View();
        }

        public ActionResult T74023() //Department 
        {
            return View();
        }

        public ActionResult T74009()
        {
            return View();
        }


        public ActionResult T74012()
        {
            return View();
        }

        public ActionResult T74013()
        {
            return View();
        }

        public ActionResult T74014()
        {
            return View();
        }

        public ActionResult T74011()
        {
            return View();
        }

        public ActionResult T74016() // Currency Information
        {
            return View();
        }

        public ActionResult T74017()
        {

            return View();
        }

        public ActionResult T74018()
        {
            return View();
        }

        public ActionResult T74019()
        {
            return View();
        }

        public ActionResult T74021()
        {
            return View();
        }

        public ActionResult T74022()
        {
            return View();
        }

        public ActionResult T74066()
        {
            return View();
        }

        public ActionResult T74024()
        {
            return View();
        }

        public ActionResult T74025()
        {
            return View();
        }

        public ActionResult T74069()
        {
            return View();
        }

        public ActionResult T74055()
        {
            return View();
        }

        public ActionResult T74038() // Service Charge(according to ambulance Size)	
        {
            return View();
        }

        public ActionResult T74068() // Message Code Generate
        {
            return View();
        }

        public ActionResult T74071() // Role Information
        {
            return View();
        }

        public ActionResult T74073() // Role Information
        {
            return View();
        }

        public ActionResult T74035() //Department(Science/Arts)
        {
            return View();
        }

        public ActionResult T74112()
        {
            return View();
        }
        public ActionResult T74056()
        {
            return View();
        }

        public ActionResult T74044() //for nationality
        {
            return View();
        }
        public ActionResult T74047()
        {
            return View();
        }
        public ActionResult T74075()
        {
            return View();
        }

        public ActionResult T74045()
        {
            return View();
        }
        public ActionResult fileUpload()
        {
            return View();
        }

        public ActionResult T74126()
        {
            return View();
        }
        public ActionResult T74096()
        {
            return View();
        }

        public ActionResult T74133()
        {
            return View();
        }
        public ActionResult T74137()
        {
            return View();
        }
        public ActionResult T74142()
        {
            return View();
        }
        public ActionResult T74143()
        {
            return View();
        }
        public ActionResult T74144()
        {
            return View();
        }
        public ActionResult T74145()
        {
            return View();
        }
        public ActionResult T74146()
        {
            return View();
        }
        public ActionResult T74147()
        {
            return View();
        }

        public ActionResult T74148()
        {
            return View();
        }
        public ActionResult T74149()
        {
            return View();
        }

        [AuthorizeUserAccessLevel(UserRole = "T74135")]
        public ActionResult T74135()
        {
            return View();
        }

        [AuthorizeUserAccessLevel(UserRole = "T74027")]
        public ActionResult T74027()
        {
            return View();
        }

        public ActionResult T74150()
        {
            return View();
        }


        public ActionResult T74151()
        {
            return View();
        }

        //public ActionResult T74007()
        //{
        //    return View();
        //}
        //public ActionResult T74057()
        //{
        //    return View();
        //}

    }
}

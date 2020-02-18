using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using BBL;
namespace Web.Controllers
{
    public class LoginController : Controller, System.Web.SessionState.IRequiresSessionState
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult CheckAdmin(admin a)
        {
            return  Json(new AdminBBL().CheckAdmin(a), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdataAdmin(admin a)
        {
            return  Json(new AdminBBL().UpdataAdmin(a), JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoginCheck(admin admin)
        {
            int r = new AdminBBL().LoginCheck(admin);
            if (r >0)
            {
                Session.Add("username", admin.username);
                Session.Add("toke", admin);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
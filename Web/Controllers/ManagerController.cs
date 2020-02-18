using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BBL;
using DAL;
using NPOI.HSSF.UserModel;
using Tools;

namespace Web.Controllers
{
    public class ManagerController : Controller, System.Web.SessionState.IRequiresSessionState
    {
        // GET: Manager
        public ActionResult ExportExcle(string where) 
        {
            List<mis_manager> list = null;
            if (where.Equals(null)||where==null||where==""||where.Equals(""))
            {
               list = ManagerBBL.ExportExcle();//获取List数据 可自行获取

            }
            else 
            {
                list = ManagerBBL.ExportExcle(where);//获取List数据 可自行获取

            }
            Dictionary<string, string> FiedNames = new Dictionary<string, string>();
            FiedNames.Add("编号", "id");
            FiedNames.Add("姓名", "name");
            FiedNames.Add("收件地址", "addres");
            FiedNames.Add("电话", "phone");
            FiedNames.Add("任教科目", "subject");
            FiedNames.Add("年级", "class");
            FiedNames.Add("时间", "logDate");
            FiedNames.Add("备注", "remarks");
            string sExportFileName = "";
            HSSFWorkbook wb = null;
            wb = WorkBook.BuildSwitchData<mis_manager>("Sheet1", list, FiedNames);//调用通用方法
            sExportFileName = "信息" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";//设置下载文件的名称
            MemoryStream stream = new MemoryStream();
            wb.Write(stream);
            var buf = stream.ToArray();
            return File(buf, "pplication/vnd.ms-excel", sExportFileName);
        }
        public ActionResult Manager()
        {
            
            if (Session["toke"] == null)
            {
              return  Redirect("~/Login/Login");
            }
            return View();
        }
        public ActionResult AddManager()
        {
            return View();
        }
        public JsonResult CheckManager(mis_manager mis)
        {

            
            return Json(new ManagerBBL().CheckManager(mis), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FindManageList(int page, int limit ,string where)
        {
            List<mis_manager> list;
            int count;
            if (where != null)
            {
                 count=new  ManagerBBL().GetManagerCount(where);
                    list = new ManagerBBL().GetManagerList(page,limit,where);

            }
            else
            {
                count =new  ManagerBBL().GetManagerCount();
                list = new ManagerBBL().GetManagerList(page,limit);
            }

            List<manager> dlist = new List<manager>();
            foreach (var v in list) 
            {
                manager manager = new manager();
                manager.id = v.id;
                manager.name = v.name;
                manager.addres = v.addres;
                manager.phone = v.phone;
                manager.subject = v.subject;
                manager.@class = v.@class;
                manager.logDate = v.logDate.ToString();
                manager.remarks = v.remarks;
                dlist.Add(manager);

            }
            Dictionary<string, object> dir = new Dictionary<string, object>();
                dir.Add("code", 0);
                dir.Add("msg", "");
                dir.Add("count", count);
                dir.Add("data", dlist);
                return Json(dir, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult AddManagerInfo(mis_manager misManager)
        {
            int r=new ManagerBBL().AddManagerInfo(misManager);
            if (r >= 1)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        

    }
}
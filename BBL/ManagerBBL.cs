using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BBL
{
    public class ManagerBBL
    {
        public List<mis_manager> FindManagerList()
        {
            return  new ManagerDAL().FindManagerList();
        }

        public List<mis_manager> GetManagerList(int pageIndex,int limit,string name)
        {
            return new ManagerDAL().GetManagerList(pageIndex,limit,name);
        }

        public static List<mis_manager> ExportExcle()
        {
            return new ManagerDAL().ExportExcle();
        }

        public List<mis_manager> GetManagerList(int pageIndex,int limit)
        {
            return new ManagerDAL().GetManagerList(pageIndex,limit);
        }

        public static List<mis_manager> ExportExcle(string where)
        {
            return new ManagerDAL().ExportExcle(where);
        }

        public int AddManagerInfo(mis_manager misManager)
        {
            return new ManagerDAL().AddManagerInfo(misManager);
        }

        public int GetManagerCount()
        {
            return new ManagerDAL().GetManagerCount();
        }

        public bool CheckManager(mis_manager mis)
        {
           return new ManagerDAL().CheckManager(mis);
        }

        public int GetManagerCount(string where)
        {
            return new ManagerDAL().GetManagerCount(where);
        }
    }
}

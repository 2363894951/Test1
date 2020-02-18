using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class ManagerDAL
    {
        public bool CheckManager(mis_manager mis)
        {
            helpsEntities helps = new helpsEntities();
//            string sql = "select * from mis_manager where name=@name and  addres=@phone";
            //List<mis_manager> list= helps.Database.SqlQuery<mis_manager>(sql).ToList();
            List<mis_manager> list = helps.mis_manager.Where(m=>m.name==mis.name && m.phone==mis.phone).ToList();
            if (list.Count > 0) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public List<mis_manager> ExportExcle()
        {
            helpsEntities helps = new helpsEntities();

            return helps.mis_manager.ToList();
        }

        public List<mis_manager> ExportExcle(string where)
        {
            helpsEntities helps = new helpsEntities();
            string sql = "select * from mis_manager where CONCAT( [name],[addres],[phone] ,[subject],[class]) LIKE '%name%';";
            return helps.mis_manager.SqlQuery(sql).ToList();
        }

        public List<mis_manager> FindManagerList()
        {
            helpsEntities helps = new helpsEntities();
           return helps.mis_manager.ToList();
        }

        public List<mis_manager> GetManagerList(int pageIndex ,int pageSize,string where)
        {   
            helpsEntities helps = new helpsEntities();
            string     sql = "declare @pagesize int declare @pageindex int set @pageindex="+pageIndex+" set @pagesize="+pageSize+ " select* from(select ROW_NUMBER() OVER(ORDER BY ID desc) as num,*from[mis_manager] where CONCAT( [name],[addres],[phone] ,[subject],[class]) LIKE '%" + where+"%') temp where num between @pageIndex* @pagesize-(@pageSize - 1) and @pageIndex*@pageSize";
            return helps.mis_manager.SqlQuery(sql).ToList();
        }
        public List<mis_manager> GetManagerList(int pageIndex ,int pageSize)
        {            
            helpsEntities helps = new helpsEntities();
            string     sql = "declare @pagesize int declare @pageindex int set @pageindex="+pageIndex+" set @pagesize="+pageSize+ " select* from(select ROW_NUMBER() OVER(ORDER BY ID desc) as num,*from[mis_manager]) temp where num between @pageIndex* @pagesize-(@pageSize - 1) and @pageIndex*@pageSize";
            return helps.mis_manager.SqlQuery(sql).ToList();
        }
        public int AddManagerInfo(mis_manager misManager)
        {
            helpsEntities helps = new helpsEntities();
            helps.mis_manager.Add(misManager);
            helps.SaveChanges();
            return 1;
        }

        public int GetManagerCount()
        {
            helpsEntities helps = new helpsEntities();
            return helps.mis_manager.Count();
        }
        public int GetManagerCount(string where)
        {
            helpsEntities helps = new helpsEntities();
//            return helps.mis_manager.Where(c=>c.name==name).Count();
            string sql ="SELECT * FROM mis_manager WHERE CONCAT( [name],[addres],[phone] ,[subject],[class]) LIKE '%"+where+"%'";
            List<mis_manager> list = helps.mis_manager.SqlQuery(sql).ToList();
            return list.Count;
        }

      
    }
}

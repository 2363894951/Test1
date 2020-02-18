using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdminDAL
    {
        
        public bool UpdataAdmin(admin a)
        { 
            helpsEntities helps = new helpsEntities();
//            string sql = "update  admin set password='"+a.password+"' where username='"+a.username+"'";
            admin admin = helps.admin.Where(c => c.username == a.username).FirstOrDefault();
            admin.password = a.password;
            if (helps.SaveChanges() > 0) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public bool CheckAdmin(admin a)
        {
            helpsEntities helps = new helpsEntities();
//            string sql = "update  admin set password='"+a.password+"' where username='"+a.username+"'";
            //List<mis_manager> list= helps.Database.SqlQuery<mis_manager>(sql).ToList();
            List<admin> list = helps.admin.Where(m=>m.username==a.username && m.password==a.password).ToList();
            if (list.Count > 0) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public int LoginCheck(admin admin)
        {
            helpsEntities helps = new helpsEntities();
            List<admin> list = helps.admin.Where(u => u.username == admin.username && u.password == admin.password).ToList();
            if (list.Count >= 1)
            {
                return list.Count;
            }
            else 
            {
                return -1;
            }

        }

        
    }
}

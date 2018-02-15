using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Model;
using System.Web;

namespace Birthday.BLL
{
    public class SessionService:IDisposable
    {
        public Model.LoginUser CurrentUser
        {
            get
            {
                return HttpContext.Current.Session["currentUser"] as Model.LoginUser;
            }

            set
            {
                HttpContext.Current.Session["currentUser"] = value;
            }
        }

       

        public void Dispose()
        {
            
        }
    }
}

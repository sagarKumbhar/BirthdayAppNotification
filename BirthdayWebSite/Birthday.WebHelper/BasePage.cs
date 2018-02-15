using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.BLL;

namespace Birthday.WebHelper
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            base.InitializeCulture();
        }

        /// <summary>
        /// OnPreRender method for all inherited pages
        /// </summary>
        /// <param name="e">Event argument</param>
        protected override void OnPreRender(EventArgs e)
        {
            this.Check();
            base.OnPreRender(e);
        }

        /// <summary>
        /// Check for url form
        /// </summary>
        private void Check()
        {
            if (Request.Url != null)
            {
                using (SessionService ss = new SessionService())
                {
                    if (ss.CurrentUser == null)
                    {
                        Response.Redirect("Login.html");
                    }
                    else
                    {
                        if(ss.CurrentUser.Profile.ProfileName == "Global HR Admin")
                        {
                           if(Request.Url.ToString().Contains("ManageUser") || Request.Url.ToString().Contains("ManageRecepient"))
                            {
                                Response.Redirect("UnAuthorized.html");
                            }
                          
                        }
                        else if(ss.CurrentUser.Profile.ProfileName == "HR User")
                        {
                            if (Request.Url.ToString().Contains("ManageHRUser") )
                            {
                                Response.Redirect("UnAuthorized.html");
                            }
                        }
                    }
                }

            }
        }
    }
}

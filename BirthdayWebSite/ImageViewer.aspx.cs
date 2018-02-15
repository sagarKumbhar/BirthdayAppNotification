using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImageViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["FileName"] != null)
        {
            try
            {
                Byte[] bytes = null;
                string filePath = Request.QueryString["FileName"];


                string contenttype = "image/" +
                   Path.GetExtension(filePath).Replace(".", string.Empty);
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();



                //Write the file to response Stream
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.Public);
                Response.ContentType = contenttype;
                Response.AddHeader("content-disposition", "attachment;filename=image");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }
            catch
            {
            }
        }
    }
}
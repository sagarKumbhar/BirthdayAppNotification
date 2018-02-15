using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Birthday.BLL;
using Birthday.Model;
using Birthday.WebHelper;
using Birthday.ViewModel;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Birthday.BLL.EntityService;

public partial class ManageUser : BasePage
{
    SessionService sessionser = new SessionService();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (sessionser.CurrentUser != null)
        {
            txtDepartment.Value = sessionser.CurrentUser.Department.DepartmentName.ToString();
            if (!Page.IsPostBack)
            {
                FillUserList();
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidFile = (fuploadEmpPic.PostedFile == null || fuploadEmpPic.PostedFile.FileName == "") ? true : false;
            string[] validFileTypes = { "jpg", "png" };
            string ext = System.IO.Path.GetExtension(fuploadEmpPic.FileName);
            if (!isValidFile)
            {
                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (ext.ToUpper() == "." + validFileTypes[i].ToUpper())
                    {
                        isValidFile = true;
                        break;
                    }
                }
            }

            if (isValidFile)
            {
                int Departmentid = 0;
                using (SessionService sser = new SessionService())
                {
                    Departmentid = sser.CurrentUser.DepartmentId;
                }
                if (hidID.Value == "")//Create
                {
                    Employee objEmp = new Employee();
                    objEmp.EmployeeName = name.Value;

                    objEmp.Email = Email.Value;
                    objEmp.EmployeeID = Convert.ToInt32(EmpID.Value);
                    objEmp.Birthdate = DateTime.ParseExact(bdate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objEmp.DepartmentId = Departmentid;


                    using (EmployeeService empSer = new EmployeeService())
                    {


                        int retVal = empSer.AddEmployee(objEmp);
                        if (retVal == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key4", "ShowAlert('danger','Employee already present with same Email or Employee ID.');", true);
                        }
                        else
                        {
                            string uploadPicLoc = "";
                            if (isValidFile && (fuploadEmpPic.PostedFile != null && fuploadEmpPic.PostedFile.FileName != ""))
                            {
                                uploadPicLoc = UplaodPictureandResize(ext, retVal);
                                empSer.UpdateEmployeePhotoLocation(uploadPicLoc, retVal);

                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ShowAlert('success','Details Saved Successfully. However,please upload a photo in requested format.');ResetCreateBox();", true);
                            }

                        }
                    }
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ShowAlert('success','Saved Successfully.');ResetCreateBox();", true);
                }
                else if (hidID.Value != "")
                {
                    Employee objEmp = new Employee();
                    objEmp.EmployeeName = name.Value;
                    objEmp.Email = Email.Value;
                    objEmp.EmployeeID = Convert.ToInt32(EmpID.Value);
                    objEmp.Birthdate = DateTime.ParseExact(bdate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objEmp.DepartmentId = Departmentid;
                    objEmp.Id = Convert.ToInt32(hidID.Value);


                    using (EmployeeService empSer = new EmployeeService())
                    {
                        int retVal = empSer.EditEmployee(objEmp); ;
                        if (retVal == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key3", "ShowAlert('danger','Employee already present with same Email or Employee ID.');", true);
                        }
                        else
                        {
                            if (isValidFile && (fuploadEmpPic.PostedFile != null && fuploadEmpPic.PostedFile.FileName != ""))
                            {
                                string uploadPicLoc = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PhotoLocation"].ToString(), objEmp.Id.ToString() + ext);

                                if (File.Exists(uploadPicLoc))
                                {
                                    File.Delete(uploadPicLoc);
                                }
                                uploadPicLoc = UplaodPictureandResize(ext, objEmp.Id);
                                //fuploadEmpPic.PostedFile.SaveAs(uploadPicLoc);

                                empSer.UpdateEmployeePhotoLocation(uploadPicLoc, objEmp.Id);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ShowAlert('success','Details Saved Successfully. However,please upload a photo in requested format.');ResetCreateBox();", true);
                            }
                        }
                    }
                    hidID.Value = "";
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "key9", "ShowAlert('success','Saved Successfully.');ResetCreateBox();", true);
                }
                //Response.Write("<script>ShowAlert('success','Saved Successfully.');ResetCreateBox();</script>");


            }
            else
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), "key9", "ShowAlert('warning','Please check uploaded photo format and size.');", true);
            }
            FillUserList();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key2", "ShowAlert('danger','Error occurred in current operation!! - " + ex.Message + "');", true);
        }

    }

    private string UplaodPictureandResize(string ext, int retVal)
    {
        string uploadPicLoc;
        //////////////
        Stream strm = fuploadEmpPic.PostedFile.InputStream;
        using (var image = System.Drawing.Image.FromStream(strm))
        {
            // Print Original Size of file (Height or Width)   
            //Label1.Text = image.Size.ToString();
            int newWidth = 200; // New Width of Image in Pixel 

            // int newHeight = 230; // New Height of Image in Pixel  

            int newHeight = (image.Size.Height * newWidth / image.Size.Width);

            var thumbImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imgRectangle);
            // Save the file  
            uploadPicLoc = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PhotoLocation"].ToString(), retVal.ToString() + ext);
            thumbImg.Save(uploadPicLoc, image.RawFormat);
            // Print new Size of file (height or Width)  

        }
        /////////////////
        return uploadPicLoc;
    }

    protected void btnUplaodExcel_Click(object sender, EventArgs e)
    {
        if (fuploadExcel.PostedFile == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key5", "ShowAlert('danger','Please select the file again');", true);
            return;
        }
        string[] validFileTypes = { "xlsx", "xls" };
        string ext = System.IO.Path.GetExtension(fuploadExcel.PostedFile.FileName);
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext.ToUpper() == "." + validFileTypes[i].ToUpper())
            {
                isValidFile = true;
                break;
            }
        }

        if (!isValidFile)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key6", "ShowAlert('danger','Invalid File.');", true);
            return;
        }
        string filepath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["MigrationTempFiles"].ToString(), fuploadExcel.FileName); //Server.MapPath("MigrationTempFiles/") + fuploadExcel.FileName;
        fuploadExcel.PostedFile.SaveAs(filepath);
        if (System.IO.File.Exists(filepath))
        {
            int Departmentid = 0;
            using (SessionService sser = new SessionService())
            {
                Departmentid = sser.CurrentUser.DepartmentId;
            }

            ExcelImporter excel = new ExcelImporter();

            System.Data.DataTable dtData = excel.ReadandUpload(filepath);
            using (EmployeeService empser = new EmployeeService())
            {
                string msg = "";
                string alert = "";
                bool retVal = empser.MigrateExcelFileData(dtData, Departmentid, ref msg);
                if (msg != "")
                {
                    string[] arr = msg.Split('$');
                    foreach (string s in arr)
                    {
                        alert += (s + "<br/>");
                    }
                }

                if (retVal)
                {
                    if (msg == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key7", "ShowAlert('success','File Uploaded Successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key7", "ShowAlertExcel('success','File Uploaded Successfully but " + alert + "');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key8", "ShowAlertExcel('danger','UnExpected Error has occured.Please check the file contents.');", true);
                }
            }
        }
        FillUserList();
    }

    private void FillUserList()
    {
        List<Employee> lstUser = new List<Employee>();
        using (EmployeeService EmpSrv = new EmployeeService())
        {
            if (sessionser.CurrentUser.Profile.ProfileName == "IT administrator")
            {
                lstUser = EmpSrv.GetAllEmployee();

            }
            else
            {
                lstUser = EmpSrv.GetAllEmployeeByDepartmentID(sessionser.CurrentUser.DepartmentId);

            }

        }

        object[] arrObj = new object[lstUser.Count];
        for (int i = 0; i < arrObj.Length; i++)
        {

            arrObj[i] = new
            {
                ID = (lstUser[i].Id).ToString(),
                EmployeeName = lstUser[i].EmployeeName,
                Birthdate = lstUser[i].Birthdate.Value.ToString("dd/MM/yyyy"),
                Email = lstUser[i].Email,
                EmployeeId = lstUser[i].EmployeeID.ToString(),
                PhotoUploaded = lstUser[i].PhotoLocation != "" ? "Yes" : "No",
                JoiningDate = lstUser[i].JoiningDate.HasValue ? lstUser[i].JoiningDate.Value.ToString("dd/MM/yyyy") : "",
                MarriageDate = lstUser[i].MarriageAnnivarsary.HasValue ? lstUser[i].MarriageAnnivarsary.Value.ToString("dd/MM/yyyy") : ""
            };
        }
        var json = new JavaScriptSerializer().Serialize(arrObj);//BindListTable('" + json + "');
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "updateDT", "BindListTable('" + json + "');", true);
        upDatatable.Update();
    }

    [WebMethod]
    public static string GetUserById(string id)
    {
        Employee emp = null;
        using (EmployeeService empSer = new EmployeeService())
        {
            emp = empSer.GetEmployeebyId(Convert.ToInt32(id));
        }
        object[] arrObj = new object[1];
        arrObj[0] = new
        {
            ID = emp.Id.ToString(),
            EmployeeName = emp.EmployeeName,
            Birthdate = emp.Birthdate.Value.ToString("dd/MM/yyyy"),
            Email = emp.Email,
            EmployeeId = emp.EmployeeID.ToString(),
            PhotoLocation = emp.PhotoLocation != "" ? ("ImageViewer.aspx?FileName=" + emp.PhotoLocation) : "",
            JoiningDate = emp.JoiningDate.HasValue ? emp.JoiningDate.Value.ToString("dd/MM/yyyy") : "",
            MarriageDate = emp.MarriageAnnivarsary.HasValue ? emp.MarriageAnnivarsary.Value.ToString("dd/MM/yyyy") : ""
        };
        var json = new JavaScriptSerializer().Serialize(arrObj);
        return json;
    }

    [WebMethod]
    public static string DeleteEmployee(string id)
    {
        int retVal = -1;
        using (EmployeeService empSer = new EmployeeService())
        {
            retVal = empSer.DeleteEmployee(Convert.ToInt32(id));
        }
        if (retVal > 0)
        {

            return "success";
        }
        else
        {
            return "danger";
        }
    }


    protected void btnAutoUpdate_Click(object sender, EventArgs e)
    {
        FillUserList();
        upDatatable.Update();

    }

    protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
    {
        string filename = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["TemplateLocation"].ToString(), "template.xlsx");
        FileInfo fileInfo = new FileInfo(filename);

        if (fileInfo.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "inline;attachment; filename=" + fileInfo.Name);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.Flush();
            Response.WriteFile(fileInfo.FullName);
            Response.End();
        }


      ;
    }
}
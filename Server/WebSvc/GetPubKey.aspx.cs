using System;
using System.Text;
using System.Web;
using System.Configuration;

using ApiUtil;
using SysMetricsDB;

namespace DGPWebSvc
{
    public partial class GetPubKey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MsgUtil msgutil = new MsgUtil();

            string respMsg = string.Empty;
            string _appname = ConfigurationManager.AppSettings["WebSvcName"].ToString();

            try
            {
                if (ConfigurationManager.AppSettings["LocState"].ToString() == LocState.Primary || ConfigurationManager.AppSettings["LocState"].ToString() == LocState.Backup)
                {
                    respMsg = ConfigurationManager.AppSettings["PubKey"].ToString();
                }
                else
                {
                    respMsg = "ERROR: Location is not available.";
                }
            }
            catch (Exception ex)
            {
                ServerErrLog.LogException("guestuser", _appname, "GetPubKey.Page_Load", ex);
                respMsg = "ERROR";
            }
            finally
            {
                Response.ContentType = "text/xml";
                Response.AppendHeader("Content-Disposition", "filename=respmsg.xml");
                Response.AppendHeader("Content-Length", Encoding.UTF8.GetBytes(respMsg).Length.ToString());
                Response.Charset = "utf-8";
                Response.Write(respMsg);

                try
                {
                    Response.Flush();
                    Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // https://support.microsoft.com/en-us/kb/312629
                }
                catch { }
            }
        }
    }
}
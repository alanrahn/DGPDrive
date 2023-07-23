using System;
using System.IO;
using System.Text;
using System.Web;
using System.Configuration;

using SvrUtil;
using CmnUtil;
using CliUtil;

namespace WebApp
{
    public partial class DGPproxy : System.Web.UI.Page
    {
        string svcURL = ConfigurationManager.AppSettings["SvcURL"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string respMsg = string.Empty;
            SvrMsgUtil svrmsgutil = new SvrMsgUtil();
            CliMsgUtil climsgutil = new CliMsgUtil();
            
            try
            {
                if (ConfigurationManager.AppSettings["LocState"].ToString() == "ONLINE")
                {
                    // check request content type
                    if (HttpContext.Current.Request.ContentType == "text/xml")
                    {
                        // read the HTTP body into a string (API request message) ignoring all other parts of the HTTP request
                        StreamReader stream = new StreamReader(Request.InputStream);
                        string requestMsg = stream.ReadToEnd();

                        // (optional) run some checks to filter the content of the API request messsage - none at this time.


                        // forward the API request message to the web service as a new HTTP request and receive its response
                        respMsg = climsgutil.HttpPost(svcURL, requestMsg);
                    }
                    else
                    {
                        string resultMsg = svrmsgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, MethResult.Error, MsgData.Text, "Only text/xml request content type is allowed.");
                        respMsg = svrmsgutil.CreateXMLResponse(AuthState.MimeType, "Incorrect Mime Type: Only text/xml request content type is allowed.", "0", "0", resultMsg, "", "", "false", "");
                    }
                }
                else
                {
                    string resultMsg = svrmsgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, MethResult.Error, MsgData.Text, "The web service is currently offline.");
                    respMsg = svrmsgutil.CreateXMLResponse(AuthState.OffLine, "The web service is currently offline.", "0", "0", resultMsg, "", "", "false", "");
                }
            }
            catch (Exception ex)
            {
                RemErrLog.WriteErrToEV("NA", Environment.MachineName, "DGPWebApp", "DGPproxy", "Page_Load", "EXCEPTION", ex.Message, ex.StackTrace);
                string resultMsg = svrmsgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, MethResult.Exception, MsgData.Text, ex.Message);
                respMsg = svrmsgutil.CreateXMLResponse(AuthState.Exception, "The web service controller encountered an exception.", "0", "0", resultMsg, "", "", "false", "");
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
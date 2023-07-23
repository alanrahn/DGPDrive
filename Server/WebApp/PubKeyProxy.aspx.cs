using System;
using System.IO;
using System.Text;
using System.Web;
using System.Configuration;

using ApiUtil;

namespace WebApp
{
    public partial class PubKeyProxy : System.Web.UI.Page
    {
        string keyURL = ConfigurationManager.AppSettings["KeyURL"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string respMsg = string.Empty;
            MsgUtil msgutil = new MsgUtil();

            try
            {
                if (ConfigurationManager.AppSettings["LocState"].ToString() == "ONLINE")
                {
                    // check request content type
                    if (HttpContext.Current.Request.ContentType == "text/xml")
                    {
                        // read the API request message into a string
                        StreamReader stream = new StreamReader(Request.InputStream);
                        string requestMsg = stream.ReadToEnd();

                        // (optional) run some checks to filter the content of the request messsage - none at this time.


                        // forward the API request message to the web service and receive its response
                        respMsg = msgutil.HttpPost(keyURL, requestMsg);
                    }
                    else
                    {
                        string resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "Only text/xml request content type is allowed.");
                        respMsg = msgutil.CreateXMLResponse("PubKeyProxy", "", AuthState.Content, "Only text/xml request content type is allowed.", "0", resultMsg);
                    }
                }
                else
                {
                    string resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Error, APIData.Text, "The web service is currently offline.");
                    respMsg = msgutil.CreateXMLResponse("PubKeyProxy", "", LocState.Offline, "The web service is currently offline.", "0", resultMsg);
                }
            }
            catch (Exception ex)
            {
                RemoteErrLog.WriteErrToEV("NA", Environment.MachineName, "DGPWebApp", "PubKeyProxy", "Page_Load", "EXCEPTION", ex.Message, ex.StackTrace);
                string resultMsg = msgutil.CreateXMLResult(MethReturn.MethodError, MethReturn.Default, APIResult.Exception, APIData.Text, ex.Message);
                respMsg = msgutil.CreateXMLResponse("PubKeyProxy", "", AuthState.Error, "PubKeyProxy.Page_Load exception." + ex.Message, "0", resultMsg);
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
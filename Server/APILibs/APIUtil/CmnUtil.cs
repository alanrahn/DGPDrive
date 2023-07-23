using System;
using System.Data;
using System.IO;
using System.Web;
using System.Xml.Linq;
using System.Configuration;
using System.Web.Hosting;
using System.Text.RegularExpressions;

namespace ApiUtil
{
    

    /// <summary>
    /// 
    /// </summary>
    public class CmnUtil
    {
        public CmnUtil()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void StoreString(string name, string value)
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application[name] = value;
            HttpContext.Current.Application.UnLock();
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetNewGID()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 
        /// </summary>
        public string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }

        /// <summary>
        /// schemaflag == true returns serialized datatable, otherwise returns plain XML
        /// </summary>
        public string TableToXml(DataTable dtable, bool schemaFlag)
        {
            StringWriter sw = new StringWriter();

            dtable.TableName = "Record";
            if (schemaFlag)
            {
                dtable.WriteXml(sw, XmlWriteMode.WriteSchema);
            }
            else
            {
                dtable.WriteXml(sw, XmlWriteMode.IgnoreSchema);
            }

            return sw.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        public DataTable XmlToTable(string dtablexml)
        {
            DataTable dt = new DataTable();

            StringReader sr = new StringReader(dtablexml);
            dt.ReadXml(sr);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FormatXMLString(string xmlStr)
        {
            XDocument xdoc = XDocument.Parse(xmlStr);

            return xdoc.ToString();
        }

        /// <summary>
        /// calculates the next datetime to run a process given an offset
        /// </summary>
        public DateTime NextDateTime(string timePeriod, double periodValue)
        {
            DateTime nextdatetime = new DateTime();

            // convert various time periods to milliseconds
            switch (timePeriod.ToUpper())
            {
                case "MS":
                    nextdatetime = DateTime.UtcNow.AddMilliseconds(periodValue);
                    break;

                case "SEC":
                    nextdatetime = DateTime.UtcNow.AddSeconds(periodValue);
                    break;

                case "MIN":
                    nextdatetime = DateTime.UtcNow.AddMinutes(periodValue);
                    break;

                case "HOUR":
                    nextdatetime = DateTime.UtcNow.AddHours(periodValue);
                    break;

                case "DAY":
                    nextdatetime = DateTime.UtcNow.AddDays(periodValue);
                    break;

                case "MONTH":
                    nextdatetime = DateTime.UtcNow.AddMonths(Convert.ToInt32(periodValue));
                    break;

                case "TOD":
                    // intervalvalue = total minutes of 24 hour clock
                    nextdatetime = DateTime.UtcNow.AddDays(1).Date;
                    nextdatetime.AddMinutes(periodValue);
                    break;
            }

            return nextdatetime;
        }

        /// <summary>
        /// checks whether or not a password meets the password rules
        /// </summary>
        public string PasswordCheck(string password)
        {
            string pwordresult = "";

            string pwordlength = ConfigurationManager.AppSettings["PasswordLength"].ToString();
            int pwordlen = Convert.ToInt32(pwordlength);

            // check password length
            if (password.Length >= pwordlen)
            {
                // check for capital letter
                if (Regex.IsMatch(password, "[A-Z]"))
                {
                    // check for lowercase letter
                    if (Regex.IsMatch(password, "[a-z]"))
                    {
                        // check for number
                        if (Regex.IsMatch(password, "[0-9]"))
                        {
                            // check for special character
                            if (Regex.IsMatch(password, @"[\.\^\-\*\+\?\|\(\)\/\[\]\{\}!#$%&@]"))
                            {
                                pwordresult = APIResult.OK;
                            }
                            else
                            {
                                pwordresult = APIResult.Error + ": the password must contain at least one special character.";
                            }
                        }
                        else 
                        {
                            pwordresult = APIResult.Error + ": the password must contain at least one number.";
                        }
                    }
                    else
                    {
                        pwordresult = APIResult.Error + ": the password must contain at least one lowercase letter.";
                    }
                }
                else
                {
                    pwordresult = APIResult.Error + ": the password must contain at least one capital letter.";
                }
            }
            else
            {
                pwordresult = "ERROR: the password must be at least " + pwordlength + " characters in length.";
            }

            return pwordresult;
        }

        // ********************************************************************************************************* //
        // ********************************************************************************************************* //
        // ********************************************************************************************************* //

        /// <summary>
        /// returns values from the web service web.config file to the client app
        /// </summary>
        public string GetServiceInfo()
        {
            string svcenckeyver = ConfigurationManager.AppSettings["SvcKeyVersion"].ToString();

            // return concatenated web service web.config values
            return "<WebSvcInfo><WebSvcVer>" + ConfigurationManager.AppSettings["WebSvcVersion"].ToString() + "</WebSvcVer>" +
                    "<MaxSegSize>" + ConfigurationManager.AppSettings["MaxSegSize"].ToString() + "</MaxSegSize>" +
                    "<MinSegSize>" + ConfigurationManager.AppSettings["MinSegSize"].ToString() + "</MinSegSize>" +
                    "<MinPwordLen>" + ConfigurationManager.AppSettings["PasswordLength"].ToString() + "</MinPwordLen></WebSvcInfo>";
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetSystemInfo()
        {
            // return concatenated system values
            return "\nStatus: " + ConfigurationManager.AppSettings["LocState"].ToString() + "," +
                    "\nSystem: " + ConfigurationManager.AppSettings["System"].ToString() + "," +
                    "\nEnvironment: " + ConfigurationManager.AppSettings["Environment"].ToString() + "," +
                    "\nLocation: " + ConfigurationManager.AppSettings["Location"].ToString() + "," +
                    "\nServer: " + Environment.MachineName + "," +
                    "\nIISWebSite: " + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        }

        /// <summary>
        /// utility method to convert the 3 Base64 characters that are not safe in a URL (used on the client)
        /// </summary>
        public static string EncodeBase64URL(string base64Str)
        {
            char[] padding = { '=' };

            return base64Str.TrimEnd(padding).Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        /// utility method to reverse the effects of the EncodeBase64URL method (used on the server)
        /// </summary>
        public static string DecodeBase64URL(string encodedBase64)
        {
            string B64Str = encodedBase64.Replace('_', '/').Replace('-', '+');
            switch (encodedBase64.Length % 4)
            {
                case 2: B64Str += "=="; break;
                case 3: B64Str += "="; break;
            }

            return B64Str;
        }

        /// <summary>
        /// used to get the system clock time time of the host (should be synched to nettime server)
        /// </summary>
        public string GetUTCTimeStr()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetIISServerName()
        {
            return HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetIISWebSiteName()
        {
            return HostingEnvironment.ApplicationHost.GetSiteName();
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetMachineName()
        {
            return Environment.MachineName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetOSName()
        {
            return Environment.OSVersion.VersionString;
        }

        public bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

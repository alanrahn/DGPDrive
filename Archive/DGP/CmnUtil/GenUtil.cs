
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace CmnUtil
{
    public class GenUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public static string UnixTimeString()
        {
            DateTimeOffset dto = DateTimeOffset.UtcNow;
            return dto.ToUnixTimeMilliseconds().ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public static string UnixTimeString(int days)
        {
            DateTimeOffset dto = DateTimeOffset.UtcNow.AddDays(days);
            return dto.ToUnixTimeMilliseconds().ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public static long UnixTimeLong()
        {
            DateTimeOffset dto = DateTimeOffset.UtcNow;
            return dto.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetNewGUID()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        ///
        /// </summary>
        public static DataTable XmlToTable(string dtablexml)
        {
            DataTable dt = new DataTable();

            StringReader sr = new StringReader(dtablexml);
            dt.ReadXml(sr);

            return dt;
        }

        /// <summary>
        /// checks whether or not a password meets the password rules
        /// </summary>
        public static string PasswordCheck(string password)
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
                                pwordresult = MethResult.OK;
                            }
                            else
                            {
                                pwordresult = MethResult.Error + ": the password must contain at least one special character.";
                            }
                        }
                        else
                        {
                            pwordresult = MethResult.Error + ": the password must contain at least one number.";
                        }
                    }
                    else
                    {
                        pwordresult = MethResult.Error + ": the password must contain at least one lowercase letter.";
                    }
                }
                else
                {
                    pwordresult = MethResult.Error + ": the password must contain at least one capital letter.";
                }
            }
            else
            {
                pwordresult = "ERROR: the password must be at least " + pwordlength + " characters in length.";
            }

            return pwordresult;
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
        public static string GetUTCTimeStr()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        }

    }
}

using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace CmnUtil
{
    public class EncUtil
    {
        public EncUtil()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetNewEncKey()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetSHA256Hash(string message)
        {
            string hash = "";

            if (!string.IsNullOrEmpty(message))
            {
                HashAlgorithm hashalg = new SHA256Managed();
                byte[] clearbytes = Encoding.ASCII.GetBytes(message);
                byte[] hashbytes = hashalg.ComputeHash(clearbytes);
                hash = Convert.ToBase64String(hashbytes);
            }

            return hash;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetHMACHash(string secretkey, string clearval)
        {
            string hexmsg = "";

            if (!string.IsNullOrEmpty(secretkey) && !string.IsNullOrEmpty(clearval))
            {
                var encoding = new ASCIIEncoding();
                byte[] key = encoding.GetBytes(secretkey);
                byte[] message = encoding.GetBytes(clearval);
                HMACSHA256 hmac = new HMACSHA256(key);
                //byte[] tmphash = hmac.ComputeHash(message);
                foreach (byte msg in hmac.Hash)
                {
                    hexmsg += msg.ToString("x2");
                }
            }

            return hexmsg;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool ValidateHMACHash(string secretkey, string clearval, string hashval)
        {
            bool result = false;

            // get hash value
            string hexMsg = GetHMACHash(secretkey, clearval);

            // compare hash values to authenticate message
            if (hashval == hexMsg)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string EncryptString(string clrStr, string encKey)
        {
            string encStr = "";

            if (!string.IsNullOrEmpty(clrStr) && !string.IsNullOrEmpty(encKey))
            {
                if (encKey.Length == 32)
                {
                    string salt = encKey.Substring(0, 16);
                    Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(encKey, Encoding.Unicode.GetBytes(salt));

                    using (RijndaelManaged rm = new RijndaelManaged())
                    {
                        rm.KeySize = 256;
                        rm.BlockSize = 128;
                        rm.Key = rfc.GetBytes(rm.KeySize / 8);
                        rm.IV = rfc.GetBytes(rm.BlockSize / 8);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, rm.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                byte[] data = Encoding.Unicode.GetBytes(clrStr);
                                cs.Write(data, 0, data.Length);
                                cs.FlushFinalBlock();

                                encStr = Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                }
            }

            return encStr;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string DecryptString(string encStr, string encKey)
        {
            string clrStr = "";

            if (!string.IsNullOrEmpty(encStr) && !string.IsNullOrEmpty(encKey))
            {
                if (encKey.Length == 32)
                {
                    string salt = encKey.Substring(0, 16);
                    Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(encKey, Encoding.Unicode.GetBytes(salt));

                    using (RijndaelManaged rm = new RijndaelManaged())
                    {
                        rm.KeySize = 256;
                        rm.BlockSize = 128;
                        rm.Key = rfc.GetBytes(rm.KeySize / 8);
                        rm.IV = rfc.GetBytes(rm.BlockSize / 8);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, rm.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                byte[] data = Convert.FromBase64String(encStr);
                                cs.Write(data, 0, data.Length);
                                cs.FlushFinalBlock();

                                byte[] clrbytes = ms.ToArray();
                                clrStr = Encoding.Unicode.GetString(clrbytes, 0, clrbytes.Length);
                            }
                        }
                    }
                }
            }

            return clrStr;
        }

        /// <summary>
        /// encrypts a symmetric key using an RSA public key
        /// </summary>
        public static string RSAEncryptKey(string clrKey, string pubKey)
        {
            string encKey = "";

            using (RSACryptoServiceProvider Rsa = new RSACryptoServiceProvider())
            {
                if (clrKey != null && clrKey != "" && pubKey != null && pubKey != "")
                {
                    Rsa.FromXmlString(pubKey);

                    byte[] clearbytes = Encoding.UTF8.GetBytes(clrKey);
                    byte[] encbytes = Rsa.Encrypt(clearbytes, false);

                    encKey = Convert.ToBase64String(encbytes);
                }
            }

            return encKey;
        }

        /// <summary>
        /// decrypts a symmetric key using an RSA keypair (private key)
        /// </summary>
        public static string RSADecryptKey(string encKey, string keyPair)
        {
            string clrKey = "";

            using (RSACryptoServiceProvider Rsa = new RSACryptoServiceProvider())
            {
                if (encKey != null && encKey != "" && keyPair != null && keyPair != "")
                {
                    Rsa.FromXmlString(keyPair);

                    byte[] encbytes = Convert.FromBase64String(encKey);
                    byte[] clearbytes = Rsa.Decrypt(encbytes, false);

                    clrKey = Encoding.UTF8.GetString(clearbytes);
                }
            }

            return clrKey;
        }


        /// <summary>
        /// Creates a new pair of private and public RSA XML keys
        /// </summary>
        public static void CreateAsymKeys(out string privateKey, out string publicKey)
        {
            //create RSA provider
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);

            // set key values
            privateKey = RSA.ToXmlString(true);
            publicKey = RSA.ToXmlString(false);

            RSA.Clear();
        }

        /// <summary>
        /// based on code from a StackOverflow discussion thread (Gzip compress string)
        /// </summary>
        public static string CompressString(string clrText)
        {
            string compStr = string.Empty;

            byte[] clrBytes = Encoding.UTF8.GetBytes(clrText);
            var memStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(clrBytes, 0, clrBytes.Length);
            }

            memStream.Position = 0;

            var compBytes = new byte[memStream.Length];
            memStream.Read(compBytes, 0, compBytes.Length);

            var gZipBytes = new byte[compBytes.Length + 4];
            Buffer.BlockCopy(compBytes, 0, gZipBytes, 4, compBytes.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(clrBytes.Length), 0, gZipBytes, 0, 4);
            compStr = Convert.ToBase64String(gZipBytes);

            return compStr;
        }

        /// <summary>
        /// based on code from a StackOverflow discussion thread
        /// </summary>
        public static string DecompressString(string compText)
        {
            string decompStr = string.Empty;

            byte[] gZipBytes = Convert.FromBase64String(compText);
            using (var memStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBytes, 0);
                memStream.Write(gZipBytes, 4, gZipBytes.Length - 4);

                var clrBytes = new byte[dataLength];

                memStream.Position = 0;
                using (var gZipStream = new GZipStream(memStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(clrBytes, 0, clrBytes.Length);
                }

                decompStr = Encoding.UTF8.GetString(clrBytes);
            }

            return decompStr;
        }
    }


}

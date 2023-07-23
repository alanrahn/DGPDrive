using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace ApiUtil
{
    public class EncryptUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public string GetHash(string message)
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
        /// based on code from a StackOverflow discussion thread (Gzip compress string)
        /// </summary>
        public string CompressString(string clrText)
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
        public string DecompressString(string compText)
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

        /// <summary>
        /// 
        /// </summary>
        public Stream CompressStream(Stream reqStream)
        {
            MemoryStream compStream = new MemoryStream();

            CompressionLevel compressionLevel = CompressionLevel.Fastest;

            using (GZipStream zipStream = new GZipStream(compStream, compressionLevel, true))
            {
                reqStream.CopyTo(zipStream);
            }

            compStream.Seek(0, SeekOrigin.Begin);

            return compStream;
        }

        /// <summary>
        /// 
        /// </summary>
        public Stream DecompressStream(Stream compStream)
        {
            MemoryStream clearStream = new MemoryStream();

            using (GZipStream zipstream = new GZipStream(compStream, CompressionMode.Decompress, true))
            {
                zipstream.CopyTo(clearStream);
            }

            clearStream.Seek(0, SeekOrigin.Begin);

            return clearStream;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EncryptString(string clrStr, string encKey)
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
        public string DecryptString(string encStr, string encKey)
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
        /// 
        /// </summary>
        public Stream EncryptStream(Stream clrStream, string encKey)
        {

            if (clrStream == null)
                throw new ArgumentNullException("clrStream");
            if (string.IsNullOrEmpty(encKey))
                throw new ArgumentNullException("encKey");

            MemoryStream encStream = new MemoryStream();

            string salt = encKey.Substring(0, 16);
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(encKey, Encoding.Unicode.GetBytes(salt));

            using (RijndaelManaged rm = new RijndaelManaged())
            {
                rm.KeySize = 256;
                rm.BlockSize = 128;
                rm.Key = rfc.GetBytes(rm.KeySize / 8);
                rm.IV = rfc.GetBytes(rm.BlockSize / 8);

                using (CryptoStream cryptoStream = new CryptoStream(clrStream, rm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.CopyTo(encStream);
                    cryptoStream.Close();
                }
            }

            return encStream;
        }

        /// <summary>
        /// 
        /// </summary>
        public Stream DecryptStream(Stream encStream, string encKey)
        {
            if (encStream == null)
                throw new ArgumentNullException("encStream");
            if (string.IsNullOrEmpty(encKey))
                throw new ArgumentNullException("encKey");

            MemoryStream clearStream = new MemoryStream();

            string salt = encKey.Substring(0, 16);
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(encKey, Encoding.Unicode.GetBytes(salt));

            using (RijndaelManaged rm = new RijndaelManaged())
            {
                rm.KeySize = 256;
                rm.BlockSize = 128;
                rm.Key = rfc.GetBytes(rm.KeySize / 8);
                rm.IV = rfc.GetBytes(rm.BlockSize / 8);

                using (CryptoStream cryptoStream = new CryptoStream(encStream, rm.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.CopyTo(clearStream);
                    cryptoStream.Close();
                }
            }

            return clearStream;
        }

        /// <summary>
        /// encrypts a symmetric key using an RSA public key
        /// </summary>
        public string RSAEncryptKey(string clrKey, string pubKey)
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
        public string RSADecryptKey(string encKey, string keyPair)
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
        /// 
        /// </summary>
        public string GetHMACHash(string secretkey, string msgstr)
        {
            string hexmsg = "";

            if (!string.IsNullOrEmpty(secretkey) && !string.IsNullOrEmpty(msgstr))
            {
                var encoding = new ASCIIEncoding();
                byte[] key = encoding.GetBytes(secretkey);
                byte[] message = encoding.GetBytes(msgstr);
                HMACSHA256 hmac = new HMACSHA256(key);
                byte[] tmphash = hmac.ComputeHash(message);
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
        public bool ValidateHMACHash(string secretkey, string msgstr, string hashval)
        {
            bool result = false;

            // get hash value
            string hexMsg = GetHMACHash(secretkey, msgstr);

            // compare hash values to authenticate message
            if (hashval == hexMsg)
            {
                result = true;
            }

            return result;
        }

    }
}

using System;
using System.Web;

using System.Configuration;
using System.Net;
using System.Text;
// added namespace to implement SHA-3 Encryption
//using HashLib;
using System.Web.Security;
// to check user authorization for requested directory access..
using System.IO;
using System.Drawing;


namespace YoelProject.Helpers
{
    public static class ExigentServiceHelper 
    {
        #region "Cloud service helper"

        public static void SetBasicAuthHeader(WebRequest req)
        {
            HttpCookie authcookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
             
            if (authcookie != null)
            {
                string ticket = authcookie.Value;
                req.Headers.Add("authticket", ticket);
            }
            else if (HttpContext.Current.Request.Headers.Count > 0 && HttpContext.Current.Request.Headers["authticket"] != null)
            {
                req.Headers.Add("authticket", HttpContext.Current.Request.Headers["authticket"].ToString());
            }
            authcookie = null;

            #region "commented-code"
            /*
            string authInfo = string.Empty;
            string accountName = ConfigurationManager.AppSettings["RestAPIAccountName"];
            string accountKey = ConfigurationManager.AppSettings["RestAPIAccountKey"];
           
            //Random objNewRequestNumber = new Random();
            //string randomNumber = objNewRequestNumber.Next().ToString();
            if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
            {
                authInfo = accountName + ":" + accountKey; //+ "$~$" + randomNumber;
            }
            
            if (!string.IsNullOrEmpty(authInfo))
            {
                IHash hashCode = HashFactory.Crypto.SHA3.CreateKeccak512();
                //HashAlgorithm hashAlgo = HashFactory.Wrappers.HashToHashAlgorithm(hashCode);
                HashResult authInfoString = hashCode.ComputeString(authInfo, Encoding.ASCII);
                req.Headers["Authorization"] = authInfoString.ToString();
               
            }
            //objNewRequestNumber = null;
            */
            #endregion

        }

        /// <summary>
        /// call method to set auth-cookie..
        /// </summary>
        public static void callAuthCookie(string strAccountName, string strAccountKey)
        {
            if (!isAuthCookieExist())   
            {
                // set authcookie..
                setAuthCookie(strAccountName, strAccountKey);
            }
        }


        /// <summary>
        /// expire auth cookie..
        /// </summary>
        public static void expireAuthCookie()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            }
        }
        /// <summary>
        /// check auth cookie existence..
        /// </summary>
        /// <returns></returns>
        public static bool isAuthCookieExist()
        {
            bool isAuthCookieExist = false;
            if (System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                isAuthCookieExist = true;
            }
            return isAuthCookieExist;
        }

        /// Modified by Shreya Garhwal - 03/16/2017
        /// <summary>
        /// set authenticate cookie for username and password..
        /// </summary>
        /// <param name="strUserName"></param>
        public static void setAuthCookie(string strAccountName, string strAccountKey)
        {
            string userdata = strAccountName + ":" + strAccountKey;
            try
            {
                string daysFromConfig = System.Configuration.ConfigurationManager.AppSettings["cookieExpirationInterval"].ToString();
                double days =Convert.ToDouble(daysFromConfig);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddDays(days), false, userdata);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(days);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                authTicket = null;
                authCookie = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                // dispose objects here.. if needed
            }
            
        }

        #endregion

        #region "File Extraction : In Bytes"
        public static byte[] GetFileContent(string fileUrl)
        {
            byte[] fileContent = null;
            try
            {
                string responseText = string.Empty;
                if (!string.IsNullOrEmpty(fileUrl))
                {
                    string url = ConfigurationManager.AppSettings["AzureStorageServiceUrl1"] + "?imagePath=" + fileUrl;
                    var azmblRequest = (HttpWebRequest)WebRequest.Create(url);
                    azmblRequest.Method = "GET";
                    // add authorization header to request header..
                    ExigentServiceHelper.SetBasicAuthHeader(azmblRequest);
                    var getResponse = (HttpWebResponse)azmblRequest.GetResponse();

                    if (getResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream respStream = getResponse.GetResponseStream())
                        {
                            var reader = new StreamReader(respStream, Encoding.ASCII);
                            responseText = reader.ReadToEnd();
                            if (!string.IsNullOrEmpty(responseText))
                            {
                                responseText = responseText.Replace("\"", "");
                                fileContent = System.Text.Encoding.ASCII.GetBytes(responseText);
                            }
                            reader.Close();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            { 
            
            }
            return fileContent;
        }

        #endregion

        #region "File Saving"
        public static string ImageToBase64(Image image,System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
      
        public static bool DeleteFileContent(string filePath, string fileName)
        {
            bool IsFileDeleted = false;
            try
            {
                string url = System.Web.Configuration.WebConfigurationManager.AppSettings["AzureStorageServiceUrl1"] + "?filePath=" + filePath + "" + "&fileName=" + fileName;

                HttpWebRequest getRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                getRequest.Method = "DELETE";
                // add authorization header to request header..
                ExigentServiceHelper.SetBasicAuthHeader(getRequest);

                var getResponse = (HttpWebResponse)getRequest.GetResponse();

                if (getResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream respStream = getResponse.GetResponseStream())
                    {
                        var reader = new StreamReader(respStream, Encoding.ASCII);
                        string responseText = reader.ReadToEnd();
                        reader.Close();
                        IsFileDeleted = Convert.ToBoolean(responseText);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return IsFileDeleted;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HOSEO_HEMS.Models;
using HOSEO_HEMS.Data;

namespace HOSEO_HEMS.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public bool HEMSLogin()
        {
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create("https://hems.hoseo.or.kr/hoseo/check_login.jsp");
            wReq.Method = "POST";
            wReq.Referer = "https://hems.hoseo.or.kr/hoseo/Login1.jsp";
            wReq.ContentType = "application/x-www-form-urlencoded";
            wReq.CookieContainer = CookieTemp.cookie;
            StreamWriter sw = new StreamWriter(wReq.GetRequestStream());
            sw.Write("gubun=S&mb_code=" + this.Username + "&mb_pass=" + this.Password);
            sw.Close();

            HttpWebResponse wRes = (HttpWebResponse)wReq.GetResponse();

            if (wRes.StatusCode == HttpStatusCode.OK)
            {
                Stream dataStream = wRes.GetResponseStream();
                StreamReader sr = new StreamReader(dataStream, Encoding.UTF8);
                string result = sr.ReadToEnd();

                if (!(result.Contains("https://hems.hoseo.or.kr/hoseo/Login1.jsp"))) //로그인 창이 아니라면 (로그인 성공 시 다음 페이지로 넘어감)
                {
                    foreach (Cookie cook in wRes.Cookies)
                    {
                        CookieTemp.cookie.Add(cook); //쿠키값 저장
                    }
                    wRes.Close();
                    dataStream.Close();
                    sr.Close();

                    return true;
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }
    }
}

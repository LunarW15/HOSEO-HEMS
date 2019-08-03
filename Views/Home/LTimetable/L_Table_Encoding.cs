using HOSEO_HEMS.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HOSEO_HEMS.Views
{
    public class L_Table_Encoding
    {
        public string Encoding_(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //euc-kr을 지원하지 않으므로 인코딩 메소드 생성
            var endcoingCode = 51949; //euc-kr 코드
            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(endcoingCode); //euc-kr 인코딩 작성

            WebClient wc = new WebClient();

            //wc.Headers.Add(HttpRequestHeader.Cookie, CookieTemp.Cookie); //cookieTemp에서 Cookie가져옴

            byte[] docBytes = wc.DownloadData(url);
            string encodeType = wc.ResponseHeaders["Content-Type"];

            string charsetKey = "charset";
            int pos = encodeType.IndexOf(charsetKey);

            Encoding currentEncoding = Encoding.Default;
            if (pos != -1)
            {
                pos = encodeType.IndexOf("=", pos + charsetKey.Length);
                if (pos != -1)
                {
                    string charset = encodeType.Substring(pos + 1);
                    currentEncoding = Encoding.GetEncoding(charset);
                }
            }

            string result = currentEncoding.GetString(docBytes); // 대상 웹 서버가 인코딩한 설정으로 디코딩!
            return result;
        }
    }
}

﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using HOSEO_HEMS.Models;
using HOSEO_HEMS.Data;

namespace HOSEO_HEMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class All_Credit : ContentPage
    {
        public ObservableCollection<string> AC_Items1 { get; set; }
        public ObservableCollection<string> AC_Items2 { get; set; }

        public All_Credit()
        {
            InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //euc-kr을 지원하지 않으므로 인코딩 메소드 생성
            var endcoingCode = 51949; //euc-kr 코드
            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(endcoingCode); //euc-kr 인코딩 작성

            WebClient wc = new WebClient();

            //wc.Headers.Add(HttpRequestHeader.Cookie, CookieTemp.Cookie); //cookieTemp에서 Cookie가져옴
            string url = "https://hems.hoseo.or.kr/hoseo/stu/mem/inf/hom/STUHOM0110L0.jsp";
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

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(result);

            string subject1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[1]").InnerHtml;
            string title1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;

            string subject2 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[6]/td[1]").InnerHtml;
            string title2 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[6]/td[3]/a/font").InnerHtml;

            string subject3 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[8]/td[1]").InnerHtml;
            string title3 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[8]/td[3]/a/font").InnerHtml;

            string subject4 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[10]/td[1]").InnerHtml;
            string title4 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[10]/td[3]/a/font").InnerHtml;

            string subject5 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[12]/td[1]").InnerHtml;
            string title5 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[12]/td[3]/a/font").InnerHtml;

            AC_Items1 = new ObservableCollection<string>
            {
                subject1,
                subject2,
                subject3,
                subject4,
                subject5
            };

            AC_Items2 = new ObservableCollection<string>
            {
                title1,
                title2,
                title3,
                title4,
                title5
            };

            subject.ItemsSource = AC_Items1;
            title.ItemsSource = AC_Items2;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("알림", "지원하지 않은 기능입니다", "확인");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}

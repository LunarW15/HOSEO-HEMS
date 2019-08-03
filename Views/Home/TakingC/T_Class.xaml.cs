using System;
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
    public partial class T_Class : ContentPage
    {
        public ObservableCollection<string> division_Items { get; set; }
        public ObservableCollection<string> subject_Items { get; set; }
        public ObservableCollection<string> instructor_Items { get; set; }

        public T_Class()
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

            string division1 = doc.DocumentNode.SelectSingleNode(".//html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[5]/td[1]").InnerHtml;
            string subject1 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[5]/td[3]/b").InnerHtml;
            string instructor1 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[5]/td[5]").InnerHtml;

            /*
            string division2 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[7]/td[1]").InnerHtml;
            string subject2 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[7]/td[3]/b").InnerHtml;
            string instructor2 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[7]/td[5]").InnerHtml;

            string division3 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[9]/td[1]").InnerHtml;
            string subject3 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[9]/td[3]/b").InnerHtml;
            string instructor3 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[9]/td[5]").InnerHtml;

            string division4 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[11]/td[1]").InnerHtml;
            string subject4 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[11]/td[3]/b").InnerHtml;
            string instructor4 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[11]/td[5]").InnerHtml;

            string division5 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[13]/td[1]").InnerHtml;
            string subject5 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[13]/td[3]/b").InnerHtml;
            string instructor5 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[13]/td[5]").InnerHtml;

            string division6 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[15]/td[1]").InnerHtml;
            string subject6 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[15]/td[3]/b").InnerHtml;
            string instructor6 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[15]/td[5]").InnerHtml;

            string division7 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[17]/td[1]").InnerHtml;
            string subject7 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[17]/td[3]/b").InnerHtml;
            string instructor7 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[17]/td[5]").InnerHtml;

            string division8 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[19]/td[1]").InnerHtml;
            string subject8 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[19]/td[3]/b").InnerHtml;
            string instructor8 = doc.DocumentNode.SelectSingleNode("/html/body/table/tbody/tr[1]/td[1]/table/tbody/tr[4]/td[2]/table[2]/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr[19]/td[5]").InnerHtml;
            */

            division_Items = new ObservableCollection<string>
            {
                division1,
                /*
                division2,
                division3,
                division4,
                division5,
                division6,
                division7,
                division8
                */
            };

            subject_Items = new ObservableCollection<string>
            {
                subject1,
                /*
                subject2,
                subject3,
                subject4,
                subject5,
                subject6,
                subject7,
                subject8
                */
            };

            instructor_Items = new ObservableCollection<string>
            {
                instructor1,
                /*
                instructor2,
                instructor3,
                instructor4,
                instructor5,
                instructor6,
                instructor7,
                instructor8
                */
            };

            division.ItemsSource = division_Items;
            subject.ItemsSource = subject_Items;
            instructor.ItemsSource = instructor_Items;
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

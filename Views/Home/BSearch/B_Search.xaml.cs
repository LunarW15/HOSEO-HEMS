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
    public partial class B_Search : ContentPage
    {
        public string Pick_vartext { get; set; }
        public ObservableCollection<B_Search_Data> elements { get; set; }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            lbl_bname.TextColor = Constants.MainTextColor;
            lbl_bauthor.TextColor = Constants.MainTextColor;
            lbl_bpub.TextColor = Constants.MainTextColor;
        }
        async void NetCheck()
        {
            NetworkCheck State = new NetworkCheck();
            if (State.CheckConnectivity()) //연결됨
            {

            }
            else
            {
                await DisplayAlert("네트워크 연결 오류", "네트워크 연결 상태를 확인해주세요!", "확인");
            }
        }
        public B_Search()
        {
            InitializeComponent();
            Init();
            NetCheck();

            pick_search.Items.Add("도서명");
            pick_search.Items.Add("저자");
            pick_search.Items.Add("출판사");

            pick_search.SelectedIndex = 0; //도서명을 기본값으로 선택되도록
        }
        private void Pick_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (pick_search.SelectedIndex)
            {
                case 0:
                    Pick_vartext = "bookNm";
                    break;
                case 1:
                    Pick_vartext = "author";
                    break;
                case 2:
                    Pick_vartext = "pubCompany";
                    break;
                default:
                    Pick_vartext = null;
                    break;
            }
        }

        private void Btn_search_Clicked(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //euc-kr을 지원하지 않으므로 인코딩 메소드 생성
            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949); //euc-kr 인코딩 작성

            WebClient wc = new WebClient();

            //wc.Headers.Add(HttpRequestHeader.Cookie, CookieTemp.Cookie); //cookieTemp에서 Cookie가져옴
            string url = "https://hems.hoseo.or.kr/hoseo/stu/mem/inf/hom/bookSearch.jsp";
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

            string BNo1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[1]").InnerHtml;
            string BName1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BAuthor1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPub1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPubY1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;

            string BNo2 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[1]").InnerHtml;
            string BName2 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BAuthor2 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPub2 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPubY2 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;

            string BNo3 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[1]").InnerHtml;
            string BName3 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BAuthor3 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPub3 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPubY3 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;

            string BNo4 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[1]").InnerHtml;
            string BName4 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BAuthor4 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPub4 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPubY4 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;

            string BNo5 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[1]").InnerHtml;
            string BName5 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BAuthor5 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPub5 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;
            string BPubY5 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;

            elements = new ObservableCollection<B_Search_Data>();
            elements.Add(new B_Search_Data
            {
                BName = BName1,
                BAuthor = BAuthor1,
                BPub = BPub1
            });
            elements.Add(new B_Search_Data
            {
                BName = BName2,
                BAuthor = BAuthor2,
                BPub = BPub2
            });
            elements.Add(new B_Search_Data
            {
                BName = BName3,
                BAuthor = BAuthor3,
                BPub = BPub3
            });
            elements.Add(new B_Search_Data
            {
                BName = BName4,
                BAuthor = BAuthor4,
                BPub = BPub4
            });
            elements.Add(new B_Search_Data
            {
                BName = BName5,
                BAuthor = BAuthor5,
                BPub = BPub5
            });

            B_Search_View.ItemsSource = elements;
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

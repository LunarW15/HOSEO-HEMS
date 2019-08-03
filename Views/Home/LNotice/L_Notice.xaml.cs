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
    public partial class L_Notice : ContentPage //강의 공지사항
    {
        public ObservableCollection<L_Notice_Data> elements { get; set; }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            lbl_subject.TextColor = Constants.MainTextColor;
            lbl_title.TextColor = Constants.MainTextColor;
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
        public L_Notice()
        {
            InitializeComponent();
            Init();
            NetCheck();

            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //euc-kr을 지원하지 않으므로 인코딩 메소드 생성
            //System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949); //euc-kr 인코딩 작성

            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create("https://hems.hoseo.or.kr/hoseo/stu/mem/inf/hom/STUHOM0110L0.jsp");
            wReq.Method = "GET";
            wReq.Referer = "https://hems.hoseo.or.kr/hoseo/check_login.jsp";
            wReq.ContentType = "text/html;charset=euc-kr";
            wReq.CookieContainer = CookieTemp.cookie;

            HttpWebResponse wRes = (HttpWebResponse)wReq.GetResponse();

            if (wRes.StatusCode == HttpStatusCode.OK)
            {
                Stream dataStream = wRes.GetResponseStream();
                StreamReader sr = new StreamReader(dataStream, Encoding.UTF8); //euckr
                string result = sr.ReadToEnd();
                DisplayAlert("test", result, "확인");
            }

                /*
                elements = new ObservableCollection<L_Notice_Data>();
                elements.Add(new L_Notice_Data
                {
                    Subjects = subject1,
                    Titles = title1
                });

                elements.Add(new L_Notice_Data
                {
                    Subjects = subject2,
                    Titles = title2
                });
                elements.Add(new L_Notice_Data
                {
                    Subjects = subject3,
                    Titles = title3
                });
                elements.Add(new L_Notice_Data
                {
                    Subjects = subject4,
                    Titles = title4
                });
                elements.Add(new L_Notice_Data
                {
                    Subjects = subject5,
                    Titles = title5
                });


                L_Notice_View.ItemsSource = elements;
                */
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

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
    public partial class L_Ttable : ContentPage
    {
        public ObservableCollection<L_Ttable_Data> elements { get; set; }

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

        public L_Ttable()
        {
            InitializeComponent();
            Init();
            NetCheck();

            L_Table_Encoding encoding_ = new L_Table_Encoding();

            string result = encoding_.Encoding_("https://hems.hoseo.or.kr/hoseo/stu/mem/inf/hom/STUHOM0110L0.jsp");

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(result); //오전 12:26 2019-07-28 안드로이드 실행 시 팅김
            //원인 한글은 2바이트 안그래도 대량의 데이터를 한번에 로딩해서 버벅이는데 한글로 인코딩하면서 배로 램오버로 팅기는 것으로 추측

            string subject1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[1]").InnerHtml;
            string title1 = doc.DocumentNode.SelectSingleNode("//*[@id='tab1']/tbody/tr[4]/td[3]/a/font").InnerHtml;

            elements = new ObservableCollection<L_Ttable_Data>();
            elements.Add(new L_Ttable_Data
            {
                Subjects = subject1,
                Titles = title1
            });

            L_Ttable_View.ItemsSource = elements;
        }
    }
}

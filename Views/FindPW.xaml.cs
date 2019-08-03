using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HOSEO_HEMS.Models;
using HOSEO_HEMS.Data;
using System.Net;
using System.IO;

namespace HOSEO_HEMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindPW : ContentPage
    {
        public FindPW()
        {
            InitializeComponent();
            Init();
            //NetCheck();
        }
        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            lbl_cno.TextColor = Constants.MainTextColor;
            lbl_name.TextColor = Constants.MainTextColor;
            lbl_resno.TextColor = Constants.MainTextColor;
            lbl_phoneno.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
        }

        private async void New_pwb_Clicked(object sender, EventArgs e)
        {
            NetworkCheck State = new NetworkCheck();
            if (State.CheckConnectivity()) //연결됨
            {
                if (String.IsNullOrWhiteSpace(entry_cno.Text)) //입력 정보 검사
                {
                    await DisplayAlert("학번 누락", "학번을 입력해주세요", "확인");
                }
                else if (entry_cno.Text.Length != 10)
                {
                    await DisplayAlert("학번 이상", "학번은 10자리입니다", "확인");
                }
                else if (String.IsNullOrWhiteSpace(entry_name.Text))
                {
                    await DisplayAlert("이름 누락", "이름을 입력해주세요", "확인");
                }
                else if (String.IsNullOrWhiteSpace(entry_resno.Text))
                {
                    await DisplayAlert("주민번호 누락", "주민번호를 입력해주세요", "확인");
                }
                else if (entry_resno.Text.Length != 6)
                {
                    await DisplayAlert("주민번호 이상", "주민번호 앞자리는 6자리입니다", "확인");
                }
                else if (String.IsNullOrWhiteSpace(entry_phoneno.Text))
                {
                    await DisplayAlert("핸드폰 번호 누락", "핸드폰 번호를 입력해주세요", "확인");
                }
                else if (entry_phoneno.Text.Length < 11)
                {
                    await DisplayAlert("핸드폰 번호 이상", "잘못된 번호입니다", "확인");
                }
                else
                {
                    HttpWebRequest hreq1 = (HttpWebRequest)WebRequest.Create("https://hems.hoseo.or.kr/hoseo/hak/cmm/stusmscert01.jsp"); //정보 입력창 생성
                    hreq1.Method = "GET"; //메소드 GET 방식
                    hreq1.ContentType = "text/html;charset=euc-kr";
                    hreq1.CookieContainer = new CookieContainer();

                    HttpWebResponse hres1 = (HttpWebResponse)hreq1.GetResponse(); //hres1 생성 (hreq1 요청)
                    hres1.Cookies = hreq1.CookieContainer.GetCookies(hreq1.RequestUri); //hreq1 쿠키값을 가져와서 hres1 쿠키에 대입

                    HttpWebRequest hreq2 = (HttpWebRequest)WebRequest.Create("https://hems.hoseo.or.kr/hoseo/hak/cmm/stusmscert02.jsp"); //발급창 생성
                    hreq2.Method = "POST"; //메소드 POST 방식
                    hreq2.Referer = "https://hems.hoseo.or.kr/hoseo/hak/cmm/stusmscert01.jsp"; //정보 입력창을 참조
                    hreq2.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    hreq2.CookieContainer = hreq1.CookieContainer; //hreq1에서 얻은 쿠키값을 hreq2에 대입

                    StreamWriter sw = new StreamWriter(hreq2.GetRequestStream());
                    sw.Write("mb_id=" + entry_cno.Text.ToString() + "&stu_name=" + entry_name.Text.ToString() + "&pid=" + entry_resno.Text.ToString() + "&mobile=" + entry_phoneno.Text.ToString()); //데이터 생성
                    sw.Close();

                    HttpWebResponse hres2 = (HttpWebResponse)hreq2.GetResponse(); //hres2 생성 (hreq2 <발급창> 요청)
                    hres2.Cookies = hreq2.CookieContainer.GetCookies(hreq2.RequestUri); //hreq2 쿠키값을 가져와서 hres2 쿠키에 대입

                    if (hres1.StatusCode == HttpStatusCode.OK)
                    {
                        Stream dataStream = hres2.GetResponseStream();
                        StreamReader sr = new StreamReader(dataStream, Encoding.UTF8);
                        string result = sr.ReadToEnd();

                        if (!(result.Contains("https://hems.hoseo.or.kr/hoseo/hak/cmm/stusmscert02.jsp"))) //요청 발급창이 아니라면 (요청 발급 성공 시 페이지 종료됨)
                        {
                            hres1.Close();
                            hres2.Close();
                            dataStream.Close();
                            sr.Close();

                            await DisplayAlert("인증 완료", "학생 본인 인증에 성공하였습니다\n새로운 비밀번호가 SMS로 전송되었습니다", "확인");
                            await this.Navigation.PopModalAsync();
                        }
                        else
                        {
                            await DisplayAlert("인증 오류", "인증에 실패했습니다", "확인");
                        }
                    }

                    else
                    {

                    }
                }
            }
            else //연결안됨
            {
                await DisplayAlert("네트워크 연결 오류", "네트워크 연결 상태를 확인해주세요!", "확인");
            }
        }
    }
}
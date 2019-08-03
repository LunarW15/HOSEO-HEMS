using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using HOSEO_HEMS.Models;
using HOSEO_HEMS.Data;

namespace HOSEO_HEMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
            NetCheck();
        }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            lbl_username.TextColor = Constants.MainTextColor;
            lbl_password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
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

        async void Signin_Clicked(object sender, EventArgs e)
        {
            NetworkCheck State = new NetworkCheck();
            if (State.CheckConnectivity()) //연결됨
            {
                if (String.IsNullOrWhiteSpace(entry_id.Text) && (String.IsNullOrWhiteSpace(entry_pw.Text)))
                {
                    await DisplayAlert("ID/PW 누락", "아이디와 비밀번호를 입력해주세요", "확인");
                }
                else if (String.IsNullOrWhiteSpace(entry_id.Text))
                {
                    await DisplayAlert("ID 누락", "아이디를 입력해주세요", "확인");
                }
                else if (String.IsNullOrWhiteSpace(entry_pw.Text))
                {
                    await DisplayAlert("PW 누락", "비밀번호를 입력해주세요", "확인");
                }
                else
                {
                    User user = new User(entry_id.Text.ToString(), entry_pw.Text.ToString());
                    if (user.HEMSLogin())
                    {
                        await DisplayAlert("로그인 성공", "로그인 되었습니다", "확인");
                        await this.Navigation.PushModalAsync(new Home());
                        //this.Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("로그인 실패", "로그인 실패했습니다", "확인");
                    }
                }
            }
            else //연결안됨
            {
                await DisplayAlert("네트워크 연결 오류", "네트워크 연결 상태를 확인해주세요!", "확인");
            }
        }
        async void Findpw_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new FindPW());
        }
    }
}
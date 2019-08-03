using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HOSEO_HEMS.Models;
using HOSEO_HEMS.Data;

namespace HOSEO_HEMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            Init();
            NetCheck();
        }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
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

        private void LNotice_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new L_Notice()); //강의 공지사항
        }

        private void Timetable_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new Ttable()); //시간표
        }

        private void TakingC_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new T_Class()); //수강교과목
        }

        private void LTimetable_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new L_Ttable()); //강의시간표
        }

        private void LBoard_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new L_Board()); //강의게시판
        }

        private void AllCredit_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new All_Credit()); //전체학점조회
        }

        private void AllGrade_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new All_Grade()); //전체성적조회
        }
        private void LEvaluation_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new L_Evaluation()); //강의평가
        }

        private void VSSearch_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new VS_Search()); //봉사활동조회
        }
        private void BSearch_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new B_Search()); //도서검색
        }
    }
}
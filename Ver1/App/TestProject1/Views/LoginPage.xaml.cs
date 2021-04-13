using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Models;
using TestProject1.Views.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            App.StartCheckInternet(Lbl_NoInternet, this);
            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedureAsync(s, e);
            
        }

        async void SignInProcedureAsync(object sender, EventArgs e)
        {
            if (Entry_Password.Text != null)
            {
                User user = new User(Entry_Username.Text, Entry_Password.Text);

                if (user.CheckInformation())// && App.UserDatabase.CheckUser(user))
                {
                    await DisplayAlert("Login", "Login success", "Oke");
                    //var result = await App.RestService.Login(user);
                    
                    var result = new Token();
                    if (result != null)
                    {
                        //App.UserDatabase.SaveUser(user);
                        //App.TokenDatabase.SaveToken(result);
                        Application.Current.MainPage = new NavigationPage(new MasterDetail());
                    }

                }
                else
                    await DisplayAlert("Login", "Login fail", "OK");
            }
            else await DisplayAlert("Password", "Need to complete login entry", "OK");
        }
    }
}
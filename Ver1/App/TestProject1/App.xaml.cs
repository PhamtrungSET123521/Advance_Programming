using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TestProject1.Data;
using TestProject1.Models;
using TestProject1.Views;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace TestProject1
{
   
    public partial class App : Application
    {
        static DBDeviceController dbcontroller;
        static TokenDatabaseController tokenDatabase;
        static UserDatabaseController userDatabase;
        static RestService restService;
        static RestService2 restService2;
        private static Label labelScreen;
        private static bool hasInternet;
        private static Page currentpage;
        private static Timer timer;
        private static bool noInterShow;

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if(userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }
                return userDatabase;
            }
        }
        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokenDatabase == null)
                {
                    tokenDatabase = new TokenDatabaseController();
                }
                return tokenDatabase;
            }
        }

        public static DBDeviceController Dbcontroller
        {
            get
            {
                if(dbcontroller == null)
                {
                    dbcontroller = new DBDeviceController();
                }
                return dbcontroller;
            }
        }

        public static RestService RestService
        {
            get
            {
                if(restService == null)
                {
                    restService = new RestService();
                }
                return restService;
            }
        }

        public static RestService2 RestService2
        {
            get
            {
                if (restService2 == null)
                {
                    Debug.WriteLine("NULLLLLLLLL");
                    restService2 = new RestService2();
                }
                return restService2;
            }
        }


        //--------------- Check Internet ------------------
        public static void StartCheckInternet(Label label, Page page)
        {
            labelScreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            hasInternet = true;
            currentpage = page;
            if(timer == null)
            {
                timer = new Timer((e) =>
                {
                    CheckIfInternetOverTime();
                }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);
            }
        }

        private static void CheckIfInternetOverTime()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if(!networkConnection.IsConnected)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (hasInternet)
                    {
                        if (!noInterShow)
                        {
                            hasInternet = false;
                            labelScreen.IsVisible = true;
                            await ShowDisplayAlert();
                        }
                    }
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    hasInternet = true;
                    labelScreen.IsVisible = false;
                });
            }
        }

        public static async Task<bool> CheckIfInternet()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            return networkConnection.IsConnected;
        }

        public static async Task<bool> CheckIfInternetAlert()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if (!networkConnection.IsConnected)
            {
                if (!noInterShow)
                {
                    await ShowDisplayAlert();
                }
                return false;
            }
            return true;
        }

        private static async Task ShowDisplayAlert()
        {
            noInterShow = false;
            await currentpage.DisplayAlert("Internet", "Device has no Internet", "OK");
            noInterShow = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestProject1.Models
{
    public class Constants
    {
        public static bool IsDev = true;

        public static Color BackgroundColor = Color.FromRgb(58,153,215);

        public static Color MainTextColor = Color.White;

        public static int LoginIconHeight = 120;
        //------------------Login----------------------

        public static string LoginUrl = "https://test.com/api/Auth/Login";

        public static string StateUri = "https://localhost:44379/";

        public static string NoInternetText = "No Internet, please reconnect";
    }
}

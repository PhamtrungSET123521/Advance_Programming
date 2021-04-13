using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Models;
using TestProject1.Views.DetailViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject1.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listview; } }
        public List<MasterMenuItem> items;
        public MasterPage()
        {
            InitializeComponent();
            SetItems();
        }

        void SetItems()
        {
            items = new List<MasterMenuItem>();
            items.Add(new MasterMenuItem("Devices", "icon.png", Color.White, typeof(InfoScreen1)));
            items.Add(new MasterMenuItem("Accounts", "icon.png", Color.White, typeof(InfoScreen2)));
            ListView.ItemsSource = items;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestProject1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject1.Views.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoScreen2 : ContentPage
    {
        private ObservableCollection<User> users = new ObservableCollection<User>();
        public InfoScreen2()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            var enumerator = App.UserDatabase.GetUser();
            if (enumerator == null)
            {
                App.UserDatabase.SaveUser(new User { Username = "hung", Password = "hung" });
                App.UserDatabase.SaveUser(new User { Username = "nhat", Password = "nhat" });
                App.UserDatabase.SaveUser(new User { Username = "admin", Password = "admin" });
                enumerator = App.UserDatabase.GetUser();
            }
            while (enumerator.MoveNext())
            {
                this.users.Add(enumerator.Current);
            }
            UserListView.ItemsSource = this.users;
        }

        public void RemoveUser(object sender, System.EventArgs e)
        {
            var item = (MenuItem)sender;
            User model = item.CommandParameter as User;
            this.users.Remove(model);
            App.UserDatabase.DeleteUser(model.Id);
        }

        public void AddUser(object sender, EventArgs e)
        {
            User device = new User();
            device.Username = EntryUsername.Text;
            device.Password = EntryPassword.Text;
            this.users.Add(device);
            App.UserDatabase.SaveUser(device);
        }
    }

   

}
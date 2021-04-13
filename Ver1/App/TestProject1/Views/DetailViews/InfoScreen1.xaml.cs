using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Input;
using TestProject1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject1.Views.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoScreen1 : ContentPage
    {
        private ObservableCollection<Devices> devices = new ObservableCollection<Devices>();
        //public Devices SelectedDevice { get; set; }

        //public ICommand RemoveDeviceCommand => new Command(RemoveDevice);
        public InfoScreen1()
        {
            InitializeComponent();
            Init();
        }

        public async Task Init()
        {
            var enumerator = App.Dbcontroller.GetDBDevice();
            var state = await UpdatePhoneAsync();
            string[] state_array = state.Split(',');
            int i = 0;

            if (enumerator == null)
            {
                App.Dbcontroller.SaveDBDevice(new Devices { Id = 0, DeviceName = "Device 1", value = false });
                App.Dbcontroller.SaveDBDevice(new Devices { Id = 0, DeviceName = "Device 2", value = false });
                App.Dbcontroller.SaveDBDevice(new Devices { Id = 0, DeviceName = "Device 3", value = false });
                //App.Dbcontroller.DeleteDBDevice(0);
                enumerator = App.Dbcontroller.GetDBDevice();
            }
            while (enumerator.MoveNext())
            {
                if(i < 3)
                {
                    enumerator.Current.value = state_array[i] == "on" ? true : false; 
                    App.Dbcontroller.SaveDBDevice(enumerator.Current);
                    i++;
                }
                this.devices.Add(enumerator.Current);
            }
            DeviceListView.ItemsSource = this.devices;

            //await UpdateInternetAsync(this.devices.ElementAt<Devices>(0).value, 1);
            //await UpdateInternetAsync(this.devices.ElementAt<Devices>(1).value, 2);
            //await UpdateInternetAsync(this.devices.ElementAt<Devices>(2).value, 3);
        }

        public void RemoveDevice(object sender, System.EventArgs e)
        {
            var item =  (MenuItem)sender;
            Devices model = item.CommandParameter as Devices;
            this.devices.Remove(model);
            App.Dbcontroller.DeleteDBDevice(model.Id);
        }

        public void AddDevice(object sender, EventArgs e)
        {
            Devices device = new Devices();
            device.DeviceName = EntryDevice.Text;
            this.devices.Add(device);
            App.Dbcontroller.SaveDBDevice(device);
        }

        public async void ToggledDevice(object sender, ToggledEventArgs e)
        {
            ViewCell cell = (sender as Xamarin.Forms.Switch).Parent.Parent as ViewCell;
            Devices device = cell.BindingContext as Devices;
            App.Dbcontroller.SaveDBDevice(device);
            await UpdateInternetAsync(this.devices.ElementAt<Devices>(0).value, 1);
            await UpdateInternetAsync(this.devices.ElementAt<Devices>(1).value, 2);
            await UpdateInternetAsync(this.devices.ElementAt<Devices>(2).value, 3);
        }

        public async Task UpdateInternetAsync(bool state, int deviceNumber)
        {
            var _state = (state == true) ? "on" : "off";
            Debug.WriteLine(_state + "--------------------------");
            await App.RestService2.PostResponse2("https://10.0.2.2:5001/home/send?command="+_state+"&&roomNumber="+deviceNumber.ToString());
            //Thread.Sleep(200);
            Debug.WriteLine(_state);
        }
        public async Task<string> UpdatePhoneAsync()
        {  
            var state = await App.RestService2.GetResponse2("https://10.0.2.2:5001/home/status");
            return state;
        }
    }
}
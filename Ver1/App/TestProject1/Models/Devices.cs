using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestProject1.Models
{
    
    public class Devices
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        
        public string DeviceName { get; set; }
        public bool value { get; set; }

        //public string SelectedDevice { get; set; }

    }
}

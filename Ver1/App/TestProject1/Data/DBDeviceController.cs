using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.Models;
using Xamarin.Forms;

namespace TestProject1.Data
{
    public class DBDeviceController
    {
        private static object locker = new object();
        private SQLiteConnection database;

        public DBDeviceController()
        {
            this.database = DependencyService.Get<ISQLite>().GetConnection();
            this.database.CreateTable<Devices>();
        }

        public IEnumerator<Devices> GetDBDevice()
        {
            lock (locker)
            {
                if(this.database.Table<Devices>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<Devices>().GetEnumerator();
                }
            }
        }

        public int SaveDBDevice(Devices device)
        {
            lock (locker)
            {
                if(device.Id != 0)
                {
                    this.database.Update(device);
                    return device.Id;
                }
                else
                {
                    return this.database.Insert(device);
                }
            }
        }

        public int DeleteDBDevice(int id)
        {
            lock (locker)
            {
                return this.database.Delete<Devices>(id);
            }
        }

        public int UpdateDBDevice(Devices device)
        {
            lock (locker)
            {
                return this.database.Update(device);
            }
        }
    }
}

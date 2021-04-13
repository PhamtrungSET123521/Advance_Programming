using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestProject1.Models;
using Xamarin.Forms;

namespace TestProject1.Data
{
    public class UserDatabaseController
    {
        private static object locker = new object();
         private SQLiteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<User>();
        }
        public IEnumerator<User> GetUser()
        {
            lock (locker)
            {
                if(this.database.Table<User>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<User>().GetEnumerator();
                }
            }
        }

        public int SaveUser(User user)
        {
            lock (locker)
            {
                if(user.Id != 0)
                {
                    this.database.Update(user);
                    return user.Id;
                }
                else
                {
                    return this.database.Insert(user);
                }
            }
        }
        public int DeleteUser(int id)
        {
            lock(locker)
            {
                return this.database.Delete<User>(id);
            }
        }

        public bool CheckUser(User user)
        {
            lock(locker)
            {
                List<User> users = this.database.Query<User>("Select * From User");
                for (int i = 0; i < users.Count(); i++)
                {
                    if(user.Username == users.ElementAt<User>(i).Username && user.Password == users.ElementAt<User>(i).Password) 
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

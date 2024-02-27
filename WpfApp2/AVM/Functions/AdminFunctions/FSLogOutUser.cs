using System.Threading.Tasks;
using WpfApp2.Data.Account;
using WpfApp2.Data;
using WpfApp2.Client;
using WpfApp2.Command;
using System.Net.Http;
using System.Windows;
using System;
using System.IO;
using System.Text.Json;

namespace WpfApp2.AVM.Functions.AdminFunctions
{
    public class FSLogOutUser : SpeсialFunction
    {
        public FSLogOutUser(User user) : base(null)
        {
            this.user = user;

            
        }

        public event LogOutUserHandler Notify;

        User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                if (user == null)
                    Notify?.Invoke();
            }
        }


        //WCommand logOutUser;
        //public WCommand LogOutUser { get { return logOutUser; } }

    }
}

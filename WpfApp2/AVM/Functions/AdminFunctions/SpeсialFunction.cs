using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Client;
using WpfApp2.Data;
using WpfApp2.Data.Account;

namespace WpfApp2.AVM.Functions.AdminFunctions
{
    public delegate void LogInUserHandler(User user);
    public delegate void LogOutUserHandler();
    public delegate void ChangeUserHandler();
    public delegate void ShowUserHandler(UserVMForAdmin user);
    public class SpeсialFunction : INotifyPropertyChanged
    {
        public SpeсialFunction(AccountClient queryMaker) 
        {
            this.queryMaker = queryMaker;
        }

        protected AccountClient queryMaker;


        HttpResponseMessage result;
        virtual public HttpResponseMessage Result
        { get { return result; } set { result = value; } }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

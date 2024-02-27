using System.Threading.Tasks;
using WpfApp2.Command;
using WpfApp2.Client;
using WpfApp2.Data.Account;
using System.Net.Http;
using System.Windows;

namespace WpfApp2.AVM.Functions.AdminFunctions
{
    public class FSShowUser: SpeсialFunction
    {
        public FSShowUser(): base(null) 
        { }

        public event ShowUserHandler Notify;

        UserVMForAdmin userInWork;
        public UserVMForAdmin UserInWork
        {
            get { return userInWork; }
            set 
            { 
                userInWork = value;
                Notify?.Invoke(userInWork);
                OnPropertyChanged("UserInWork"); 
            }
        }
    }
}

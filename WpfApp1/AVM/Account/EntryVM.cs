using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.AVM.Account
{
    public class EntryVM: INotifyPropertyChanged
    {
        string login;
        string password;

        public string LoginProp 
        { 
            get { return login; } 
            set { login = value; OnPropertyChanged("LoginProp"); } 
        }

        public string PassWord 
        { 
            get { return password; } 
            set { password = value; OnPropertyChanged("PassWord"); } 
        }

        public EntryVM()
        {
            login = "";
            password = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
